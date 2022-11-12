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
                    join ProductCategory_A in _dbcontext.ProductCategory on Product.ProductCategoryID equals ProductCategory_A.ProductCategoryID into ProductCategory_G from ProductCategory in ProductCategory_G.DefaultIfEmpty()// \ProductID\ProductCategoryID
                    join ProductCategory_Parent_A in _dbcontext.ProductCategory on ProductCategory.ParentProductCategoryID equals ProductCategory_Parent_A.ProductCategoryID into ProductCategory_Parent_G from ProductCategory_Parent in ProductCategory_Parent_G.DefaultIfEmpty()// \ProductID\ProductCategoryID\ParentProductCategoryID
                    join ProductModel_A in _dbcontext.ProductModel on Product.ProductModelID equals ProductModel_A.ProductModelID into ProductModel_G from ProductModel in ProductModel_G.DefaultIfEmpty()// \ProductID\ProductModelID
                    join SalesOrderHeader in _dbcontext.SalesOrderHeader on t.SalesOrderID equals SalesOrderHeader.SalesOrderID// \SalesOrderID
                    join BillTo_A in _dbcontext.Address on SalesOrderHeader.BillToAddressID equals BillTo_A.AddressID into BillTo_G from BillTo in BillTo_G.DefaultIfEmpty()// \SalesOrderID\BillToAddressID
                    join ShipTo_A in _dbcontext.Address on SalesOrderHeader.ShipToAddressID equals ShipTo_A.AddressID into ShipTo_G from ShipTo in ShipTo_G.DefaultIfEmpty()// \SalesOrderID\ShipToAddressID
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

        public async Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>> Search(
            SalesOrderDetailAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<SalesOrderDetailDataModel.DefaultView>();
                return new ListResponse<SalesOrderDetailDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>>.FromResult(new ListResponse<SalesOrderDetailDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        public async Task<Response<SalesOrderDetailDataModel.DefaultView>> Update(SalesOrderDetailIdentifier id, SalesOrderDetailDataModel input)
        {
            if (input == null)
                return await Task<Response<SalesOrderDetailDataModel.DefaultView>>.FromResult(new Response<SalesOrderDetailDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.SalesOrderDetail
                     where

                    t.SalesOrderID == id.SalesOrderID
                    &&
                    t.SalesOrderDetailID == id.SalesOrderDetailID
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<SalesOrderDetailDataModel.DefaultView>>.FromResult(new Response<SalesOrderDetailDataModel.DefaultView> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.SalesOrderID = input.SalesOrderID;
                existing.OrderQty = input.OrderQty;
                existing.ProductID = input.ProductID;
                existing.UnitPrice = input.UnitPrice;
                existing.UnitPriceDiscount = input.UnitPriceDiscount;
                existing.ModifiedDate = input.ModifiedDate;
                await _dbcontext.SaveChangesAsync();

                var responseBody =
                    (
                    from t in _dbcontext.SalesOrderDetail

                    join Product in _dbcontext.Product on t.ProductID equals Product.ProductID// \ProductID
                    join SalesOrderHeader in _dbcontext.SalesOrderHeader on t.SalesOrderID equals SalesOrderHeader.SalesOrderID// \SalesOrderID
                    where t.SalesOrderID == existing.SalesOrderID && t.SalesOrderDetailID == existing.SalesOrderDetailID

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

        public async Task<Response<SalesOrderDetailDataModel.DefaultView>> Create(SalesOrderDetailDataModel input)
        {
            if (input == null)
                return await Task<Response<SalesOrderDetailDataModel.DefaultView>>.FromResult(new Response<SalesOrderDetailDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new SalesOrderDetail
                {
                            SalesOrderID = input.SalesOrderID,
                            OrderQty = input.OrderQty,
                            ProductID = input.ProductID,
                            UnitPrice = input.UnitPrice,
                            UnitPriceDiscount = input.UnitPriceDiscount,
                            ModifiedDate = input.ModifiedDate,
                };
                await _dbcontext.SalesOrderDetail.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                var responseBody =
                    (
                    from t in _dbcontext.SalesOrderDetail

                    join Product in _dbcontext.Product on t.ProductID equals Product.ProductID// \ProductID
                    join SalesOrderHeader in _dbcontext.SalesOrderHeader on t.SalesOrderID equals SalesOrderHeader.SalesOrderID// \SalesOrderID
                    where t.SalesOrderID == toInsert.SalesOrderID && t.SalesOrderDetailID == toInsert.SalesOrderDetailID

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

        private IQueryable<NameValuePair> GetCodeListQuery(
            SalesOrderDetailAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.SalesOrderDetail

                    join Product in _dbcontext.Product on t.ProductID equals Product.ProductID// \ProductID
                    join ProductCategory_A in _dbcontext.ProductCategory on Product.ProductCategoryID equals ProductCategory_A.ProductCategoryID into ProductCategory_G from ProductCategory in ProductCategory_G.DefaultIfEmpty()// \ProductID\ProductCategoryID
                    join ProductCategory_Parent_A in _dbcontext.ProductCategory on ProductCategory.ParentProductCategoryID equals ProductCategory_Parent_A.ProductCategoryID into ProductCategory_Parent_G from ProductCategory_Parent in ProductCategory_Parent_G.DefaultIfEmpty()// \ProductID\ProductCategoryID\ParentProductCategoryID
                    join ProductModel_A in _dbcontext.ProductModel on Product.ProductModelID equals ProductModel_A.ProductModelID into ProductModel_G from ProductModel in ProductModel_G.DefaultIfEmpty()// \ProductID\ProductModelID
                    join SalesOrderHeader in _dbcontext.SalesOrderHeader on t.SalesOrderID equals SalesOrderHeader.SalesOrderID// \SalesOrderID
                    join BillTo_A in _dbcontext.Address on SalesOrderHeader.BillToAddressID equals BillTo_A.AddressID into BillTo_G from BillTo in BillTo_G.DefaultIfEmpty()// \SalesOrderID\BillToAddressID
                    join ShipTo_A in _dbcontext.Address on SalesOrderHeader.ShipToAddressID equals ShipTo_A.AddressID into ShipTo_G from ShipTo in ShipTo_G.DefaultIfEmpty()// \SalesOrderID\ShipToAddressID
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
                let _Value = string.Concat(new string[] { t.SalesOrderID.ToString(),"|",t.SalesOrderDetailID.ToString() })
                select new NameValuePair
                {

                        Name = t.SalesOrderID.ToString(),
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
            SalesOrderDetailAdvancedQuery query)
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

