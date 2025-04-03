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
    public class SalesOrderHeaderRepository
        : ISalesOrderHeaderRepository
    {
        private readonly Dictionary<string, string> _queryOrderBys = new()
        {
        };

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

                    join BillTo_A in _dbcontext.Address on t.BillToAddressID equals BillTo_A.AddressID into BillTo_G from BillTo in BillTo_G.DefaultIfEmpty()// \BillToAddressID
                    join ShipTo_A in _dbcontext.Address on t.ShipToAddressID equals ShipTo_A.AddressID into ShipTo_G from ShipTo in ShipTo_G.DefaultIfEmpty()// \ShipToAddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                where
                    (string.IsNullOrEmpty(query.TextSearch) ||
                    query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.SalesOrderNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.PurchaseOrderNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.AccountNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.ShipMethod!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.CreditCardApprovalCode!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.Comment!, "%" + query.TextSearch + "%")) ||
                    query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.SalesOrderNumber!, query.TextSearch + "%") || EF.Functions.Like(t.PurchaseOrderNumber!, query.TextSearch + "%") || EF.Functions.Like(t.AccountNumber!, query.TextSearch + "%") || EF.Functions.Like(t.ShipMethod!, query.TextSearch + "%") || EF.Functions.Like(t.CreditCardApprovalCode!, query.TextSearch + "%") || EF.Functions.Like(t.Comment!, query.TextSearch + "%")) ||
                    query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.SalesOrderNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.PurchaseOrderNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.AccountNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.ShipMethod!, "%" + query.TextSearch) || EF.Functions.Like(t.CreditCardApprovalCode!, "%" + query.TextSearch) || EF.Functions.Like(t.Comment!, "%" + query.TextSearch)))&&
                    (!query.BillToAddressID.HasValue || BillTo.AddressID == query.BillToAddressID)
                    &&
                    (!query.ShipToAddressID.HasValue || ShipTo.AddressID == query.ShipToAddressID)
                    &&
                    (!query.CustomerID.HasValue || Customer.CustomerID == query.CustomerID)&&
                    (!query.OnlineOrderFlag.HasValue || query.OnlineOrderFlag == BooleanSearchOptions.All || query.OnlineOrderFlag == BooleanSearchOptions.True && t.OnlineOrderFlag == true || query.OnlineOrderFlag == BooleanSearchOptions.False && t.OnlineOrderFlag != true)&&
                    (!query.OrderDateRangeLower.HasValue && !query.OrderDateRangeUpper.HasValue || (!query.OrderDateRangeLower.HasValue || t.OrderDate >= query.OrderDateRangeLower) && (!query.OrderDateRangeLower.HasValue || t.OrderDate <= query.OrderDateRangeUpper))
                    &&
                    (!query.DueDateRangeLower.HasValue && !query.DueDateRangeUpper.HasValue || (!query.DueDateRangeLower.HasValue || t.DueDate >= query.DueDateRangeLower) && (!query.DueDateRangeLower.HasValue || t.DueDate <= query.DueDateRangeUpper))
                    &&
                    (!query.ShipDateRangeLower.HasValue && !query.ShipDateRangeUpper.HasValue || (!query.ShipDateRangeLower.HasValue || t.ShipDate >= query.ShipDateRangeLower) && (!query.ShipDateRangeLower.HasValue || t.ShipDate <= query.ShipDateRangeUpper))
                    &&
                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))&&
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

            // // 2. With Paging And OrderBy
            // var orderBys = QueryOrderBySetting.Parse(query.OrderBys);
            // if (orderBys.Any())
            // {
            //     queryable = queryable.OrderBy(QueryOrderBySetting.GetOrderByExpression(orderBys));
            // }

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

        public async Task<Response<SalesOrderHeaderDataModel.DefaultView>> Update(SalesOrderHeaderIdentifier id, SalesOrderHeaderDataModel.DefaultView input, string[]? toUpdatePropertyList = null)
        {
            if (input == null)
                return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(new Response<SalesOrderHeaderDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (
                    from t in _dbcontext.SalesOrderHeader
                     where
                         id.SalesOrderID.HasValue && t.SalesOrderID == id.SalesOrderID
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(new Response<SalesOrderHeaderDataModel.DefaultView> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                CopyUpdateValues(input, toUpdatePropertyList, existing);

                await _dbcontext.SaveChangesAsync();
                return await Get(id);

            }
            catch (Exception ex)
            {
                return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(new Response<SalesOrderHeaderDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private static void CopyUpdateValues(SalesOrderHeaderDataModel.DefaultView? input, string[]? toUpdatePropertyList, SalesOrderHeader existing)
        {
            if (input == null)
                return;

            // 1. This Table - SalesOrderHeader
            if (toUpdatePropertyList == null || toUpdatePropertyList.Length == 0)
            {
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
            }
            else
            //update Specific Properties if in toUpdatePropertyList
            {
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.RevisionNumber)))
                    existing.RevisionNumber = input.RevisionNumber;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.OrderDate)))
                    existing.OrderDate = input.OrderDate;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.DueDate)))
                    existing.DueDate = input.DueDate;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.ShipDate)))
                    existing.ShipDate = input.ShipDate;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.Status)))
                    existing.Status = input.Status;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.OnlineOrderFlag)))
                    existing.OnlineOrderFlag = input.OnlineOrderFlag;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.PurchaseOrderNumber)))
                    existing.PurchaseOrderNumber = input.PurchaseOrderNumber;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.AccountNumber)))
                    existing.AccountNumber = input.AccountNumber;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.CustomerID)))
                    existing.CustomerID = input.CustomerID;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.ShipToAddressID)))
                    existing.ShipToAddressID = input.ShipToAddressID;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.BillToAddressID)))
                    existing.BillToAddressID = input.BillToAddressID;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.ShipMethod)))
                    existing.ShipMethod = input.ShipMethod;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.CreditCardApprovalCode)))
                    existing.CreditCardApprovalCode = input.CreditCardApprovalCode;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.SubTotal)))
                    existing.SubTotal = input.SubTotal;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.TaxAmt)))
                    existing.TaxAmt = input.TaxAmt;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.Freight)))
                    existing.Freight = input.Freight;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.Comment)))
                    existing.Comment = input.Comment;
                if(toUpdatePropertyList.Contains(nameof(SalesOrderHeaderDataModel.ModifiedDate)))
                    existing.ModifiedDate = input.ModifiedDate;
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

                    join BillTo_A in _dbcontext.Address on t.BillToAddressID equals BillTo_A.AddressID into BillTo_G from BillTo in BillTo_G.DefaultIfEmpty()// \BillToAddressID
                    join ShipTo_A in _dbcontext.Address on t.ShipToAddressID equals ShipTo_A.AddressID into ShipTo_G from ShipTo in ShipTo_G.DefaultIfEmpty()// \ShipToAddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                    where
                        id.SalesOrderID.HasValue && t.SalesOrderID == id.SalesOrderID

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

        public async Task<Response<SalesOrderHeaderDataModel.DefaultView>> Create(SalesOrderHeaderDataModel.DefaultView input)
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
                return await Get(new SalesOrderHeaderIdentifier { SalesOrderID = toInsert.SalesOrderID });
            }
            catch (Exception ex)
            {
                return await Task<Response<SalesOrderHeaderDataModel.DefaultView>>.FromResult(new Response<SalesOrderHeaderDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private IQueryable<NameValuePair> GetCodeListQuery(
            SalesOrderHeaderAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var queryable =
                from t in _dbcontext.SalesOrderHeader

                    join BillTo_A in _dbcontext.Address on t.BillToAddressID equals BillTo_A.AddressID into BillTo_G from BillTo in BillTo_G.DefaultIfEmpty()// \BillToAddressID
                    join ShipTo_A in _dbcontext.Address on t.ShipToAddressID equals ShipTo_A.AddressID into ShipTo_G from ShipTo in ShipTo_G.DefaultIfEmpty()// \ShipToAddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                where
                    (string.IsNullOrEmpty(query.TextSearch) ||
                    query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.SalesOrderNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.PurchaseOrderNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.AccountNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.ShipMethod!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.CreditCardApprovalCode!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.Comment!, "%" + query.TextSearch + "%")) ||
                    query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.SalesOrderNumber!, query.TextSearch + "%") || EF.Functions.Like(t.PurchaseOrderNumber!, query.TextSearch + "%") || EF.Functions.Like(t.AccountNumber!, query.TextSearch + "%") || EF.Functions.Like(t.ShipMethod!, query.TextSearch + "%") || EF.Functions.Like(t.CreditCardApprovalCode!, query.TextSearch + "%") || EF.Functions.Like(t.Comment!, query.TextSearch + "%")) ||
                    query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.SalesOrderNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.PurchaseOrderNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.AccountNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.ShipMethod!, "%" + query.TextSearch) || EF.Functions.Like(t.CreditCardApprovalCode!, "%" + query.TextSearch) || EF.Functions.Like(t.Comment!, "%" + query.TextSearch)))&&
                    (!query.BillToAddressID.HasValue || BillTo.AddressID == query.BillToAddressID)
                    &&
                    (!query.ShipToAddressID.HasValue || ShipTo.AddressID == query.ShipToAddressID)
                    &&
                    (!query.CustomerID.HasValue || Customer.CustomerID == query.CustomerID)&&
                    (!query.OnlineOrderFlag.HasValue || query.OnlineOrderFlag == BooleanSearchOptions.All || query.OnlineOrderFlag == BooleanSearchOptions.True && t.OnlineOrderFlag == true || query.OnlineOrderFlag == BooleanSearchOptions.False && t.OnlineOrderFlag != true)&&
                    (!query.OrderDateRangeLower.HasValue && !query.OrderDateRangeUpper.HasValue || (!query.OrderDateRangeLower.HasValue || t.OrderDate >= query.OrderDateRangeLower) && (!query.OrderDateRangeLower.HasValue || t.OrderDate <= query.OrderDateRangeUpper))
                    &&
                    (!query.DueDateRangeLower.HasValue && !query.DueDateRangeUpper.HasValue || (!query.DueDateRangeLower.HasValue || t.DueDate >= query.DueDateRangeLower) && (!query.DueDateRangeLower.HasValue || t.DueDate <= query.DueDateRangeUpper))
                    &&
                    (!query.ShipDateRangeLower.HasValue && !query.ShipDateRangeUpper.HasValue || (!query.ShipDateRangeLower.HasValue || t.ShipDate >= query.ShipDateRangeLower) && (!query.ShipDateRangeLower.HasValue || t.ShipDate <= query.ShipDateRangeUpper))
                    &&
                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))&&
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

