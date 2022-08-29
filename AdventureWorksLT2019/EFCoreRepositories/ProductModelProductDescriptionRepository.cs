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

    }
}

