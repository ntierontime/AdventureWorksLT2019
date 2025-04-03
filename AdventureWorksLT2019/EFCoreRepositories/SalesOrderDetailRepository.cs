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
    public class SalesOrderDetailRepository
        : ISalesOrderDetailRepository
    {
        private readonly Dictionary<string, string> _queryOrderBys = new()
        {
        };

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
                    (!query.CustomerID.HasValue || Customer.CustomerID == query.CustomerID)&&
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

            // // 2. With Paging And OrderBy
            // var orderBys = QueryOrderBySetting.Parse(query.OrderBys);
            // if (orderBys.Any())
            // {
            //     queryable = queryable.OrderBy(QueryOrderBySetting.GetOrderByExpression(orderBys));
            // }

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

        private IQueryable<SalesOrderDetail> GetIQueryableByPrimaryIdentifierList(
            List<SalesOrderDetailIdentifier> ids)
        {
            var queryable =
                from t in _dbcontext.SalesOrderDetail
                    join SalesOrderHeader in _dbcontext.SalesOrderHeader on t.SalesOrderID equals SalesOrderHeader.SalesOrderID// \SalesOrderID

                select t;

            return queryable;
        }

        public async Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>> BulkUpdate(
            BatchActionRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> data)
        {
            if (data.ActionData == null)
            {
                return await Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>>.FromResult(
                    new ListResponse<SalesOrderDetailDataModel.DefaultView[]> { Status = HttpStatusCode.BadRequest });
            }
            try
            {
                var querable = GetIQueryableByPrimaryIdentifierList(data.Ids);

                return await Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>>.FromResult(
                    new ListResponse<SalesOrderDetailDataModel.DefaultView[]> { Status = HttpStatusCode.BadRequest });
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>>.FromResult(
                    new ListResponse<SalesOrderDetailDataModel.DefaultView[]> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private IQueryable<SalesOrderDetailDataModel.DefaultView> GetIQueryableAsBulkUpdateResponse(
            List<SalesOrderDetailIdentifier> ids)
        {
            var queryable =
                from t in _dbcontext.SalesOrderDetail
                    join Product in _dbcontext.Product on t.ProductID equals Product.ProductID// \ProductID
                    join SalesOrderHeader in _dbcontext.SalesOrderHeader on t.SalesOrderID equals SalesOrderHeader.SalesOrderID// \SalesOrderID

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

            return queryable;
        }

        public async Task<Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> input)
        {
            // 1. BulkDelete is not enabled

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<SalesOrderDetail> newEFItems = [];
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new SalesOrderDetail
                        {
                            SalesOrderID = item.SalesOrderID,
                            OrderQty = item.OrderQty,
                            ProductID = item.ProductID,
                            UnitPrice = item.UnitPrice,
                            UnitPriceDiscount = item.UnitPriceDiscount,
                            ModifiedDate = item.ModifiedDate,
                        };
                        _dbcontext.SalesOrderDetail.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.SalesOrderDetail
                             where

                             t.SalesOrderID == item.SalesOrderID
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            CopyUpdateValues(item, null, existing);
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
                        select t.SalesOrderID);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.SalesOrderID);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (
                    from t in _dbcontext.SalesOrderDetail
                    join Product in _dbcontext.Product on t.ProductID equals Product.ProductID// \ProductID
                    join SalesOrderHeader in _dbcontext.SalesOrderHeader on t.SalesOrderID equals SalesOrderHeader.SalesOrderID// \SalesOrderID
                    where identifierListToloadResponseItems.Contains(t.SalesOrderID)

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
                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.SalesOrderID == t.SalesOrderID)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.SalesOrderID == t.SalesOrderID)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<SalesOrderDetailDataModel.DefaultView>> Update(SalesOrderDetailIdentifier id, SalesOrderDetailDataModel.DefaultView input, string[]? toUpdatePropertyList = null)
        {
            if (input == null)
                return await Task<Response<SalesOrderDetailDataModel.DefaultView>>.FromResult(new Response<SalesOrderDetailDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (
                    from t in _dbcontext.SalesOrderDetail
                    join SalesOrderHeader in _dbcontext.SalesOrderHeader on t.SalesOrderID equals SalesOrderHeader.SalesOrderID// \SalesOrderID
                     where
                         (
                         id.SalesOrderDetailID.HasValue && t.SalesOrderDetailID == id.SalesOrderDetailID
                         )
                         &&
                         (
                         id.SalesOrderID.HasValue && t.SalesOrderID == id.SalesOrderID
                         )
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<SalesOrderDetailDataModel.DefaultView>>.FromResult(new Response<SalesOrderDetailDataModel.DefaultView> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                CopyUpdateValues(input, toUpdatePropertyList, existing);

                await _dbcontext.SaveChangesAsync();
                return await Get(id);

            }
            catch (Exception ex)
            {
                return await Task<Response<SalesOrderDetailDataModel.DefaultView>>.FromResult(new Response<SalesOrderDetailDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private static void CopyUpdateValues(SalesOrderDetailDataModel.DefaultView? input, string[]? toUpdatePropertyList, SalesOrderDetail existing)
        {
            if (input == null)
                return;

            // 1. This Table - SalesOrderDetail
            if (toUpdatePropertyList == null || toUpdatePropertyList.Length == 0)
            {
                existing.SalesOrderID = input.SalesOrderID;
                existing.OrderQty = input.OrderQty;
                existing.ProductID = input.ProductID;
                existing.UnitPrice = input.UnitPrice;
                existing.UnitPriceDiscount = input.UnitPriceDiscount;
                existing.ModifiedDate = input.ModifiedDate;
            }
            else
            //update Specific Properties if in toUpdatePropertyList
            {
                if(toUpdatePropertyList.Contains(nameof(SalesOrderDetailDataModel.OrderQty)))
                    existing.OrderQty = input.OrderQty;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderDetailDataModel.ProductID)))
                    existing.ProductID = input.ProductID;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderDetailDataModel.UnitPrice)))
                    existing.UnitPrice = input.UnitPrice;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderDetailDataModel.UnitPriceDiscount)))
                    existing.UnitPriceDiscount = input.UnitPriceDiscount;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderDetailDataModel.ModifiedDate)))
                    existing.ModifiedDate = input.ModifiedDate;
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
                        (
                        id.SalesOrderDetailID.HasValue && t.SalesOrderDetailID == id.SalesOrderDetailID
                        )
                        &&
                        (
                        id.SalesOrderID.HasValue && t.SalesOrderID == id.SalesOrderID
                        )

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

        public async Task<Response<SalesOrderDetailDataModel.DefaultView>> Create(SalesOrderDetailDataModel.DefaultView input)
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
                return await Get(new SalesOrderDetailIdentifier { SalesOrderID = toInsert.SalesOrderID, SalesOrderDetailID = toInsert.SalesOrderDetailID });
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
                    (!query.CustomerID.HasValue || Customer.CustomerID == query.CustomerID)&&
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

