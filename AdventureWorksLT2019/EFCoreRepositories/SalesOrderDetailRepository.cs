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
    public class SalesOrderDetailRepository
        : ISalesOrderDetailRepository
    {
        private readonly ILogger<SalesOrderDetailRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public SalesOrderDetailRepository(EFDbContext dbcontext, ILogger<SalesOrderDetailRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<SalesOrderDetailDataModel.DefaultView> SearchQuery(
            SalesOrderDetailAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.SalesOrderDetail

                    join Product in _dbcontext.Product on t.ProductID equals Product.ProductID// \ProductID
                    join ProductCategory in _dbcontext.ProductCategory on Product.ProductCategoryID equals ProductCategory.ProductCategoryID// \ProductID\ProductCategoryID
                    join ProductCategory_Parent in _dbcontext.ProductCategory on ProductCategory.ParentProductCategoryID equals ProductCategory_Parent.ProductCategoryID// \ProductID\ProductCategoryID\ParentProductCategoryID
                    join ProductModel in _dbcontext.ProductModel on Product.ProductModelID equals ProductModel.ProductModelID// \ProductID\ProductModelID
                    join SalesOrderHeader in _dbcontext.SalesOrderHeader on t.SalesOrderID equals SalesOrderHeader.SalesOrderID// \SalesOrderID
                    join BillTo in _dbcontext.Address on SalesOrderHeader.BillToAddressID equals BillTo.AddressID// \SalesOrderID\BillToAddressID
                    join ShipTo in _dbcontext.Address on SalesOrderHeader.ShipToAddressID equals ShipTo.AddressID// \SalesOrderID\ShipToAddressID
                    join Customer in _dbcontext.Customer on SalesOrderHeader.CustomerID equals Customer.CustomerID// \SalesOrderID\CustomerID
                where

                    (!query.ProductID.HasValue || Product.ProductID == query.ProductID)
                    &&
                    (!query.ProductCategoryID.HasValue || ProductCategory.ProductCategoryID == query.ProductCategoryID)
                    &&
                    (!query.ProductCategory_ParentID.HasValue || ProductCategory_Parent.ProductCategoryID == query.ProductCategory_ParentID)
                    &&
                    (!query.ProductModelID.HasValue || ProductModel.ProductModelID == query.ProductModelID)
                    &&
                    (!query.SalesOrderID.HasValue || SalesOrderHeader.SalesOrderID == query.SalesOrderID)
                    &&
                    (!query.BillToID.HasValue || BillTo.AddressID == query.BillToID)
                    &&
                    (!query.ShipToID.HasValue || ShipTo.AddressID == query.ShipToID)
                    &&
                    (!query.CustomerID.HasValue || Customer.CustomerID == query.CustomerID)
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))

                select new SalesOrderDetailDataModel.DefaultView
                {

                        SalesOrderID = t.SalesOrderID,
                        SalesOrderDetailID = t.SalesOrderDetailID,
                        OrderQty = t.OrderQty,
                        ProductID = t.ProductID,
                        UnitPrice = t.UnitPrice,
                        UnitPriceDiscount = t.UnitPriceDiscount,
                        LineTotal = t.LineTotal,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        Product_Name = Product.Name,
                        SalesOrderHeader_Name = SalesOrderHeader.SalesOrderNumber,
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

        public async Task<PagedResponse<SalesOrderDetailDataModel.DefaultView[]>> Search(
            SalesOrderDetailAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<SalesOrderDetailDataModel.DefaultView>();
                return new PagedResponse<SalesOrderDetailDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<SalesOrderDetailDataModel.DefaultView[]>>.FromResult(new PagedResponse<SalesOrderDetailDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        public async Task<Response<SalesOrderDetailDataModel.DefaultView>> Get(SalesOrderDetailIdentifier id)
        {
            if (id == null)
                return await Task<Response<SalesOrderDetailDataModel.DefaultView>>.FromResult(new Response<SalesOrderDetailDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {

                var responseBody =
                    (
                    from t in _dbcontext.SalesOrderDetail

                    join Product in _dbcontext.Product on t.ProductID equals Product.ProductID// \ProductID
                    join SalesOrderHeader in _dbcontext.SalesOrderHeader on t.SalesOrderID equals SalesOrderHeader.SalesOrderID// \SalesOrderID
                    where

                    t.SalesOrderID == id.SalesOrderID
                    &&
                    t.SalesOrderDetailID == id.SalesOrderDetailID

                    select new SalesOrderDetailDataModel.DefaultView
                    {

                        SalesOrderID = t.SalesOrderID,
                        SalesOrderDetailID = t.SalesOrderDetailID,
                        OrderQty = t.OrderQty,
                        ProductID = t.ProductID,
                        UnitPrice = t.UnitPrice,
                        UnitPriceDiscount = t.UnitPriceDiscount,
                        LineTotal = t.LineTotal,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        Product_Name = Product.Name,
                        SalesOrderHeader_Name = SalesOrderHeader.SalesOrderNumber,

                    }).First();
                if (responseBody == null)
                    return await Task<Response<SalesOrderDetailDataModel.DefaultView>>.FromResult(new Response<SalesOrderDetailDataModel.DefaultView> { Status = HttpStatusCode.NotFound });
                return await Task<Response<SalesOrderDetailDataModel.DefaultView>>.FromResult(
                    new Response<SalesOrderDetailDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<SalesOrderDetailDataModel.DefaultView>>.FromResult(new Response<SalesOrderDetailDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

    }
}

