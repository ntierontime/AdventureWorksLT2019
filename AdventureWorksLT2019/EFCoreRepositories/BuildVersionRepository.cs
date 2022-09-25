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
    public class BuildVersionRepository
        : IBuildVersionRepository
    {
        private readonly ILogger<BuildVersionRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public BuildVersionRepository(EFDbContext dbcontext, ILogger<BuildVersionRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<BuildVersionDataModel> SearchQuery(
            BuildVersionAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.BuildVersion

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Database_Version!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Database_Version!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Database_Version!, "%" + query.TextSearch)))
                    &&

                    (!query.VersionDateRangeLower.HasValue && !query.VersionDateRangeUpper.HasValue || (!query.VersionDateRangeLower.HasValue || t.VersionDate >= query.VersionDateRangeLower) && (!query.VersionDateRangeLower.HasValue || t.VersionDate <= query.VersionDateRangeUpper))
                    &&
                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Database_Version) ||
                            query.Database_VersionSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Database_Version!, "%" + query.Database_Version + "%") ||
                            query.Database_VersionSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Database_Version!, query.Database_Version + "%") ||
                            query.Database_VersionSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Database_Version!, "%" + query.Database_Version))

                select new BuildVersionDataModel
                {

                        SystemInformationID = t.SystemInformationID,
                        Database_Version = t.Database_Version,
                        VersionDate = t.VersionDate,
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

        public async Task<ListResponse<BuildVersionDataModel[]>> Search(
            BuildVersionAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<BuildVersionDataModel>();
                return new ListResponse<BuildVersionDataModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<BuildVersionDataModel[]>>.FromResult(new ListResponse<BuildVersionDataModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        public async Task<Response<BuildVersionDataModel>> Get(BuildVersionIdentifier id)
        {
            if (id == null)
                return await Task<Response<BuildVersionDataModel>>.FromResult(new Response<BuildVersionDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.BuildVersion.SingleOrDefault(
                    t =>

                    t.SystemInformationID == id.SystemInformationID
                    &&
                    t.VersionDate == id.VersionDate
                    &&
                    t.ModifiedDate == id.ModifiedDate
                );

                if (existing == null)
                    return await Task<Response<BuildVersionDataModel>>.FromResult(new Response<BuildVersionDataModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<BuildVersionDataModel>>.FromResult(
                    new Response<BuildVersionDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new BuildVersionDataModel
                        {
                            SystemInformationID = existing.SystemInformationID,
                            Database_Version = existing.Database_Version,
                            VersionDate = existing.VersionDate,
                            ModifiedDate = existing.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<BuildVersionDataModel>>.FromResult(new Response<BuildVersionDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private IQueryable<NameValuePair> GetCodeListQuery(
            BuildVersionAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.BuildVersion

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Database_Version!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Database_Version!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Database_Version!, "%" + query.TextSearch)))
                    &&

                    (!query.VersionDateRangeLower.HasValue && !query.VersionDateRangeUpper.HasValue || (!query.VersionDateRangeLower.HasValue || t.VersionDate >= query.VersionDateRangeLower) && (!query.VersionDateRangeLower.HasValue || t.VersionDate <= query.VersionDateRangeUpper))
                    &&
                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Database_Version) ||
                            query.Database_VersionSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Database_Version!, "%" + query.Database_Version + "%") ||
                            query.Database_VersionSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Database_Version!, query.Database_Version + "%") ||
                            query.Database_VersionSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Database_Version!, "%" + query.Database_Version))
                let _Value = string.Concat(new string[] { t.SystemInformationID.ToString(),"|",t.VersionDate.ToString(),"|",t.ModifiedDate.ToString() })
                select new NameValuePair
                {

                        Name = t.Database_Version,
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
            BuildVersionAdvancedQuery query)
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

