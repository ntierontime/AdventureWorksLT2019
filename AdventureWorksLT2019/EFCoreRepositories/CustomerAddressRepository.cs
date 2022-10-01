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

        public async Task<ListResponse<CustomerAddressDataModel.DefaultView[]>> Search(
            CustomerAddressAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<CustomerAddressDataModel.DefaultView>();
                return new ListResponse<CustomerAddressDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<CustomerAddressDataModel.DefaultView[]>>.FromResult(new ListResponse<CustomerAddressDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<CustomerAddress> GetIQueryableByPrimaryIdentifierList(
            List<CustomerAddressIdentifier> ids)
        {
            var queryable =
                from t in _dbcontext.CustomerAddress

                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<CustomerAddressIdentifier> ids)
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

        public async Task<Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<CustomerAddress> newEFItems = new();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new CustomerAddress
                        {
                            CustomerID = item.CustomerID,
                            AddressID = item.AddressID,
                            AddressType = item.AddressType,
                            ModifiedDate = item.ModifiedDate,
                        };
                        _dbcontext.CustomerAddress.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.CustomerAddress
                             where

                             t.CustomerID == item.CustomerID
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                            existing.CustomerID = item.CustomerID;
                            existing.AddressID = item.AddressID;
                            existing.AddressType = item.AddressType;
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
                        select t.CustomerID);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.CustomerID);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.CustomerAddress
                    join Address in _dbcontext.Address on t.AddressID equals Address.AddressID// \AddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                    where identifierListToloadResponseItems.Contains(t.CustomerID)

                    select new CustomerAddressDataModel.DefaultView
                    {

                        CustomerID = t.CustomerID,
                        AddressID = t.AddressID,
                        AddressType = t.AddressType,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        Address_Name = Address.AddressLine1,
                        Customer_Name = Customer.Title,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.CustomerID == t.CustomerID)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.CustomerID == t.CustomerID)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<CustomerAddressDataModel.DefaultView>> Update(CustomerAddressIdentifier id, CustomerAddressDataModel input)
        {
            if (input == null)
                return await Task<Response<CustomerAddressDataModel.DefaultView>>.FromResult(new Response<CustomerAddressDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.CustomerAddress
                     where

                    t.CustomerID == id.CustomerID
                    &&
                    t.AddressID == id.AddressID
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<CustomerAddressDataModel.DefaultView>>.FromResult(new Response<CustomerAddressDataModel.DefaultView> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.CustomerID = input.CustomerID;
                existing.AddressID = input.AddressID;
                existing.AddressType = input.AddressType;
                existing.ModifiedDate = input.ModifiedDate;
                await _dbcontext.SaveChangesAsync();

                var responseBody =
                    (
                    from t in _dbcontext.CustomerAddress

                    join Address in _dbcontext.Address on t.AddressID equals Address.AddressID// \AddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                    where t.CustomerID == existing.CustomerID && t.AddressID == existing.AddressID

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

        public async Task<Response<CustomerAddressDataModel.DefaultView>> Create(CustomerAddressDataModel input)
        {
            if (input == null)
                return await Task<Response<CustomerAddressDataModel.DefaultView>>.FromResult(new Response<CustomerAddressDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new CustomerAddress
                {
                            CustomerID = input.CustomerID,
                            AddressID = input.AddressID,
                            AddressType = input.AddressType,
                            ModifiedDate = input.ModifiedDate,
                };
                await _dbcontext.CustomerAddress.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                var responseBody =
                    (
                    from t in _dbcontext.CustomerAddress

                    join Address in _dbcontext.Address on t.AddressID equals Address.AddressID// \AddressID
                    join Customer in _dbcontext.Customer on t.CustomerID equals Customer.CustomerID// \CustomerID
                    where t.CustomerID == toInsert.CustomerID && t.AddressID == toInsert.AddressID

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

        public async Task<Response> Delete(CustomerAddressIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.CustomerAddress
                     where

                    t.CustomerID == id.CustomerID
                    &&
                    t.AddressID == id.AddressID
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.CustomerAddress.Remove(existing);
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
                let _Value = string.Concat(new string[] { t.CustomerID.ToString(),"|",t.AddressID.ToString() })
                select new NameValuePair
                {

                        Name = t.AddressType,
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
            CustomerAddressAdvancedQuery query)
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

