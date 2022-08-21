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
    public class AddressRepository
        : IAddressRepository
    {
        private readonly ILogger<AddressRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public AddressRepository(EFDbContext dbcontext, ILogger<AddressRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<AddressDataModel> SearchQuery(
            AddressAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.Address

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.AddressLine1!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.AddressLine2!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.City!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.StateProvince!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.CountryRegion!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.PostalCode!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.AddressLine1!, query.TextSearch + "%") || EF.Functions.Like(t.AddressLine2!, query.TextSearch + "%") || EF.Functions.Like(t.City!, query.TextSearch + "%") || EF.Functions.Like(t.StateProvince!, query.TextSearch + "%") || EF.Functions.Like(t.CountryRegion!, query.TextSearch + "%") || EF.Functions.Like(t.PostalCode!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.AddressLine1!, "%" + query.TextSearch) || EF.Functions.Like(t.AddressLine2!, "%" + query.TextSearch) || EF.Functions.Like(t.City!, "%" + query.TextSearch) || EF.Functions.Like(t.StateProvince!, "%" + query.TextSearch) || EF.Functions.Like(t.CountryRegion!, "%" + query.TextSearch) || EF.Functions.Like(t.PostalCode!, "%" + query.TextSearch)))
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.AddressLine1) ||
                            query.AddressLine1SearchType == TextSearchTypes.Contains && EF.Functions.Like(t.AddressLine1!, "%" + query.AddressLine1 + "%") ||
                            query.AddressLine1SearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.AddressLine1!, query.AddressLine1 + "%") ||
                            query.AddressLine1SearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.AddressLine1!, "%" + query.AddressLine1))
                    &&
                    (string.IsNullOrEmpty(query.AddressLine2) ||
                            query.AddressLine2SearchType == TextSearchTypes.Contains && EF.Functions.Like(t.AddressLine2!, "%" + query.AddressLine2 + "%") ||
                            query.AddressLine2SearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.AddressLine2!, query.AddressLine2 + "%") ||
                            query.AddressLine2SearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.AddressLine2!, "%" + query.AddressLine2))
                    &&
                    (string.IsNullOrEmpty(query.City) ||
                            query.CitySearchType == TextSearchTypes.Contains && EF.Functions.Like(t.City!, "%" + query.City + "%") ||
                            query.CitySearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.City!, query.City + "%") ||
                            query.CitySearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.City!, "%" + query.City))
                    &&
                    (string.IsNullOrEmpty(query.StateProvince) ||
                            query.StateProvinceSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.StateProvince!, "%" + query.StateProvince + "%") ||
                            query.StateProvinceSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.StateProvince!, query.StateProvince + "%") ||
                            query.StateProvinceSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.StateProvince!, "%" + query.StateProvince))
                    &&
                    (string.IsNullOrEmpty(query.CountryRegion) ||
                            query.CountryRegionSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.CountryRegion!, "%" + query.CountryRegion + "%") ||
                            query.CountryRegionSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.CountryRegion!, query.CountryRegion + "%") ||
                            query.CountryRegionSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.CountryRegion!, "%" + query.CountryRegion))
                    &&
                    (string.IsNullOrEmpty(query.PostalCode) ||
                            query.PostalCodeSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.PostalCode!, "%" + query.PostalCode + "%") ||
                            query.PostalCodeSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.PostalCode!, query.PostalCode + "%") ||
                            query.PostalCodeSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.PostalCode!, "%" + query.PostalCode))

                select new AddressDataModel
                {

                        AddressID = t.AddressID,
                        AddressLine1 = t.AddressLine1,
                        AddressLine2 = t.AddressLine2,
                        City = t.City,
                        StateProvince = t.StateProvince,
                        CountryRegion = t.CountryRegion,
                        PostalCode = t.PostalCode,
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

        public async Task<PagedResponse<AddressDataModel[]>> Search(
            AddressAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<AddressDataModel>();
                return new PagedResponse<AddressDataModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<AddressDataModel[]>>.FromResult(new PagedResponse<AddressDataModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<Address> GetIQueryableByPrimaryIdentifierList(
            List<AddressIdentifier> ids)
        {
            var idList = ids.Select(t => t.AddressID).ToList();
            var queryable =
                from t in _dbcontext.Address
                where idList.Contains(t.AddressID)
                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<AddressIdentifier> ids)
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

        public async Task<Response<MultiItemsCUDModel<AddressIdentifier, AddressDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<AddressIdentifier, AddressDataModel> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDModel<AddressIdentifier, AddressDataModel>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDModel<AddressIdentifier, AddressDataModel>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<Address> newEFItems = new List<Address>();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new Address
                        {
                            AddressLine1 = item.AddressLine1,
                            AddressLine2 = item.AddressLine2,
                            City = item.City,
                            StateProvince = item.StateProvince,
                            CountryRegion = item.CountryRegion,
                            PostalCode = item.PostalCode,
                            ModifiedDate = item.ModifiedDate,
                        };
                        _dbcontext.Address.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.Address
                             where

                             t.AddressID == item.AddressID
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.AddressLine1 = item.AddressLine1;
                existing.AddressLine2 = item.AddressLine2;
                existing.City = item.City;
                existing.StateProvince = item.StateProvince;
                existing.CountryRegion = item.CountryRegion;
                existing.PostalCode = item.PostalCode;
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
                        select t.AddressID);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.AddressID);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.Address
                    where identifierListToloadResponseItems.Contains(t.AddressID)

                    select new AddressDataModel
                    {

                        AddressID = t.AddressID,
                        AddressLine1 = t.AddressLine1,
                        AddressLine2 = t.AddressLine2,
                        City = t.City,
                        StateProvince = t.StateProvince,
                        CountryRegion = t.CountryRegion,
                        PostalCode = t.PostalCode,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDModel<AddressIdentifier, AddressDataModel>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDModel<AddressIdentifier, AddressDataModel>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.AddressID == t.AddressID)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.AddressID == t.AddressID)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDModel<AddressIdentifier, AddressDataModel>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<AddressDataModel>> Update(AddressIdentifier id, AddressDataModel input)
        {
            if (input == null)
                return await Task<Response<AddressDataModel>>.FromResult(new Response<AddressDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.Address
                     where

                    t.AddressID == id.AddressID
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<AddressDataModel>>.FromResult(new Response<AddressDataModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.AddressLine1 = input.AddressLine1;
                existing.AddressLine2 = input.AddressLine2;
                existing.City = input.City;
                existing.StateProvince = input.StateProvince;
                existing.CountryRegion = input.CountryRegion;
                existing.PostalCode = input.PostalCode;
                existing.ModifiedDate = input.ModifiedDate;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<AddressDataModel>>.FromResult(
                    new Response<AddressDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new AddressDataModel
                        {
                            AddressID = existing.AddressID,
                            AddressLine1 = existing.AddressLine1,
                            AddressLine2 = existing.AddressLine2,
                            City = existing.City,
                            StateProvince = existing.StateProvince,
                            CountryRegion = existing.CountryRegion,
                            PostalCode = existing.PostalCode,
                            rowguid = existing.rowguid,
                            ModifiedDate = existing.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<AddressDataModel>>.FromResult(new Response<AddressDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<AddressDataModel>> Get(AddressIdentifier id)
        {
            if (id == null)
                return await Task<Response<AddressDataModel>>.FromResult(new Response<AddressDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.Address.SingleOrDefault(
                    t =>

                    t.AddressID == id.AddressID
                );

                if (existing == null)
                    return await Task<Response<AddressDataModel>>.FromResult(new Response<AddressDataModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<AddressDataModel>>.FromResult(
                    new Response<AddressDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new AddressDataModel
                        {
                            AddressID = existing.AddressID,
                            AddressLine1 = existing.AddressLine1,
                            AddressLine2 = existing.AddressLine2,
                            City = existing.City,
                            StateProvince = existing.StateProvince,
                            CountryRegion = existing.CountryRegion,
                            PostalCode = existing.PostalCode,
                            rowguid = existing.rowguid,
                            ModifiedDate = existing.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<AddressDataModel>>.FromResult(new Response<AddressDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<AddressDataModel>> Create(AddressDataModel input)
        {
            if (input == null)
                return await Task<Response<AddressDataModel>>.FromResult(new Response<AddressDataModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new Address
                {
                            AddressLine1 = input.AddressLine1,
                            AddressLine2 = input.AddressLine2,
                            City = input.City,
                            StateProvince = input.StateProvince,
                            CountryRegion = input.CountryRegion,
                            PostalCode = input.PostalCode,
                            ModifiedDate = input.ModifiedDate,
                };
                await _dbcontext.Address.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<AddressDataModel>>.FromResult(
                    new Response<AddressDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new AddressDataModel
                        {
                            AddressID = toInsert.AddressID,
                            AddressLine1 = toInsert.AddressLine1,
                            AddressLine2 = toInsert.AddressLine2,
                            City = toInsert.City,
                            StateProvince = toInsert.StateProvince,
                            CountryRegion = toInsert.CountryRegion,
                            PostalCode = toInsert.PostalCode,
                            rowguid = toInsert.rowguid,
                            ModifiedDate = toInsert.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<AddressDataModel>>.FromResult(new Response<AddressDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(AddressIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.Address
                     where

                    t.AddressID == id.AddressID
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.Address.Remove(existing);
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
            AddressAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.Address

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.AddressLine1!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.AddressLine2!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.City!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.StateProvince!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.CountryRegion!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.PostalCode!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.AddressLine1!, query.TextSearch + "%") || EF.Functions.Like(t.AddressLine2!, query.TextSearch + "%") || EF.Functions.Like(t.City!, query.TextSearch + "%") || EF.Functions.Like(t.StateProvince!, query.TextSearch + "%") || EF.Functions.Like(t.CountryRegion!, query.TextSearch + "%") || EF.Functions.Like(t.PostalCode!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.AddressLine1!, "%" + query.TextSearch) || EF.Functions.Like(t.AddressLine2!, "%" + query.TextSearch) || EF.Functions.Like(t.City!, "%" + query.TextSearch) || EF.Functions.Like(t.StateProvince!, "%" + query.TextSearch) || EF.Functions.Like(t.CountryRegion!, "%" + query.TextSearch) || EF.Functions.Like(t.PostalCode!, "%" + query.TextSearch)))
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.AddressLine1) ||
                            query.AddressLine1SearchType == TextSearchTypes.Contains && EF.Functions.Like(t.AddressLine1!, "%" + query.AddressLine1 + "%") ||
                            query.AddressLine1SearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.AddressLine1!, query.AddressLine1 + "%") ||
                            query.AddressLine1SearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.AddressLine1!, "%" + query.AddressLine1))
                    &&
                    (string.IsNullOrEmpty(query.AddressLine2) ||
                            query.AddressLine2SearchType == TextSearchTypes.Contains && EF.Functions.Like(t.AddressLine2!, "%" + query.AddressLine2 + "%") ||
                            query.AddressLine2SearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.AddressLine2!, query.AddressLine2 + "%") ||
                            query.AddressLine2SearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.AddressLine2!, "%" + query.AddressLine2))
                    &&
                    (string.IsNullOrEmpty(query.City) ||
                            query.CitySearchType == TextSearchTypes.Contains && EF.Functions.Like(t.City!, "%" + query.City + "%") ||
                            query.CitySearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.City!, query.City + "%") ||
                            query.CitySearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.City!, "%" + query.City))
                    &&
                    (string.IsNullOrEmpty(query.StateProvince) ||
                            query.StateProvinceSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.StateProvince!, "%" + query.StateProvince + "%") ||
                            query.StateProvinceSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.StateProvince!, query.StateProvince + "%") ||
                            query.StateProvinceSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.StateProvince!, "%" + query.StateProvince))
                    &&
                    (string.IsNullOrEmpty(query.CountryRegion) ||
                            query.CountryRegionSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.CountryRegion!, "%" + query.CountryRegion + "%") ||
                            query.CountryRegionSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.CountryRegion!, query.CountryRegion + "%") ||
                            query.CountryRegionSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.CountryRegion!, "%" + query.CountryRegion))
                    &&
                    (string.IsNullOrEmpty(query.PostalCode) ||
                            query.PostalCodeSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.PostalCode!, "%" + query.PostalCode + "%") ||
                            query.PostalCodeSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.PostalCode!, query.PostalCode + "%") ||
                            query.PostalCodeSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.PostalCode!, "%" + query.PostalCode))

                select new NameValuePair
                {

                        Value = t.AddressID.ToString(),
                        Name = t.AddressLine1,
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

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            AddressAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = GetCodeListQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = GetCodeListQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<NameValuePair>();
                return new PagedResponse<NameValuePair[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<NameValuePair[]>>.FromResult(new PagedResponse<NameValuePair[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

    }
}

