using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.EFCoreContext;
using AdventureWorksLT2019.Models;
using Framework.Helpers;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLT2019.EFCoreRepositories
{
    public class ProductCategoryRepository
        : IProductCategoryRepository
    {
        private readonly ILogger<ProductCategoryRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ProductCategoryRepository(EFDbContext dbcontext, ILogger<ProductCategoryRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<ProductCategoryDataModel.DefaultView> SearchQuery(
            ProductCategoryAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ProductCategory

                    join Parent in _dbcontext.ProductCategory on t.ParentProductCategoryID equals Parent.ProductCategoryID// \ParentProductCategoryID
                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Name!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Name!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Name!, "%" + query.TextSearch)))
                    &&

                    (!query.ParentProductCategoryID.HasValue || Parent.ProductCategoryID == query.ParentProductCategoryID)
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Name) ||
                            query.NameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Name!, "%" + query.Name + "%") ||
                            query.NameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Name!, query.Name + "%") ||
                            query.NameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Name!, "%" + query.Name))

                select new ProductCategoryDataModel.DefaultView
                {

                        ProductCategoryID = t.ProductCategoryID,
                        ParentProductCategoryID = t.ParentProductCategoryID,
                        Name = t.Name,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        Parent_Name = Parent.Name,
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

        public async Task<ListResponse<ProductCategoryDataModel.DefaultView[]>> Search(
            ProductCategoryAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ProductCategoryDataModel.DefaultView>();
                return new ListResponse<ProductCategoryDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<ProductCategoryDataModel.DefaultView[]>>.FromResult(new ListResponse<ProductCategoryDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<ProductCategory> GetIQueryableByPrimaryIdentifierList(
            List<ProductCategoryIdentifier> ids)
        {
            var idList = ids.Select(t => t.ProductCategoryID).ToList();
            var queryable =
                from t in _dbcontext.ProductCategory
                where idList.Contains(t.ProductCategoryID)
                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<ProductCategoryIdentifier> ids)
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

        public async Task<Response<MultiItemsCUDRequest<ProductCategoryIdentifier, ProductCategoryDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductCategoryIdentifier, ProductCategoryDataModel.DefaultView> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDRequest<ProductCategoryIdentifier, ProductCategoryDataModel.DefaultView>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDRequest<ProductCategoryIdentifier, ProductCategoryDataModel.DefaultView>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<ProductCategory> newEFItems = new List<ProductCategory>();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new ProductCategory
                        {
                            ParentProductCategoryID = item.ParentProductCategoryID,
                            Name = item.Name,
                            ModifiedDate = item.ModifiedDate,
                        };
                        _dbcontext.ProductCategory.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.ProductCategory
                             where

                             t.ProductCategoryID == item.ProductCategoryID
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.ParentProductCategoryID = item.ParentProductCategoryID;
                existing.Name = item.Name;
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
                        select t.ProductCategoryID);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.ProductCategoryID);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.ProductCategory
                    join Parent in _dbcontext.ProductCategory on t.ParentProductCategoryID equals Parent.ProductCategoryID// \ParentProductCategoryID
                    where identifierListToloadResponseItems.Contains(t.ProductCategoryID)

                    select new ProductCategoryDataModel.DefaultView
                    {

                        ProductCategoryID = t.ProductCategoryID,
                        ParentProductCategoryID = t.ParentProductCategoryID,
                        Name = t.Name,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        Parent_Name = Parent.Name,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDRequest<ProductCategoryIdentifier, ProductCategoryDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDRequest<ProductCategoryIdentifier, ProductCategoryDataModel.DefaultView>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.ProductCategoryID == t.ProductCategoryID)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.ProductCategoryID == t.ProductCategoryID)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDRequest<ProductCategoryIdentifier, ProductCategoryDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<ProductCategoryDataModel.DefaultView>> Update(ProductCategoryIdentifier id, ProductCategoryDataModel input)
        {
            if (input == null)
                return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(new Response<ProductCategoryDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ProductCategory
                     where

                    t.ProductCategoryID == id.ProductCategoryID
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(new Response<ProductCategoryDataModel.DefaultView> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.ParentProductCategoryID = input.ParentProductCategoryID;
                existing.Name = input.Name;
                existing.ModifiedDate = input.ModifiedDate;
                await _dbcontext.SaveChangesAsync();

                var responseBody =
                    (
                    from t in _dbcontext.ProductCategory

                    join Parent in _dbcontext.ProductCategory on t.ParentProductCategoryID equals Parent.ProductCategoryID// \ParentProductCategoryID
                    where t.ProductCategoryID == existing.ProductCategoryID

                    select new ProductCategoryDataModel.DefaultView
                    {

                        ProductCategoryID = t.ProductCategoryID,
                        ParentProductCategoryID = t.ParentProductCategoryID,
                        Name = t.Name,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        Parent_Name = Parent.Name,

                    }).First();

                return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(
                    new Response<ProductCategoryDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(new Response<ProductCategoryDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ProductCategoryDataModel.DefaultView>> Get(ProductCategoryIdentifier id)
        {
            if (id == null)
                return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(new Response<ProductCategoryDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {

                var responseBody =
                    (
                    from t in _dbcontext.ProductCategory

                    join Parent in _dbcontext.ProductCategory on t.ParentProductCategoryID equals Parent.ProductCategoryID// \ParentProductCategoryID
                    where

                    t.ProductCategoryID == id.ProductCategoryID

                    select new ProductCategoryDataModel.DefaultView
                    {

                        ProductCategoryID = t.ProductCategoryID,
                        ParentProductCategoryID = t.ParentProductCategoryID,
                        Name = t.Name,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        Parent_Name = Parent.Name,

                    }).First();
                if (responseBody == null)
                    return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(new Response<ProductCategoryDataModel.DefaultView> { Status = HttpStatusCode.NotFound });
                return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(
                    new Response<ProductCategoryDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(new Response<ProductCategoryDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ProductCategoryDataModel.DefaultView>> Create(ProductCategoryDataModel input)
        {
            if (input == null)
                return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(new Response<ProductCategoryDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ProductCategory
                {
                            ParentProductCategoryID = input.ParentProductCategoryID,
                            Name = input.Name,
                            ModifiedDate = input.ModifiedDate,
                };
                await _dbcontext.ProductCategory.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                var responseBody =
                    (
                    from t in _dbcontext.ProductCategory

                    join Parent in _dbcontext.ProductCategory on t.ParentProductCategoryID equals Parent.ProductCategoryID// \ParentProductCategoryID
                    where t.ProductCategoryID == toInsert.ProductCategoryID

                    select new ProductCategoryDataModel.DefaultView
                    {

                        ProductCategoryID = t.ProductCategoryID,
                        ParentProductCategoryID = t.ParentProductCategoryID,
                        Name = t.Name,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        Parent_Name = Parent.Name,

                    }).First();

                return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(
                    new Response<ProductCategoryDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(new Response<ProductCategoryDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ProductCategoryIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ProductCategory
                     where

                    t.ProductCategoryID == id.ProductCategoryID
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ProductCategory.Remove(existing);
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
            ProductCategoryAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ProductCategory

                    join Parent in _dbcontext.ProductCategory on t.ParentProductCategoryID equals Parent.ProductCategoryID// \ParentProductCategoryID
                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Name!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Name!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Name!, "%" + query.TextSearch)))
                    &&

                    (!query.ParentProductCategoryID.HasValue || Parent.ProductCategoryID == query.ParentProductCategoryID)
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Name) ||
                            query.NameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Name!, "%" + query.Name + "%") ||
                            query.NameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Name!, query.Name + "%") ||
                            query.NameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Name!, "%" + query.Name))

                select new NameValuePair
                {

                        Name = t.Name,
                        Value = t.ProductCategoryID.ToString(),
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
            ProductCategoryAdvancedQuery query)
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

