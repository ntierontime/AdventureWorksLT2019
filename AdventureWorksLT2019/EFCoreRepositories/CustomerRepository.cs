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
    public class CustomerRepository
        : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public CustomerRepository(EFDbContext dbcontext, ILogger<CustomerRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<CustomerDataModel> SearchQuery(
            CustomerAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.Customer

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Title!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.FirstName!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.MiddleName!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.LastName!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.Suffix!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.CompanyName!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.SalesPerson!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.EmailAddress!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.Phone!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.PasswordHash!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.PasswordSalt!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Title!, query.TextSearch + "%") || EF.Functions.Like(t.FirstName!, query.TextSearch + "%") || EF.Functions.Like(t.MiddleName!, query.TextSearch + "%") || EF.Functions.Like(t.LastName!, query.TextSearch + "%") || EF.Functions.Like(t.Suffix!, query.TextSearch + "%") || EF.Functions.Like(t.CompanyName!, query.TextSearch + "%") || EF.Functions.Like(t.SalesPerson!, query.TextSearch + "%") || EF.Functions.Like(t.EmailAddress!, query.TextSearch + "%") || EF.Functions.Like(t.Phone!, query.TextSearch + "%") || EF.Functions.Like(t.PasswordHash!, query.TextSearch + "%") || EF.Functions.Like(t.PasswordSalt!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Title!, "%" + query.TextSearch) || EF.Functions.Like(t.FirstName!, "%" + query.TextSearch) || EF.Functions.Like(t.MiddleName!, "%" + query.TextSearch) || EF.Functions.Like(t.LastName!, "%" + query.TextSearch) || EF.Functions.Like(t.Suffix!, "%" + query.TextSearch) || EF.Functions.Like(t.CompanyName!, "%" + query.TextSearch) || EF.Functions.Like(t.SalesPerson!, "%" + query.TextSearch) || EF.Functions.Like(t.EmailAddress!, "%" + query.TextSearch) || EF.Functions.Like(t.Phone!, "%" + query.TextSearch) || EF.Functions.Like(t.PasswordHash!, "%" + query.TextSearch) || EF.Functions.Like(t.PasswordSalt!, "%" + query.TextSearch)))
                    &&

                    (query.NameStyle == BooleanSearchOptions.All || query.NameStyle == BooleanSearchOptions.True && t.NameStyle || query.NameStyle == BooleanSearchOptions.False && !t.NameStyle)
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Title) ||
                            query.TitleSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Title!, "%" + query.Title + "%") ||
                            query.TitleSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Title!, query.Title + "%") ||
                            query.TitleSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Title!, "%" + query.Title))
                    &&
                    (string.IsNullOrEmpty(query.FirstName) ||
                            query.FirstNameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.FirstName!, "%" + query.FirstName + "%") ||
                            query.FirstNameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.FirstName!, query.FirstName + "%") ||
                            query.FirstNameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.FirstName!, "%" + query.FirstName))
                    &&
                    (string.IsNullOrEmpty(query.MiddleName) ||
                            query.MiddleNameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.MiddleName!, "%" + query.MiddleName + "%") ||
                            query.MiddleNameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.MiddleName!, query.MiddleName + "%") ||
                            query.MiddleNameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.MiddleName!, "%" + query.MiddleName))
                    &&
                    (string.IsNullOrEmpty(query.LastName) ||
                            query.LastNameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.LastName!, "%" + query.LastName + "%") ||
                            query.LastNameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.LastName!, query.LastName + "%") ||
                            query.LastNameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.LastName!, "%" + query.LastName))
                    &&
                    (string.IsNullOrEmpty(query.Suffix) ||
                            query.SuffixSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Suffix!, "%" + query.Suffix + "%") ||
                            query.SuffixSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Suffix!, query.Suffix + "%") ||
                            query.SuffixSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Suffix!, "%" + query.Suffix))
                    &&
                    (string.IsNullOrEmpty(query.CompanyName) ||
                            query.CompanyNameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.CompanyName!, "%" + query.CompanyName + "%") ||
                            query.CompanyNameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.CompanyName!, query.CompanyName + "%") ||
                            query.CompanyNameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.CompanyName!, "%" + query.CompanyName))
                    &&
                    (string.IsNullOrEmpty(query.SalesPerson) ||
                            query.SalesPersonSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.SalesPerson!, "%" + query.SalesPerson + "%") ||
                            query.SalesPersonSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.SalesPerson!, query.SalesPerson + "%") ||
                            query.SalesPersonSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.SalesPerson!, "%" + query.SalesPerson))
                    &&
                    (string.IsNullOrEmpty(query.EmailAddress) ||
                            query.EmailAddressSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.EmailAddress!, "%" + query.EmailAddress + "%") ||
                            query.EmailAddressSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.EmailAddress!, query.EmailAddress + "%") ||
                            query.EmailAddressSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.EmailAddress!, "%" + query.EmailAddress))
                    &&
                    (string.IsNullOrEmpty(query.Phone) ||
                            query.PhoneSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Phone!, "%" + query.Phone + "%") ||
                            query.PhoneSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Phone!, query.Phone + "%") ||
                            query.PhoneSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Phone!, "%" + query.Phone))
                    &&
                    (string.IsNullOrEmpty(query.PasswordHash) ||
                            query.PasswordHashSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.PasswordHash!, "%" + query.PasswordHash + "%") ||
                            query.PasswordHashSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.PasswordHash!, query.PasswordHash + "%") ||
                            query.PasswordHashSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.PasswordHash!, "%" + query.PasswordHash))
                    &&
                    (string.IsNullOrEmpty(query.PasswordSalt) ||
                            query.PasswordSaltSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.PasswordSalt!, "%" + query.PasswordSalt + "%") ||
                            query.PasswordSaltSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.PasswordSalt!, query.PasswordSalt + "%") ||
                            query.PasswordSaltSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.PasswordSalt!, "%" + query.PasswordSalt))

                select new CustomerDataModel
                {

                        CustomerID = t.CustomerID,
                        NameStyle = t.NameStyle,
                        Title = t.Title,
                        FirstName = t.FirstName,
                        MiddleName = t.MiddleName,
                        LastName = t.LastName,
                        Suffix = t.Suffix,
                        CompanyName = t.CompanyName,
                        SalesPerson = t.SalesPerson,
                        EmailAddress = t.EmailAddress,
                        Phone = t.Phone,
                        PasswordHash = t.PasswordHash,
                        PasswordSalt = t.PasswordSalt,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
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

        public async Task<ListResponse<CustomerDataModel[]>> Search(
            CustomerAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<CustomerDataModel>();
                return new ListResponse<CustomerDataModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<CustomerDataModel[]>>.FromResult(new ListResponse<CustomerDataModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<Customer> GetIQueryableByPrimaryIdentifierList(
            List<CustomerIdentifier> ids)
        {
            var idList = ids.Select(t => t.CustomerID).ToList();
            var queryable =
                from t in _dbcontext.Customer
                where idList.Contains(t.CustomerID)
                select t;

            return queryable;
        }

        public async Task<ListResponse<CustomerDataModel[]>> BulkUpdate(
            BatchActionRequest<CustomerIdentifier, CustomerDataModel> data)
        {
            if (data.ActionData == null)
            {
                return await Task<ListResponse<CustomerDataModel[]>>.FromResult(
                    new ListResponse<CustomerDataModel[]> { Status = HttpStatusCode.BadRequest });
            }
            try
            {
                var querable = GetIQueryableByPrimaryIdentifierList(data.Ids);

                if (data.ActionName == "NameStyle")
                {
                    var result = await querable.BatchUpdateAsync(t =>
                        new Customer
                        {
                            NameStyle = data.ActionData.NameStyle,
                        });
                    var responseBody = GetIQueryableAsBulkUpdateResponse(data.Ids);
                    return await Task<ListResponse<CustomerDataModel[]>>.FromResult(
                        new ListResponse<CustomerDataModel[]> {
                            Status = HttpStatusCode.OK,
                            ResponseBody = responseBody.ToArray(),
                        });
                }

                return await Task<ListResponse<CustomerDataModel[]>>.FromResult(
                    new ListResponse<CustomerDataModel[]> { Status = HttpStatusCode.BadRequest });
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<CustomerDataModel[]>>.FromResult(
                    new ListResponse<CustomerDataModel[]> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

private IQueryable<CustomerDataModel> GetIQueryableAsBulkUpdateResponse(
            List<CustomerIdentifier> ids)
        {
            var idList = ids.Select(t => t.CustomerID).ToList();
            var queryable =
                from t in _dbcontext.Customer
                where idList.Contains(t.CustomerID)

                select new CustomerDataModel
                {
                        CustomerID = t.CustomerID,
                        NameStyle = t.NameStyle,
                        Title = t.Title,
                        FirstName = t.FirstName,
                        MiddleName = t.MiddleName,
                        LastName = t.LastName,
                        Suffix = t.Suffix,
                        CompanyName = t.CompanyName,
                        SalesPerson = t.SalesPerson,
                        EmailAddress = t.EmailAddress,
                        Phone = t.Phone,
                        PasswordHash = t.PasswordHash,
                        PasswordSalt = t.PasswordSalt,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                };

            return queryable;
        }

        public async Task<Response<CustomerDataModel>> Update(CustomerIdentifier id, CustomerDataModel input)
        {
            if (input == null)
                return await Task<Response<CustomerDataModel>>.FromResult(new Response<CustomerDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.Customer
                     where

                    t.CustomerID == id.CustomerID
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<CustomerDataModel>>.FromResult(new Response<CustomerDataModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.NameStyle = input.NameStyle;
                existing.Title = input.Title;
                existing.FirstName = input.FirstName;
                existing.MiddleName = input.MiddleName;
                existing.LastName = input.LastName;
                existing.Suffix = input.Suffix;
                existing.CompanyName = input.CompanyName;
                existing.SalesPerson = input.SalesPerson;
                existing.EmailAddress = input.EmailAddress;
                existing.Phone = input.Phone;
                existing.PasswordHash = input.PasswordHash;
                existing.PasswordSalt = input.PasswordSalt;
                existing.ModifiedDate = input.ModifiedDate;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<CustomerDataModel>>.FromResult(
                    new Response<CustomerDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new CustomerDataModel
                        {
                    CustomerID = existing.CustomerID,
                    NameStyle = existing.NameStyle,
                    Title = existing.Title,
                    FirstName = existing.FirstName,
                    MiddleName = existing.MiddleName,
                    LastName = existing.LastName,
                    Suffix = existing.Suffix,
                    CompanyName = existing.CompanyName,
                    SalesPerson = existing.SalesPerson,
                    EmailAddress = existing.EmailAddress,
                    Phone = existing.Phone,
                    PasswordHash = existing.PasswordHash,
                    PasswordSalt = existing.PasswordSalt,
                    rowguid = existing.rowguid,
                    ModifiedDate = existing.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<CustomerDataModel>>.FromResult(new Response<CustomerDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<CustomerDataModel>> Get(CustomerIdentifier id)
        {
            if (id == null)
                return await Task<Response<CustomerDataModel>>.FromResult(new Response<CustomerDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.Customer.SingleOrDefault(
                    t =>

                    t.CustomerID == id.CustomerID
                );

                if (existing == null)
                    return await Task<Response<CustomerDataModel>>.FromResult(new Response<CustomerDataModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<CustomerDataModel>>.FromResult(
                    new Response<CustomerDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new CustomerDataModel
                        {
                    CustomerID = existing.CustomerID,
                    NameStyle = existing.NameStyle,
                    Title = existing.Title,
                    FirstName = existing.FirstName,
                    MiddleName = existing.MiddleName,
                    LastName = existing.LastName,
                    Suffix = existing.Suffix,
                    CompanyName = existing.CompanyName,
                    SalesPerson = existing.SalesPerson,
                    EmailAddress = existing.EmailAddress,
                    Phone = existing.Phone,
                    PasswordHash = existing.PasswordHash,
                    PasswordSalt = existing.PasswordSalt,
                    rowguid = existing.rowguid,
                    ModifiedDate = existing.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<CustomerDataModel>>.FromResult(new Response<CustomerDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<CustomerDataModel>> Create(CustomerDataModel input)
        {
            if (input == null)
                return await Task<Response<CustomerDataModel>>.FromResult(new Response<CustomerDataModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new Customer
                {
                            NameStyle = input.NameStyle,
                            Title = input.Title,
                            FirstName = input.FirstName,
                            MiddleName = input.MiddleName,
                            LastName = input.LastName,
                            Suffix = input.Suffix,
                            CompanyName = input.CompanyName,
                            SalesPerson = input.SalesPerson,
                            EmailAddress = input.EmailAddress,
                            Phone = input.Phone,
                            PasswordHash = input.PasswordHash,
                            PasswordSalt = input.PasswordSalt,
                            ModifiedDate = input.ModifiedDate,
                };
                await _dbcontext.Customer.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<CustomerDataModel>>.FromResult(
                    new Response<CustomerDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new CustomerDataModel
                        {
                    CustomerID = toInsert.CustomerID,
                    NameStyle = toInsert.NameStyle,
                    Title = toInsert.Title,
                    FirstName = toInsert.FirstName,
                    MiddleName = toInsert.MiddleName,
                    LastName = toInsert.LastName,
                    Suffix = toInsert.Suffix,
                    CompanyName = toInsert.CompanyName,
                    SalesPerson = toInsert.SalesPerson,
                    EmailAddress = toInsert.EmailAddress,
                    Phone = toInsert.Phone,
                    PasswordHash = toInsert.PasswordHash,
                    PasswordSalt = toInsert.PasswordSalt,
                    rowguid = toInsert.rowguid,
                    ModifiedDate = toInsert.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<CustomerDataModel>>.FromResult(new Response<CustomerDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private IQueryable<NameValuePair> GetCodeListQuery(
            CustomerAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.Customer

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Title!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.FirstName!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.MiddleName!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.LastName!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.Suffix!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.CompanyName!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.SalesPerson!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.EmailAddress!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.Phone!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.PasswordHash!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.PasswordSalt!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Title!, query.TextSearch + "%") || EF.Functions.Like(t.FirstName!, query.TextSearch + "%") || EF.Functions.Like(t.MiddleName!, query.TextSearch + "%") || EF.Functions.Like(t.LastName!, query.TextSearch + "%") || EF.Functions.Like(t.Suffix!, query.TextSearch + "%") || EF.Functions.Like(t.CompanyName!, query.TextSearch + "%") || EF.Functions.Like(t.SalesPerson!, query.TextSearch + "%") || EF.Functions.Like(t.EmailAddress!, query.TextSearch + "%") || EF.Functions.Like(t.Phone!, query.TextSearch + "%") || EF.Functions.Like(t.PasswordHash!, query.TextSearch + "%") || EF.Functions.Like(t.PasswordSalt!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Title!, "%" + query.TextSearch) || EF.Functions.Like(t.FirstName!, "%" + query.TextSearch) || EF.Functions.Like(t.MiddleName!, "%" + query.TextSearch) || EF.Functions.Like(t.LastName!, "%" + query.TextSearch) || EF.Functions.Like(t.Suffix!, "%" + query.TextSearch) || EF.Functions.Like(t.CompanyName!, "%" + query.TextSearch) || EF.Functions.Like(t.SalesPerson!, "%" + query.TextSearch) || EF.Functions.Like(t.EmailAddress!, "%" + query.TextSearch) || EF.Functions.Like(t.Phone!, "%" + query.TextSearch) || EF.Functions.Like(t.PasswordHash!, "%" + query.TextSearch) || EF.Functions.Like(t.PasswordSalt!, "%" + query.TextSearch)))
                    &&

                    (query.NameStyle == BooleanSearchOptions.All || query.NameStyle == BooleanSearchOptions.True && t.NameStyle || query.NameStyle == BooleanSearchOptions.False && !t.NameStyle)
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Title) ||
                            query.TitleSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Title!, "%" + query.Title + "%") ||
                            query.TitleSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Title!, query.Title + "%") ||
                            query.TitleSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Title!, "%" + query.Title))
                    &&
                    (string.IsNullOrEmpty(query.FirstName) ||
                            query.FirstNameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.FirstName!, "%" + query.FirstName + "%") ||
                            query.FirstNameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.FirstName!, query.FirstName + "%") ||
                            query.FirstNameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.FirstName!, "%" + query.FirstName))
                    &&
                    (string.IsNullOrEmpty(query.MiddleName) ||
                            query.MiddleNameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.MiddleName!, "%" + query.MiddleName + "%") ||
                            query.MiddleNameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.MiddleName!, query.MiddleName + "%") ||
                            query.MiddleNameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.MiddleName!, "%" + query.MiddleName))
                    &&
                    (string.IsNullOrEmpty(query.LastName) ||
                            query.LastNameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.LastName!, "%" + query.LastName + "%") ||
                            query.LastNameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.LastName!, query.LastName + "%") ||
                            query.LastNameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.LastName!, "%" + query.LastName))
                    &&
                    (string.IsNullOrEmpty(query.Suffix) ||
                            query.SuffixSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Suffix!, "%" + query.Suffix + "%") ||
                            query.SuffixSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Suffix!, query.Suffix + "%") ||
                            query.SuffixSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Suffix!, "%" + query.Suffix))
                    &&
                    (string.IsNullOrEmpty(query.CompanyName) ||
                            query.CompanyNameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.CompanyName!, "%" + query.CompanyName + "%") ||
                            query.CompanyNameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.CompanyName!, query.CompanyName + "%") ||
                            query.CompanyNameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.CompanyName!, "%" + query.CompanyName))
                    &&
                    (string.IsNullOrEmpty(query.SalesPerson) ||
                            query.SalesPersonSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.SalesPerson!, "%" + query.SalesPerson + "%") ||
                            query.SalesPersonSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.SalesPerson!, query.SalesPerson + "%") ||
                            query.SalesPersonSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.SalesPerson!, "%" + query.SalesPerson))
                    &&
                    (string.IsNullOrEmpty(query.EmailAddress) ||
                            query.EmailAddressSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.EmailAddress!, "%" + query.EmailAddress + "%") ||
                            query.EmailAddressSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.EmailAddress!, query.EmailAddress + "%") ||
                            query.EmailAddressSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.EmailAddress!, "%" + query.EmailAddress))
                    &&
                    (string.IsNullOrEmpty(query.Phone) ||
                            query.PhoneSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Phone!, "%" + query.Phone + "%") ||
                            query.PhoneSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Phone!, query.Phone + "%") ||
                            query.PhoneSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Phone!, "%" + query.Phone))
                    &&
                    (string.IsNullOrEmpty(query.PasswordHash) ||
                            query.PasswordHashSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.PasswordHash!, "%" + query.PasswordHash + "%") ||
                            query.PasswordHashSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.PasswordHash!, query.PasswordHash + "%") ||
                            query.PasswordHashSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.PasswordHash!, "%" + query.PasswordHash))
                    &&
                    (string.IsNullOrEmpty(query.PasswordSalt) ||
                            query.PasswordSaltSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.PasswordSalt!, "%" + query.PasswordSalt + "%") ||
                            query.PasswordSaltSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.PasswordSalt!, query.PasswordSalt + "%") ||
                            query.PasswordSaltSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.PasswordSalt!, "%" + query.PasswordSalt))

                select new NameValuePair
                {

                        Value = t.CustomerID.ToString(),
                        Name = t.Title,
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
            CustomerAdvancedQuery query)
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

