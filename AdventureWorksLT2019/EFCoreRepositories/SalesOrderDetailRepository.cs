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

                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<SalesOrderDetailIdentifier> ids)
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

        public async Task<Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

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
                List<SalesOrderDetail> newEFItems = new List<SalesOrderDetail>();
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
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.SalesOrderID = item.SalesOrderID;
                existing.OrderQty = item.OrderQty;
                existing.ProductID = item.ProductID;
                existing.UnitPrice = item.UnitPrice;
                existing.UnitPriceDiscount = item.UnitPriceDiscount;
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
                        select t.SalesOrderID);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.SalesOrderID);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.SalesOrderDetail
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

        public async Task<Response> Delete(SalesOrderDetailIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.SalesOrderDetail
                     where

                    t.SalesOrderID == id.SalesOrderID
                    &&
                    t.SalesOrderDetailID == id.SalesOrderDetailID
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.SalesOrderDetail.Remove(existing);
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

