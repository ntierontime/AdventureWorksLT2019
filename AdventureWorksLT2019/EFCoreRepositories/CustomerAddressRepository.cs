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
    public class CustomerAddressRepository
        : ICustomerAddressRepository
    {
        private readonly ILogger<CustomerAddressRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public CustomerAddressRepository(EFDbContext dbcontext, ILogger<CustomerAddressRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<CustomerAddressDataModel.DefaultView> SearchQuery(
            CustomerAddressAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.CustomerAddress

                    join Address in _dbcontext.Address on t.AddressID equals Address.AddressID// \AddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.AddressType!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.AddressType!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.AddressType!, "%" + query.TextSearch)))
                    &&

                    (!query.AddressID.HasValue || Address.AddressID == query.AddressID)
                    &&
                    (!query.CustomerID.HasValue || Customer.CustomerID == query.CustomerID)
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.AddressType) ||
                            query.AddressTypeSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.AddressType!, "%" + query.AddressType + "%") ||
                            query.AddressTypeSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.AddressType!, query.AddressType + "%") ||
                            query.AddressTypeSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.AddressType!, "%" + query.AddressType))

                select new CustomerAddressDataModel.DefaultView
                {

                        CustomerID = t.CustomerID,
                        AddressID = t.AddressID,
                        AddressType = t.AddressType,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        Address_Name = Address.AddressLine1,
                        Customer_Name = Customer.Title,
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

        public async Task<PagedResponse<CustomerAddressDataModel.DefaultView[]>> Search(
            CustomerAddressAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<CustomerAddressDataModel.DefaultView>();
                return new PagedResponse<CustomerAddressDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<CustomerAddressDataModel.DefaultView[]>>.FromResult(new PagedResponse<CustomerAddressDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        public async Task<Response<CustomerAddressDataModel.DefaultView>> Get(CustomerAddressIdentifier id)
        {
            if (id == null)
                return await Task<Response<CustomerAddressDataModel.DefaultView>>.FromResult(new Response<CustomerAddressDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {

                var responseBody =
                    (
                    from t in _dbcontext.CustomerAddress

                    join Address in _dbcontext.Address on t.AddressID equals Address.AddressID// \AddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                    where

                    t.CustomerID == id.CustomerID
                    &&
                    t.AddressID == id.AddressID

                    select new CustomerAddressDataModel.DefaultView
                    {

                        CustomerID = t.CustomerID,
                        AddressID = t.AddressID,
                        AddressType = t.AddressType,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        Address_Name = Address.AddressLine1,
                        Customer_Name = Customer.Title,

                    }).First();
                if (responseBody == null)
                    return await Task<Response<CustomerAddressDataModel.DefaultView>>.FromResult(new Response<CustomerAddressDataModel.DefaultView> { Status = HttpStatusCode.NotFound });
                return await Task<Response<CustomerAddressDataModel.DefaultView>>.FromResult(
                    new Response<CustomerAddressDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<CustomerAddressDataModel.DefaultView>>.FromResult(new Response<CustomerAddressDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

    }
}

