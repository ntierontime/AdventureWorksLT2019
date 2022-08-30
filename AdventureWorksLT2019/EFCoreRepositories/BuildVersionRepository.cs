using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.EFCoreContext;
using AdventureWorksLT2019.Models;
using Framework.Helpers;
using Framework.Common;
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

        private IQueryable<BuildVersion> GetIQueryableByPrimaryIdentifierList(
            List<BuildVersionIdentifier> ids)
        {
            var queryable =
                from t in _dbcontext.BuildVersion

                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<BuildVersionIdentifier> ids)
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

        public async Task<Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>>> MultiItemsCUD(
            MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<BuildVersion> newEFItems = new List<BuildVersion>();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new BuildVersion
                        {
                            Database_Version = item.Database_Version,
                            VersionDate = item.VersionDate,
                            ModifiedDate = item.ModifiedDate,
                        };
                        _dbcontext.BuildVersion.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.BuildVersion
                             where

                             t.SystemInformationID == item.SystemInformationID
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Database_Version = item.Database_Version;
                existing.VersionDate = item.VersionDate;
                existing.ModifiedDate = item.ModifiedDate;
                        }
                    }
                }
                await _dbcontext.SaveChangesAsync();

                // 3.2 Load Response
                var identifierListToloadResponseItems = new List<byte>();

                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in newEFItems
                        select t.SystemInformationID);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.SystemInformationID);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.BuildVersion
                    where identifierListToloadResponseItems.Contains(t.SystemInformationID)

                    select new BuildVersionDataModel
                    {

                        SystemInformationID = t.SystemInformationID,
                        Database_Version = t.Database_Version,
                        VersionDate = t.VersionDate,
                        ModifiedDate = t.ModifiedDate,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.SystemInformationID == t.SystemInformationID)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.SystemInformationID == t.SystemInformationID)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<BuildVersionDataModel>> Update(BuildVersionIdentifier id, BuildVersionDataModel input)
        {
            if (input == null)
                return await Task<Response<BuildVersionDataModel>>.FromResult(new Response<BuildVersionDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.BuildVersion
                     where

                    t.SystemInformationID == id.SystemInformationID
                    &&
                    t.VersionDate == id.VersionDate
                    &&
                    t.ModifiedDate == id.ModifiedDate
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<BuildVersionDataModel>>.FromResult(new Response<BuildVersionDataModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Database_Version = input.Database_Version;
                existing.VersionDate = input.VersionDate;
                existing.ModifiedDate = input.ModifiedDate;
                await _dbcontext.SaveChangesAsync();

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

        public async Task<Response<BuildVersionDataModel>> Create(BuildVersionDataModel input)
        {
            if (input == null)
                return await Task<Response<BuildVersionDataModel>>.FromResult(new Response<BuildVersionDataModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new BuildVersion
                {
                            Database_Version = input.Database_Version,
                            VersionDate = input.VersionDate,
                            ModifiedDate = input.ModifiedDate,
                };
                await _dbcontext.BuildVersion.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<BuildVersionDataModel>>.FromResult(
                    new Response<BuildVersionDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new BuildVersionDataModel
                        {
                            SystemInformationID = toInsert.SystemInformationID,
                            Database_Version = toInsert.Database_Version,
                            VersionDate = toInsert.VersionDate,
                            ModifiedDate = toInsert.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<BuildVersionDataModel>>.FromResult(new Response<BuildVersionDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(BuildVersionIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.BuildVersion
                     where

                    t.SystemInformationID == id.SystemInformationID
                    &&
                    t.VersionDate == id.VersionDate
                    &&
                    t.ModifiedDate == id.ModifiedDate
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.BuildVersion.Remove(existing);
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

