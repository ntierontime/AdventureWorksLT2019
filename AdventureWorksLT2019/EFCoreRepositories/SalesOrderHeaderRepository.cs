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
    public class SalesOrderHeaderRepository
        : ISalesOrderHeaderRepository
    {
        private readonly ILogger<SalesOrderHeaderRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public SalesOrderHeaderRepository(EFDbContext dbcontext, ILogger<SalesOrderHeaderRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<SalesOrderHeaderDataModel.DefaultView> SearchQuery(
            SalesOrderHeaderAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.SalesOrderHeader

                    join BillTo in _dbcontext.Address on t.BillToAddressID equals BillTo.AddressID// \BillToAddressID
                    join ShipTo in _dbcontext.Address on t.ShipToAddressID equals ShipTo.AddressID// \ShipToAddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.SalesOrderNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.PurchaseOrderNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.AccountNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.ShipMethod!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.CreditCardApprovalCode!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.Comment!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.SalesOrderNumber!, query.TextSearch + "%") || EF.Functions.Like(t.PurchaseOrderNumber!, query.TextSearch + "%") || EF.Functions.Like(t.AccountNumber!, query.TextSearch + "%") || EF.Functions.Like(t.ShipMethod!, query.TextSearch + "%") || EF.Functions.Like(t.CreditCardApprovalCode!, query.TextSearch + "%") || EF.Functions.Like(t.Comment!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.SalesOrderNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.PurchaseOrderNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.AccountNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.ShipMethod!, "%" + query.TextSearch) || EF.Functions.Like(t.CreditCardApprovalCode!, "%" + query.TextSearch) || EF.Functions.Like(t.Comment!, "%" + query.TextSearch)))
                    &&

                    (!query.BillToAddressID.HasValue || BillTo.AddressID == query.BillToAddressID)
                    &&
                    (!query.ShipToAddressID.HasValue || ShipTo.AddressID == query.ShipToAddressID)
                    &&
                    (!query.CustomerID.HasValue || Customer.CustomerID == query.CustomerID)
                    &&

                    (!query.OnlineOrderFlag.HasValue || t.OnlineOrderFlag == query.OnlineOrderFlag)
                    &&

                    (!query.OrderDateRangeLower.HasValue && !query.OrderDateRangeUpper.HasValue || (!query.OrderDateRangeLower.HasValue || t.OrderDate >= query.OrderDateRangeLower) && (!query.OrderDateRangeLower.HasValue || t.OrderDate <= query.OrderDateRangeUpper))
                    &&
                    (!query.DueDateRangeLower.HasValue && !query.DueDateRangeUpper.HasValue || (!query.DueDateRangeLower.HasValue || t.DueDate >= query.DueDateRangeLower) && (!query.DueDateRangeLower.HasValue || t.DueDate <= query.DueDateRangeUpper))
                    &&
                    (!query.ShipDateRangeLower.HasValue && !query.ShipDateRangeUpper.HasValue || (!query.ShipDateRangeLower.HasValue || t.ShipDate >= query.ShipDateRangeLower) && (!query.ShipDateRangeLower.HasValue || t.ShipDate <= query.ShipDateRangeUpper))
                    &&
                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.SalesOrderNumber) ||
                            query.SalesOrderNumberSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.SalesOrderNumber!, "%" + query.SalesOrderNumber + "%") ||
                            query.SalesOrderNumberSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.SalesOrderNumber!, query.SalesOrderNumber + "%") ||
                            query.SalesOrderNumberSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.SalesOrderNumber!, "%" + query.SalesOrderNumber))
                    &&
                    (string.IsNullOrEmpty(query.PurchaseOrderNumber) ||
                            query.PurchaseOrderNumberSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.PurchaseOrderNumber!, "%" + query.PurchaseOrderNumber + "%") ||
                            query.PurchaseOrderNumberSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.PurchaseOrderNumber!, query.PurchaseOrderNumber + "%") ||
                            query.PurchaseOrderNumberSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.PurchaseOrderNumber!, "%" + query.PurchaseOrderNumber))
                    &&
                    (string.IsNullOrEmpty(query.AccountNumber) ||
                            query.AccountNumberSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.AccountNumber!, "%" + query.AccountNumber + "%") ||
                            query.AccountNumberSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.AccountNumber!, query.AccountNumber + "%") ||
                            query.AccountNumberSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.AccountNumber!, "%" + query.AccountNumber))
                    &&
                    (string.IsNullOrEmpty(query.ShipMethod) ||
                            query.ShipMethodSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.ShipMethod!, "%" + query.ShipMethod + "%") ||
                            query.ShipMethodSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.ShipMethod!, query.ShipMethod + "%") ||
                            query.ShipMethodSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.ShipMethod!, "%" + query.ShipMethod))
                    &&
                    (string.IsNullOrEmpty(query.CreditCardApprovalCode) ||
                            query.CreditCardApprovalCodeSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.CreditCardApprovalCode!, "%" + query.CreditCardApprovalCode + "%") ||
                            query.CreditCardApprovalCodeSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.CreditCardApprovalCode!, query.CreditCardApprovalCode + "%") ||
                            query.CreditCardApprovalCodeSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.CreditCardApprovalCode!, "%" + query.CreditCardApprovalCode))
                    &&
                    (string.IsNullOrEmpty(query.Comment) ||
                            query.CommentSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Comment!, "%" + query.Comment + "%") ||
                            query.CommentSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Comment!, query.Comment + "%") ||
                            query.CommentSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Comment!, "%" + query.Comment))

                select new SalesOrderHeaderDataModel.DefaultView
                {

                        SalesOrderID = t.SalesOrderID,
                        RevisionNumber = t.RevisionNumber,
                        OrderDate = t.OrderDate,
                        DueDate = t.DueDate,
                        ShipDate = t.ShipDate,
                        Status = t.Status,
                        OnlineOrderFlag = t.OnlineOrderFlag,
                        SalesOrderNumber = t.SalesOrderNumber,
                        PurchaseOrderNumber = t.PurchaseOrderNumber,
                        AccountNumber = t.AccountNumber,
                        CustomerID = t.CustomerID,
                        ShipToAddressID = t.ShipToAddressID,
                        BillToAddressID = t.BillToAddressID,
                        ShipMethod = t.ShipMethod,
                        CreditCardApprovalCode = t.CreditCardApprovalCode,
                        SubTotal = t.SubTotal,
                        TaxAmt = t.TaxAmt,
                        Freight = t.Freight,
                        TotalDue = t.TotalDue,
                        Comment = t.Comment,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        BillTo_Name = BillTo.AddressLine1,
                        Customer_Name = Customer.Title,
                        ShipTo_Name = ShipTo.AddressLine1,
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

        public async Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>> Search(
            SalesOrderHeaderAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<SalesOrderHeaderDataModel.DefaultView>();
                return new ListResponse<SalesOrderHeaderDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>>.FromResult(new ListResponse<SalesOrderHeaderDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<SalesOrderHeader> GetIQueryableByPrimaryIdentifierList(
            List<SalesOrderHeaderIdentifier> ids)
        {
            var idList = ids.Select(t => t.SalesOrderID).ToList();
            var queryable =
                from t in _dbcontext.SalesOrderHeader
                where idList.Contains(t.SalesOrderID)
                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<SalesOrderHeaderIdentifier> ids)
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

        public async Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>> BulkUpdate(
            BatchActionRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView> data)
        {
            if (data.ActionData == null)
            {
                return await Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>>.FromResult(
                    new ListResponse<SalesOrderHeaderDataModel.DefaultView[]> { Status = HttpStatusCode.BadRequest });
            }
            try
            {
                var querable = GetIQueryableByPrimaryIdentifierList(data.Ids);

                if (data.ActionName == "OnlineOrderFlag")
                {
                    var result = await querable.BatchUpdateAsync(t =>
                        new SalesOrderHeader
                        {
                            OnlineOrderFlag = data.ActionData.OnlineOrderFlag,
                        });
                    var responseBody = GetIQueryableAsBulkUpdateResponse(data.Ids);
                    return await Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>>.FromResult(
                        new ListResponse<SalesOrderHeaderDataModel.DefaultView[]> {
                            Status = HttpStatusCode.OK,
                            ResponseBody = responseBody.ToArray(),
                        });
                }

                return await Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>>.FromResult(
                    new ListResponse<SalesOrderHeaderDataModel.DefaultView[]> { Status = HttpStatusCode.BadRequest });
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>>.FromResult(
                    new ListResponse<SalesOrderHeaderDataModel.DefaultView[]> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

private IQueryable<SalesOrderHeaderDataModel.DefaultView> GetIQueryableAsBulkUpdateResponse(
            List<SalesOrderHeaderIdentifier> ids)
        {
            var idList = ids.Select(t => t.SalesOrderID).ToList();
            var queryable =
                from t in _dbcontext.SalesOrderHeader
                    join BillTo in _dbcontext.Address on t.BillToAddressID equals BillTo.AddressID// \BillToAddressID
                    join ShipTo in _dbcontext.Address on t.ShipToAddressID equals ShipTo.AddressID// \ShipToAddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                where idList.Contains(t.SalesOrderID)

                select new SalesOrderHeaderDataModel.DefaultView
                {
                        SalesOrderID = t.SalesOrderID,
                        RevisionNumber = t.RevisionNumber,
                        OrderDate = t.OrderDate,
                        DueDate = t.DueDate,
                        ShipDate = t.ShipDate,
                        Status = t.Status,
                        OnlineOrderFlag = t.OnlineOrderFlag,
                        SalesOrderNumber = t.SalesOrderNumber,
                        PurchaseOrderNumber = t.PurchaseOrderNumber,
                        AccountNumber = t.AccountNumber,
                        CustomerID = t.CustomerID,
                        ShipToAddressID = t.ShipToAddressID,
                        BillToAddressID = t.BillToAddressID,
                        ShipMethod = t.ShipMethod,
                        CreditCardApprovalCode = t.CreditCardApprovalCode,
                        SubTotal = t.SubTotal,
                        TaxAmt = t.TaxAmt,
                        Freight = t.Freight,
                        TotalDue = t.TotalDue,
                        Comment = t.Comment,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        BillTo_Name = BillTo.AddressLine1,
                        Customer_Name = Customer.Title,
                        ShipTo_Name = ShipTo.AddressLine1,
                };

            return queryable;
        }

        public async Task<Response<MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<SalesOrderHeader> newEFItems = new List<SalesOrderHeader>();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new SalesOrderHeader
                        {
                            RevisionNumber = item.RevisionNumber,
                            OrderDate = item.OrderDate,
                            DueDate = item.DueDate,
                            ShipDate = item.ShipDate,
                            Status = item.Status,
                            OnlineOrderFlag = item.OnlineOrderFlag,
                            PurchaseOrderNumber = item.PurchaseOrderNumber,
                            AccountNumber = item.AccountNumber,
                            CustomerID = item.CustomerID,
                            ShipToAddressID = item.ShipToAddressID,
                            BillToAddressID = item.BillToAddressID,
                            ShipMethod = item.ShipMethod,
                            CreditCardApprovalCode = item.CreditCardApprovalCode,
                            SubTotal = item.SubTotal,
                            TaxAmt = item.TaxAmt,
                            Freight = item.Freight,
                            Comment = item.Comment,
                            ModifiedDate = item.ModifiedDate,
                        };
                        _dbcontext.SalesOrderHeader.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.SalesOrderHeader
                             where

                             t.SalesOrderID == item.SalesOrderID
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.RevisionNumber = item.RevisionNumber;
                existing.OrderDate = item.OrderDate;
                existing.DueDate = item.DueDate;
                existing.ShipDate = item.ShipDate;
                existing.Status = item.Status;
                existing.OnlineOrderFlag = item.OnlineOrderFlag;
                existing.PurchaseOrderNumber = item.PurchaseOrderNumber;
                existing.AccountNumber = item.AccountNumber;
                existing.CustomerID = item.CustomerID;
                existing.ShipToAddressID = item.ShipToAddressID;
                existing.BillToAddressID = item.BillToAddressID;
                existing.ShipMethod = item.ShipMethod;
                existing.CreditCardApprovalCode = item.CreditCardApprovalCode;
                existing.SubTotal = item.SubTotal;
                existing.TaxAmt = item.TaxAmt;
                existing.Freight = item.Freight;
                existing.Comment = item.Comment;
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
                    (from t in _dbcontext.SalesOrderHeader
                    join BillTo in _dbcontext.Address on t.BillToAddressID equals BillTo.AddressID// \BillToAddressID
                    join ShipTo in _dbcontext.Address on t.ShipToAddressID equals ShipTo.AddressID// \ShipToAddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                    where identifierListToloadResponseItems.Contains(t.SalesOrderID)

                    select new SalesOrderHeaderDataModel.DefaultView
                    {

                        SalesOrderID = t.SalesOrderID,
                        RevisionNumber = t.RevisionNumber,
                        OrderDate = t.OrderDate,
                        DueDate = t.DueDate,
                        ShipDate = t.ShipDate,
                        Status = t.Status,
                        OnlineOrderFlag = t.OnlineOrderFlag,
                        SalesOrderNumber = t.SalesOrderNumber,
                        PurchaseOrderNumber = t.PurchaseOrderNumber,
                        AccountNumber = t.AccountNumber,
                        CustomerID = t.CustomerID,
                        ShipToAddressID = t.ShipToAddressID,
                        BillToAddressID = t.BillToAddressID,
                        ShipMethod = t.ShipMethod,
                        CreditCardApprovalCode = t.CreditCardApprovalCode,
                        SubTotal = t.SubTotal,
                        TaxAmt = t.TaxAmt,
                        Freight = t.Freight,
                        TotalDue = t.TotalDue,
                        Comment = t.Comment,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        BillTo_Name = BillTo.AddressLine1,
                        Customer_Name = Customer.Title,
                        ShipTo_Name = ShipTo.AddressLine1,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>
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
                return await Task.FromResult(new Response<MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<SalesOrderHeaderDataModel.DefaultView>> Update(SalesOrderHeaderIdentifier id, SalesOrderHeaderDataModel input)
        {
            if (input == null)
                return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(new Response<SalesOrderHeaderDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.SalesOrderHeader
                     where

                    t.SalesOrderID == id.SalesOrderID
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(new Response<SalesOrderHeaderDataModel.DefaultView> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.RevisionNumber = input.RevisionNumber;
                existing.OrderDate = input.OrderDate;
                existing.DueDate = input.DueDate;
                existing.ShipDate = input.ShipDate;
                existing.Status = input.Status;
                existing.OnlineOrderFlag = input.OnlineOrderFlag;
                existing.PurchaseOrderNumber = input.PurchaseOrderNumber;
                existing.AccountNumber = input.AccountNumber;
                existing.CustomerID = input.CustomerID;
                existing.ShipToAddressID = input.ShipToAddressID;
                existing.BillToAddressID = input.BillToAddressID;
                existing.ShipMethod = input.ShipMethod;
                existing.CreditCardApprovalCode = input.CreditCardApprovalCode;
                existing.SubTotal = input.SubTotal;
                existing.TaxAmt = input.TaxAmt;
                existing.Freight = input.Freight;
                existing.Comment = input.Comment;
                existing.ModifiedDate = input.ModifiedDate;
                await _dbcontext.SaveChangesAsync();

                var responseBody =
                    (
                    from t in _dbcontext.SalesOrderHeader

                    join BillTo in _dbcontext.Address on t.BillToAddressID equals BillTo.AddressID// \BillToAddressID
                    join ShipTo in _dbcontext.Address on t.ShipToAddressID equals ShipTo.AddressID// \ShipToAddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                    where t.SalesOrderID == existing.SalesOrderID

                    select new SalesOrderHeaderDataModel.DefaultView
                    {

                        SalesOrderID = t.SalesOrderID,
                        RevisionNumber = t.RevisionNumber,
                        OrderDate = t.OrderDate,
                        DueDate = t.DueDate,
                        ShipDate = t.ShipDate,
                        Status = t.Status,
                        OnlineOrderFlag = t.OnlineOrderFlag,
                        SalesOrderNumber = t.SalesOrderNumber,
                        PurchaseOrderNumber = t.PurchaseOrderNumber,
                        AccountNumber = t.AccountNumber,
                        CustomerID = t.CustomerID,
                        ShipToAddressID = t.ShipToAddressID,
                        BillToAddressID = t.BillToAddressID,
                        ShipMethod = t.ShipMethod,
                        CreditCardApprovalCode = t.CreditCardApprovalCode,
                        SubTotal = t.SubTotal,
                        TaxAmt = t.TaxAmt,
                        Freight = t.Freight,
                        TotalDue = t.TotalDue,
                        Comment = t.Comment,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        BillTo_Name = BillTo.AddressLine1,
                        Customer_Name = Customer.Title,
                        ShipTo_Name = ShipTo.AddressLine1,

                    }).First();

                return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(
                    new Response<SalesOrderHeaderDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(new Response<SalesOrderHeaderDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<SalesOrderHeaderDataModel.DefaultView>> Get(SalesOrderHeaderIdentifier id)
        {
            if (id == null)
                return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(new Response<SalesOrderHeaderDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {

                var responseBody =
                    (
                    from t in _dbcontext.SalesOrderHeader

                    join BillTo in _dbcontext.Address on t.BillToAddressID equals BillTo.AddressID// \BillToAddressID
                    join ShipTo in _dbcontext.Address on t.ShipToAddressID equals ShipTo.AddressID// \ShipToAddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                    where

                    t.SalesOrderID == id.SalesOrderID

                    select new SalesOrderHeaderDataModel.DefaultView
                    {

                        SalesOrderID = t.SalesOrderID,
                        RevisionNumber = t.RevisionNumber,
                        OrderDate = t.OrderDate,
                        DueDate = t.DueDate,
                        ShipDate = t.ShipDate,
                        Status = t.Status,
                        OnlineOrderFlag = t.OnlineOrderFlag,
                        SalesOrderNumber = t.SalesOrderNumber,
                        PurchaseOrderNumber = t.PurchaseOrderNumber,
                        AccountNumber = t.AccountNumber,
                        CustomerID = t.CustomerID,
                        ShipToAddressID = t.ShipToAddressID,
                        BillToAddressID = t.BillToAddressID,
                        ShipMethod = t.ShipMethod,
                        CreditCardApprovalCode = t.CreditCardApprovalCode,
                        SubTotal = t.SubTotal,
                        TaxAmt = t.TaxAmt,
                        Freight = t.Freight,
                        TotalDue = t.TotalDue,
                        Comment = t.Comment,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        BillTo_Name = BillTo.AddressLine1,
                        Customer_Name = Customer.Title,
                        ShipTo_Name = ShipTo.AddressLine1,

                    }).First();
                if (responseBody == null)
                    return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(new Response<SalesOrderHeaderDataModel.DefaultView> { Status = HttpStatusCode.NotFound });
                return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(
                    new Response<SalesOrderHeaderDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(new Response<SalesOrderHeaderDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<SalesOrderHeaderDataModel.DefaultView>> Create(SalesOrderHeaderDataModel input)
        {
            if (input == null)
                return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(new Response<SalesOrderHeaderDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new SalesOrderHeader
                {
                            RevisionNumber = input.RevisionNumber,
                            OrderDate = input.OrderDate,
                            DueDate = input.DueDate,
                            ShipDate = input.ShipDate,
                            Status = input.Status,
                            OnlineOrderFlag = input.OnlineOrderFlag,
                            PurchaseOrderNumber = input.PurchaseOrderNumber,
                            AccountNumber = input.AccountNumber,
                            CustomerID = input.CustomerID,
                            ShipToAddressID = input.ShipToAddressID,
                            BillToAddressID = input.BillToAddressID,
                            ShipMethod = input.ShipMethod,
                            CreditCardApprovalCode = input.CreditCardApprovalCode,
                            SubTotal = input.SubTotal,
                            TaxAmt = input.TaxAmt,
                            Freight = input.Freight,
                            Comment = input.Comment,
                            ModifiedDate = input.ModifiedDate,
                };
                await _dbcontext.SalesOrderHeader.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                var responseBody =
                    (
                    from t in _dbcontext.SalesOrderHeader

                    join BillTo in _dbcontext.Address on t.BillToAddressID equals BillTo.AddressID// \BillToAddressID
                    join ShipTo in _dbcontext.Address on t.ShipToAddressID equals ShipTo.AddressID// \ShipToAddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                    where t.SalesOrderID == toInsert.SalesOrderID

                    select new SalesOrderHeaderDataModel.DefaultView
                    {

                        SalesOrderID = t.SalesOrderID,
                        RevisionNumber = t.RevisionNumber,
                        OrderDate = t.OrderDate,
                        DueDate = t.DueDate,
                        ShipDate = t.ShipDate,
                        Status = t.Status,
                        OnlineOrderFlag = t.OnlineOrderFlag,
                        SalesOrderNumber = t.SalesOrderNumber,
                        PurchaseOrderNumber = t.PurchaseOrderNumber,
                        AccountNumber = t.AccountNumber,
                        CustomerID = t.CustomerID,
                        ShipToAddressID = t.ShipToAddressID,
                        BillToAddressID = t.BillToAddressID,
                        ShipMethod = t.ShipMethod,
                        CreditCardApprovalCode = t.CreditCardApprovalCode,
                        SubTotal = t.SubTotal,
                        TaxAmt = t.TaxAmt,
                        Freight = t.Freight,
                        TotalDue = t.TotalDue,
                        Comment = t.Comment,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        BillTo_Name = BillTo.AddressLine1,
                        Customer_Name = Customer.Title,
                        ShipTo_Name = ShipTo.AddressLine1,

                    }).First();

                return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(
                    new Response<SalesOrderHeaderDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(new Response<SalesOrderHeaderDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(SalesOrderHeaderIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.SalesOrderHeader
                     where

                    t.SalesOrderID == id.SalesOrderID
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.SalesOrderHeader.Remove(existing);
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
            SalesOrderHeaderAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.SalesOrderHeader

                    join BillTo in _dbcontext.Address on t.BillToAddressID equals BillTo.AddressID// \BillToAddressID
                    join ShipTo in _dbcontext.Address on t.ShipToAddressID equals ShipTo.AddressID// \ShipToAddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.SalesOrderNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.PurchaseOrderNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.AccountNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.ShipMethod!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.CreditCardApprovalCode!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.Comment!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.SalesOrderNumber!, query.TextSearch + "%") || EF.Functions.Like(t.PurchaseOrderNumber!, query.TextSearch + "%") || EF.Functions.Like(t.AccountNumber!, query.TextSearch + "%") || EF.Functions.Like(t.ShipMethod!, query.TextSearch + "%") || EF.Functions.Like(t.CreditCardApprovalCode!, query.TextSearch + "%") || EF.Functions.Like(t.Comment!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.SalesOrderNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.PurchaseOrderNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.AccountNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.ShipMethod!, "%" + query.TextSearch) || EF.Functions.Like(t.CreditCardApprovalCode!, "%" + query.TextSearch) || EF.Functions.Like(t.Comment!, "%" + query.TextSearch)))
                    &&

                    (!query.BillToAddressID.HasValue || BillTo.AddressID == query.BillToAddressID)
                    &&
                    (!query.ShipToAddressID.HasValue || ShipTo.AddressID == query.ShipToAddressID)
                    &&
                    (!query.CustomerID.HasValue || Customer.CustomerID == query.CustomerID)
                    &&

                    (!query.OnlineOrderFlag.HasValue || t.OnlineOrderFlag == query.OnlineOrderFlag)
                    &&

                    (!query.OrderDateRangeLower.HasValue && !query.OrderDateRangeUpper.HasValue || (!query.OrderDateRangeLower.HasValue || t.OrderDate >= query.OrderDateRangeLower) && (!query.OrderDateRangeLower.HasValue || t.OrderDate <= query.OrderDateRangeUpper))
                    &&
                    (!query.DueDateRangeLower.HasValue && !query.DueDateRangeUpper.HasValue || (!query.DueDateRangeLower.HasValue || t.DueDate >= query.DueDateRangeLower) && (!query.DueDateRangeLower.HasValue || t.DueDate <= query.DueDateRangeUpper))
                    &&
                    (!query.ShipDateRangeLower.HasValue && !query.ShipDateRangeUpper.HasValue || (!query.ShipDateRangeLower.HasValue || t.ShipDate >= query.ShipDateRangeLower) && (!query.ShipDateRangeLower.HasValue || t.ShipDate <= query.ShipDateRangeUpper))
                    &&
                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.SalesOrderNumber) ||
                            query.SalesOrderNumberSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.SalesOrderNumber!, "%" + query.SalesOrderNumber + "%") ||
                            query.SalesOrderNumberSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.SalesOrderNumber!, query.SalesOrderNumber + "%") ||
                            query.SalesOrderNumberSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.SalesOrderNumber!, "%" + query.SalesOrderNumber))
                    &&
                    (string.IsNullOrEmpty(query.PurchaseOrderNumber) ||
                            query.PurchaseOrderNumberSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.PurchaseOrderNumber!, "%" + query.PurchaseOrderNumber + "%") ||
                            query.PurchaseOrderNumberSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.PurchaseOrderNumber!, query.PurchaseOrderNumber + "%") ||
                            query.PurchaseOrderNumberSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.PurchaseOrderNumber!, "%" + query.PurchaseOrderNumber))
                    &&
                    (string.IsNullOrEmpty(query.AccountNumber) ||
                            query.AccountNumberSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.AccountNumber!, "%" + query.AccountNumber + "%") ||
                            query.AccountNumberSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.AccountNumber!, query.AccountNumber + "%") ||
                            query.AccountNumberSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.AccountNumber!, "%" + query.AccountNumber))
                    &&
                    (string.IsNullOrEmpty(query.ShipMethod) ||
                            query.ShipMethodSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.ShipMethod!, "%" + query.ShipMethod + "%") ||
                            query.ShipMethodSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.ShipMethod!, query.ShipMethod + "%") ||
                            query.ShipMethodSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.ShipMethod!, "%" + query.ShipMethod))
                    &&
                    (string.IsNullOrEmpty(query.CreditCardApprovalCode) ||
                            query.CreditCardApprovalCodeSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.CreditCardApprovalCode!, "%" + query.CreditCardApprovalCode + "%") ||
                            query.CreditCardApprovalCodeSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.CreditCardApprovalCode!, query.CreditCardApprovalCode + "%") ||
                            query.CreditCardApprovalCodeSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.CreditCardApprovalCode!, "%" + query.CreditCardApprovalCode))
                    &&
                    (string.IsNullOrEmpty(query.Comment) ||
                            query.CommentSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Comment!, "%" + query.Comment + "%") ||
                            query.CommentSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Comment!, query.Comment + "%") ||
                            query.CommentSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Comment!, "%" + query.Comment))

                select new NameValuePair
                {

                        Value = t.SalesOrderID.ToString(),
                        Name = t.SalesOrderNumber,
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
            SalesOrderHeaderAdvancedQuery query)
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

