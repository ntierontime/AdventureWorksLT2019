using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.EFCoreContext;
using AdventureWorksLT2019.Models;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;

namespace AdventureWorksLT2019.EFCoreRepositories
{
    public class ProductDescriptionRepository
        : IProductDescriptionRepository
    {
        private readonly ILogger<ProductDescriptionRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ProductDescriptionRepository(EFDbContext dbcontext, ILogger<ProductDescriptionRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<ProductDescriptionDataModel> SearchQuery(
            ProductDescriptionAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ProductDescription

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Description!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Description!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Description!, "%" + query.TextSearch)))
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Description) ||
                            query.DescriptionSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Description!, "%" + query.Description + "%") ||
                            query.DescriptionSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Description!, query.Description + "%") ||
                            query.DescriptionSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Description!, "%" + query.Description))

                select new ProductDescriptionDataModel
                {

                        ProductDescriptionID = t.ProductDescriptionID,
                        Description = t.Description,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                };

            // 1. Without Paging And OrderBy
            if (!withPagingAndOrderBy)
                return queryable;

            // 2. With Paging And OrderBy
            var orderBys = QueryOrderBySetting.Parse(query.OrderBys);
            if (orderBys.Any())
            {
                queryable = queryable.OrderBy(QueryOrderBySetting.GetOrderByExpression(orderBys));
            }

            queryable = queryable.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);

            return queryable;
        }

        public async Task<ListResponse<ProductDescriptionDataModel[]>> Search(
            ProductDescriptionAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ProductDescriptionDataModel>();
                return new ListResponse<ProductDescriptionDataModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<ProductDescriptionDataModel[]>>.FromResult(new ListResponse<ProductDescriptionDataModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<ProductDescription> GetIQueryableByPrimaryIdentifierList(
            List<ProductDescriptionIdentifier> ids)
        {
            var idList = ids.Select(t => t.ProductDescriptionID).ToList();
            var queryable =
                from t in _dbcontext.ProductDescription
                where idList.Contains(t.ProductDescriptionID)
                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<ProductDescriptionIdentifier> ids)
        {
            try
            {
                var queryable = GetIQueryableByPrimaryIdentifierList(ids);
                var result = await queryable.BatchDeleteAsync();

                return await Task<Response>.FromResult(
                    new Response
                    {
                        Status = HttpStatusCode.OK,
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<ProductDescription> newEFItems = new();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new ProductDescription
                        {
                            Description = item.Description,
                            ModifiedDate = item.ModifiedDate,
                        };
                        _dbcontext.ProductDescription.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.ProductDescription
                             where

                             t.ProductDescriptionID == item.ProductDescriptionID
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                            existing.Description = item.Description;
                            existing.ModifiedDate = item.ModifiedDate;
                        }
                    }
                }
                await _dbcontext.SaveChangesAsync();

                // 3.2 Load Response
                var identifierListToloadResponseItems = new List<int>();

                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in newEFItems
                        select t.ProductDescriptionID);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.ProductDescriptionID);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.ProductDescription
                    where identifierListToloadResponseItems.Contains(t.ProductDescriptionID)

                    select new ProductDescriptionDataModel
                    {

                        ProductDescriptionID = t.ProductDescriptionID,
                        Description = t.Description,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.ProductDescriptionID == t.ProductDescriptionID)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.ProductDescriptionID == t.ProductDescriptionID)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<ProductDescriptionDataModel>> Update(ProductDescriptionIdentifier id, ProductDescriptionDataModel input)
        {
            if (input == null)
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ProductDescription
                     where

                    t.ProductDescriptionID == id.ProductDescriptionID
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Description = input.Description;
                existing.ModifiedDate = input.ModifiedDate;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ProductDescriptionDataModel>>.FromResult(
                    new Response<ProductDescriptionDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ProductDescriptionDataModel
                        {
                    ProductDescriptionID = existing.ProductDescriptionID,
                    Description = existing.Description,
                    rowguid = existing.rowguid,
                    ModifiedDate = existing.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ProductDescriptionDataModel>> Get(ProductDescriptionIdentifier id)
        {
            if (id == null)
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ProductDescription.SingleOrDefault(
                    t =>

                    t.ProductDescriptionID == id.ProductDescriptionID
                );

                if (existing == null)
                    return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ProductDescriptionDataModel>>.FromResult(
                    new Response<ProductDescriptionDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ProductDescriptionDataModel
                        {
                    ProductDescriptionID = existing.ProductDescriptionID,
                    Description = existing.Description,
                    rowguid = existing.rowguid,
                    ModifiedDate = existing.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ProductDescriptionDataModel>> Create(ProductDescriptionDataModel input)
        {
            if (input == null)
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ProductDescription
                {
                            Description = input.Description,
                            ModifiedDate = input.ModifiedDate,
                };
                await _dbcontext.ProductDescription.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ProductDescriptionDataModel>>.FromResult(
                    new Response<ProductDescriptionDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ProductDescriptionDataModel
                        {
                    ProductDescriptionID = toInsert.ProductDescriptionID,
                    Description = toInsert.Description,
                    rowguid = toInsert.rowguid,
                    ModifiedDate = toInsert.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ProductDescriptionIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ProductDescription
                     where

                    t.ProductDescriptionID == id.ProductDescriptionID
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ProductDescription.Remove(existing);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response>.FromResult(
                    new Response
                    {
                        Status = HttpStatusCode.OK,
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private IQueryable<NameValuePair> GetCodeListQuery(
            ProductDescriptionAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ProductDescription

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Description!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Description!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Description!, "%" + query.TextSearch)))
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Description) ||
                            query.DescriptionSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Description!, "%" + query.Description + "%") ||
                            query.DescriptionSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Description!, query.Description + "%") ||
                            query.DescriptionSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Description!, "%" + query.Description))

                select new NameValuePair
                {

                        Value = t.ProductDescriptionID.ToString(),
                        Name = t.Description,
                };

            // 1. Without Paging And OrderBy
            if (!withPagingAndOrderBy)
                return queryable;

            // 2. With Paging And OrderBy
            var orderBys = QueryOrderBySetting.Parse(query.OrderBys);
            if (orderBys.Any())
            {
                queryable = queryable.OrderBy(QueryOrderBySetting.GetOrderByExpression(orderBys));
            }

            queryable = queryable.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);

            return queryable;
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductDescriptionAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = GetCodeListQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = GetCodeListQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<NameValuePair>();
                return new ListResponse<NameValuePair[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<NameValuePair[]>>.FromResult(new ListResponse<NameValuePair[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        public async Task<Response<ProductDescriptionDataModel>> CreateComposite(ProductDescriptionCompositeModel input)
        {
            if (input == null)
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                // 1. Master: ProductDescription
                var master = new ProductDescription
                {
                    // Properties.1. Value Type Properties
                    Description = input.__Master__!.Description,
                    rowguid = input.__Master__!.rowguid,
                    ModifiedDate = input.__Master__!.ModifiedDate,
                };
                // 2.1.1. ListTable ProductDescription.ProductModelProductDescription
                if(input.ProductModelProductDescriptions_Via_ProductDescriptionID != null)
                {
                    foreach(var item in input.ProductModelProductDescriptions_Via_ProductDescriptionID)
                    {
                        master.ProductModelProductDescription.Add(new ProductModelProductDescription
                        {
                                Culture = item.Culture,
                                rowguid = item.rowguid,
                                ModifiedDate = item.ModifiedDate,
                        });
                    }
                }

                _dbcontext.ProductDescription.Add(master);

                await _dbcontext.SaveChangesAsync();

                return await Get(new ProductDescriptionIdentifier { ProductDescriptionID = master.ProductDescriptionID, });
            }
            catch (Exception ex)
            {
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

    }
}

