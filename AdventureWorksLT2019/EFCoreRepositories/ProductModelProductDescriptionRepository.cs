using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.EFCoreContext;
using AdventureWorksLT2019.Models;
using Framework.Helpers;
using Framework.Common;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLT2019.EFCoreRepositories
{
    public class ProductModelProductDescriptionRepository
        : IProductModelProductDescriptionRepository
    {
        private readonly ILogger<ProductModelProductDescriptionRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ProductModelProductDescriptionRepository(EFDbContext dbcontext, ILogger<ProductModelProductDescriptionRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<ProductModelProductDescriptionDataModel.DefaultView> SearchQuery(
            ProductModelProductDescriptionAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ProductModelProductDescription

                    join ProductDescription in _dbcontext.ProductDescription on t.ProductDescriptionID equals ProductDescription.ProductDescriptionID// \ProductDescriptionID
                    join ProductModel in _dbcontext.ProductModel on t.ProductModelID equals ProductModel.ProductModelID// \ProductModelID
                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Culture!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Culture!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Culture!, "%" + query.TextSearch)))
                    &&

                    (!query.ProductDescriptionID.HasValue || ProductDescription.ProductDescriptionID == query.ProductDescriptionID)
                    &&
                    (!query.ProductModelID.HasValue || ProductModel.ProductModelID == query.ProductModelID)
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Culture) ||
                            query.CultureSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Culture!, "%" + query.Culture + "%") ||
                            query.CultureSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Culture!, query.Culture + "%") ||
                            query.CultureSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Culture!, "%" + query.Culture))

                select new ProductModelProductDescriptionDataModel.DefaultView
                {

                        ProductModelID = t.ProductModelID,
                        ProductDescriptionID = t.ProductDescriptionID,
                        Culture = t.Culture,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        ProductDescription_Name = ProductDescription.Description,
                        ProductModel_Name = ProductModel.Name,
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

        public async Task<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>> Search(
            ProductModelProductDescriptionAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ProductModelProductDescriptionDataModel.DefaultView>();
                return new ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>>.FromResult(new ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<ProductModelProductDescription> GetIQueryableByPrimaryIdentifierList(
            List<ProductModelProductDescriptionIdentifier> ids)
        {
            var queryable =
                from t in _dbcontext.ProductModelProductDescription

                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<ProductModelProductDescriptionIdentifier> ids)
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

        public async Task<Response<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<ProductModelProductDescription> newEFItems = new List<ProductModelProductDescription>();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new ProductModelProductDescription
                        {
                            ProductModelID = item.ProductModelID,
                            ProductDescriptionID = item.ProductDescriptionID,
                            Culture = item.Culture,
                            ModifiedDate = item.ModifiedDate,
                        };
                        _dbcontext.ProductModelProductDescription.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.ProductModelProductDescription
                             where

                             t.ProductModelID == item.ProductModelID
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.ProductModelID = item.ProductModelID;
                existing.ProductDescriptionID = item.ProductDescriptionID;
                existing.Culture = item.Culture;
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
                        select t.ProductModelID);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.ProductModelID);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.ProductModelProductDescription
                    join ProductDescription in _dbcontext.ProductDescription on t.ProductDescriptionID equals ProductDescription.ProductDescriptionID// \ProductDescriptionID
                    join ProductModel in _dbcontext.ProductModel on t.ProductModelID equals ProductModel.ProductModelID// \ProductModelID
                    where identifierListToloadResponseItems.Contains(t.ProductModelID)

                    select new ProductModelProductDescriptionDataModel.DefaultView
                    {

                        ProductModelID = t.ProductModelID,
                        ProductDescriptionID = t.ProductDescriptionID,
                        Culture = t.Culture,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        ProductDescription_Name = ProductDescription.Description,
                        ProductModel_Name = ProductModel.Name,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.ProductModelID == t.ProductModelID)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.ProductModelID == t.ProductModelID)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Update(ProductModelProductDescriptionIdentifier id, ProductModelProductDescriptionDataModel input)
        {
            if (input == null)
                return await Task<Response<ProductModelProductDescriptionDataModel.DefaultView>>.FromResult(new Response<ProductModelProductDescriptionDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ProductModelProductDescription
                     where

                    t.ProductModelID == id.ProductModelID
                    &&
                    t.ProductDescriptionID == id.ProductDescriptionID
                    &&
                    t.Culture == id.Culture
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ProductModelProductDescriptionDataModel.DefaultView>>.FromResult(new Response<ProductModelProductDescriptionDataModel.DefaultView> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.ProductModelID = input.ProductModelID;
                existing.ProductDescriptionID = input.ProductDescriptionID;
                existing.Culture = input.Culture;
                existing.ModifiedDate = input.ModifiedDate;
                await _dbcontext.SaveChangesAsync();

                var responseBody =
                    (
                    from t in _dbcontext.ProductModelProductDescription

                    join ProductDescription in _dbcontext.ProductDescription on t.ProductDescriptionID equals ProductDescription.ProductDescriptionID// \ProductDescriptionID
                    join ProductModel in _dbcontext.ProductModel on t.ProductModelID equals ProductModel.ProductModelID// \ProductModelID
                    where t.ProductModelID == existing.ProductModelID && t.ProductDescriptionID == existing.ProductDescriptionID && t.Culture == existing.Culture

                    select new ProductModelProductDescriptionDataModel.DefaultView
                    {

                        ProductModelID = t.ProductModelID,
                        ProductDescriptionID = t.ProductDescriptionID,
                        Culture = t.Culture,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        ProductDescription_Name = ProductDescription.Description,
                        ProductModel_Name = ProductModel.Name,

                    }).First();

                return await Task<Response<ProductModelProductDescriptionDataModel.DefaultView>>.FromResult(
                    new Response<ProductModelProductDescriptionDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductModelProductDescriptionDataModel.DefaultView>>.FromResult(new Response<ProductModelProductDescriptionDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Get(ProductModelProductDescriptionIdentifier id)
        {
            if (id == null)
                return await Task<Response<ProductModelProductDescriptionDataModel.DefaultView>>.FromResult(new Response<ProductModelProductDescriptionDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {

                var responseBody =
                    (
                    from t in _dbcontext.ProductModelProductDescription

                    join ProductDescription in _dbcontext.ProductDescription on t.ProductDescriptionID equals ProductDescription.ProductDescriptionID// \ProductDescriptionID
                    join ProductModel in _dbcontext.ProductModel on t.ProductModelID equals ProductModel.ProductModelID// \ProductModelID
                    where

                    t.ProductModelID == id.ProductModelID
                    &&
                    t.ProductDescriptionID == id.ProductDescriptionID
                    &&
                    t.Culture == id.Culture

                    select new ProductModelProductDescriptionDataModel.DefaultView
                    {

                        ProductModelID = t.ProductModelID,
                        ProductDescriptionID = t.ProductDescriptionID,
                        Culture = t.Culture,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        ProductDescription_Name = ProductDescription.Description,
                        ProductModel_Name = ProductModel.Name,

                    }).First();
                if (responseBody == null)
                    return await Task<Response<ProductModelProductDescriptionDataModel.DefaultView>>.FromResult(new Response<ProductModelProductDescriptionDataModel.DefaultView> { Status = HttpStatusCode.NotFound });
                return await Task<Response<ProductModelProductDescriptionDataModel.DefaultView>>.FromResult(
                    new Response<ProductModelProductDescriptionDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductModelProductDescriptionDataModel.DefaultView>>.FromResult(new Response<ProductModelProductDescriptionDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Create(ProductModelProductDescriptionDataModel input)
        {
            if (input == null)
                return await Task<Response<ProductModelProductDescriptionDataModel.DefaultView>>.FromResult(new Response<ProductModelProductDescriptionDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ProductModelProductDescription
                {
                            ProductModelID = input.ProductModelID,
                            ProductDescriptionID = input.ProductDescriptionID,
                            Culture = input.Culture,
                            ModifiedDate = input.ModifiedDate,
                };
                await _dbcontext.ProductModelProductDescription.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                var responseBody =
                    (
                    from t in _dbcontext.ProductModelProductDescription

                    join ProductDescription in _dbcontext.ProductDescription on t.ProductDescriptionID equals ProductDescription.ProductDescriptionID// \ProductDescriptionID
                    join ProductModel in _dbcontext.ProductModel on t.ProductModelID equals ProductModel.ProductModelID// \ProductModelID
                    where t.ProductModelID == toInsert.ProductModelID && t.ProductDescriptionID == toInsert.ProductDescriptionID && t.Culture == toInsert.Culture

                    select new ProductModelProductDescriptionDataModel.DefaultView
                    {

                        ProductModelID = t.ProductModelID,
                        ProductDescriptionID = t.ProductDescriptionID,
                        Culture = t.Culture,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        ProductDescription_Name = ProductDescription.Description,
                        ProductModel_Name = ProductModel.Name,

                    }).First();

                return await Task<Response<ProductModelProductDescriptionDataModel.DefaultView>>.FromResult(
                    new Response<ProductModelProductDescriptionDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductModelProductDescriptionDataModel.DefaultView>>.FromResult(new Response<ProductModelProductDescriptionDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ProductModelProductDescriptionIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ProductModelProductDescription
                     where

                    t.ProductModelID == id.ProductModelID
                    &&
                    t.ProductDescriptionID == id.ProductDescriptionID
                    &&
                    t.Culture == id.Culture
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ProductModelProductDescription.Remove(existing);
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
            ProductModelProductDescriptionAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ProductModelProductDescription

                    join ProductDescription in _dbcontext.ProductDescription on t.ProductDescriptionID equals ProductDescription.ProductDescriptionID// \ProductDescriptionID
                    join ProductModel in _dbcontext.ProductModel on t.ProductModelID equals ProductModel.ProductModelID// \ProductModelID
                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Culture!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Culture!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Culture!, "%" + query.TextSearch)))
                    &&

                    (!query.ProductDescriptionID.HasValue || ProductDescription.ProductDescriptionID == query.ProductDescriptionID)
                    &&
                    (!query.ProductModelID.HasValue || ProductModel.ProductModelID == query.ProductModelID)
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Culture) ||
                            query.CultureSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Culture!, "%" + query.Culture + "%") ||
                            query.CultureSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Culture!, query.Culture + "%") ||
                            query.CultureSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Culture!, "%" + query.Culture))
                let _Value = string.Concat(new string[] { t.ProductModelID.ToString(),"|",t.ProductDescriptionID.ToString(),"|",t.Culture! })
                select new NameValuePair
                {

                        Name = t.Culture,
                        Value = _Value,
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
            ProductModelProductDescriptionAdvancedQuery query)
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

    }
}

