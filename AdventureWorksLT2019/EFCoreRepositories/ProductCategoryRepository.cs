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
    public class ProductCategoryRepository
        : IProductCategoryRepository
    {
        private readonly Dictionary<string, string> _queryOrderBys = new()
        {
        };

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

                    join Parent_A in _dbcontext.ProductCategory on t.ParentProductCategoryID equals Parent_A.ProductCategoryID into Parent_G from Parent in Parent_G.DefaultIfEmpty()// \ParentProductCategoryID
                where
                    (string.IsNullOrEmpty(query.TextSearch) ||
                    query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Name!, "%" + query.TextSearch + "%")) ||
                    query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Name!, query.TextSearch + "%")) ||
                    query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Name!, "%" + query.TextSearch)))&&
                    (!query.ParentProductCategoryID.HasValue || Parent.ProductCategoryID == query.ParentProductCategoryID)&&
                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))&&
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

            // // 2. With Paging And OrderBy
            // var orderBys = QueryOrderBySetting.Parse(query.OrderBys);
            // if (orderBys.Any())
            // {
            //     queryable = queryable.OrderBy(QueryOrderBySetting.GetOrderByExpression(orderBys));
            // }

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

        public async Task<Response<ProductCategoryDataModel.DefaultView>> Update(ProductCategoryIdentifier id, ProductCategoryDataModel.DefaultView input, string[]? toUpdatePropertyList = null)
        {
            if (input == null)
                return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(new Response<ProductCategoryDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (
                    from t in _dbcontext.ProductCategory
                     where
                         id.ProductCategoryID.HasValue && t.ProductCategoryID == id.ProductCategoryID
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(new Response<ProductCategoryDataModel.DefaultView> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                CopyUpdateValues(input, toUpdatePropertyList, existing);

                await _dbcontext.SaveChangesAsync();
                return await Get(id);

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(new Response<ProductCategoryDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private static void CopyUpdateValues(ProductCategoryDataModel.DefaultView? input, string[]? toUpdatePropertyList, ProductCategory existing)
        {
            if (input == null)
                return;

            // 1. This Table - ProductCategory
            if (toUpdatePropertyList == null || toUpdatePropertyList.Length == 0)
            {
                existing.ParentProductCategoryID = input.ParentProductCategoryID;
                existing.Name = input.Name;
                existing.ModifiedDate = input.ModifiedDate;
            }
            else
            //update Specific Properties if in toUpdatePropertyList
            {
                if(toUpdatePropertyList.Contains(nameof(ProductCategoryDataModel.ParentProductCategoryID)))
                    existing.ParentProductCategoryID = input.ParentProductCategoryID;
                if(toUpdatePropertyList.Contains(nameof(ProductCategoryDataModel.Name)))
                    existing.Name = input.Name;
                if(toUpdatePropertyList.Contains(nameof(ProductCategoryDataModel.ModifiedDate)))
                    existing.ModifiedDate = input.ModifiedDate;
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

                    join Parent_A in _dbcontext.ProductCategory on t.ParentProductCategoryID equals Parent_A.ProductCategoryID into Parent_G from Parent in Parent_G.DefaultIfEmpty()// \ParentProductCategoryID
                    where
                        id.ProductCategoryID.HasValue && t.ProductCategoryID == id.ProductCategoryID

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

        public async Task<Response<ProductCategoryDataModel.DefaultView>> Create(ProductCategoryDataModel.DefaultView input)
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
                return await Get(new ProductCategoryIdentifier { ProductCategoryID = toInsert.ProductCategoryID });
            }
            catch (Exception ex)
            {
                return await Task<Response<ProductCategoryDataModel.DefaultView>>.FromResult(new Response<ProductCategoryDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private IQueryable<NameValuePair> GetCodeListQuery(
            ProductCategoryAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var queryable =
                from t in _dbcontext.ProductCategory

                    join Parent_A in _dbcontext.ProductCategory on t.ParentProductCategoryID equals Parent_A.ProductCategoryID into Parent_G from Parent in Parent_G.DefaultIfEmpty()// \ParentProductCategoryID
                where
                    (string.IsNullOrEmpty(query.TextSearch) ||
                    query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Name!, "%" + query.TextSearch + "%")) ||
                    query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Name!, query.TextSearch + "%")) ||
                    query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Name!, "%" + query.TextSearch)))&&
                    (!query.ParentProductCategoryID.HasValue || Parent.ProductCategoryID == query.ParentProductCategoryID)&&
                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))&&
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

            // // 2. With Paging And OrderBy
            // var orderBys = QueryOrderBySetting.Parse(query.OrderBys);
            // if (orderBys.Any())
            // {
            //     queryable = queryable.OrderBy(QueryOrderBySetting.GetOrderByExpression(orderBys));
            // }

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

