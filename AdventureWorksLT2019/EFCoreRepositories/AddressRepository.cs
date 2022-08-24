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

    }
}

