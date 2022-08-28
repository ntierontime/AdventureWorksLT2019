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
    public class ErrorLogRepository
        : IErrorLogRepository
    {
        private readonly ILogger<ErrorLogRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ErrorLogRepository(EFDbContext dbcontext, ILogger<ErrorLogRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<ErrorLogDataModel> SearchQuery(
            ErrorLogAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ErrorLog

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.UserName!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.ErrorProcedure!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.ErrorMessage!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.UserName!, query.TextSearch + "%") || EF.Functions.Like(t.ErrorProcedure!, query.TextSearch + "%") || EF.Functions.Like(t.ErrorMessage!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.UserName!, "%" + query.TextSearch) || EF.Functions.Like(t.ErrorProcedure!, "%" + query.TextSearch) || EF.Functions.Like(t.ErrorMessage!, "%" + query.TextSearch)))
                    &&

                    (!query.ErrorTimeRangeLower.HasValue && !query.ErrorTimeRangeUpper.HasValue || (!query.ErrorTimeRangeLower.HasValue || t.ErrorTime >= query.ErrorTimeRangeLower) && (!query.ErrorTimeRangeLower.HasValue || t.ErrorTime <= query.ErrorTimeRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.UserName) ||
                            query.UserNameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.UserName!, "%" + query.UserName + "%") ||
                            query.UserNameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.UserName!, query.UserName + "%") ||
                            query.UserNameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.UserName!, "%" + query.UserName))
                    &&
                    (string.IsNullOrEmpty(query.ErrorProcedure) ||
                            query.ErrorProcedureSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.ErrorProcedure!, "%" + query.ErrorProcedure + "%") ||
                            query.ErrorProcedureSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.ErrorProcedure!, query.ErrorProcedure + "%") ||
                            query.ErrorProcedureSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.ErrorProcedure!, "%" + query.ErrorProcedure))
                    &&
                    (string.IsNullOrEmpty(query.ErrorMessage) ||
                            query.ErrorMessageSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.ErrorMessage!, "%" + query.ErrorMessage + "%") ||
                            query.ErrorMessageSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.ErrorMessage!, query.ErrorMessage + "%") ||
                            query.ErrorMessageSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.ErrorMessage!, "%" + query.ErrorMessage))

                select new ErrorLogDataModel
                {

                        ErrorLogID = t.ErrorLogID,
                        ErrorTime = t.ErrorTime,
                        UserName = t.UserName,
                        ErrorNumber = t.ErrorNumber,
                        ErrorSeverity = t.ErrorSeverity,
                        ErrorState = t.ErrorState,
                        ErrorProcedure = t.ErrorProcedure,
                        ErrorLine = t.ErrorLine,
                        ErrorMessage = t.ErrorMessage,
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

        public async Task<PagedResponse<ErrorLogDataModel[]>> Search(
            ErrorLogAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ErrorLogDataModel>();
                return new PagedResponse<ErrorLogDataModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ErrorLogDataModel[]>>.FromResult(new PagedResponse<ErrorLogDataModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<ErrorLog> GetIQueryableByPrimaryIdentifierList(
            List<ErrorLogIdentifier> ids)
        {
            var idList = ids.Select(t => t.ErrorLogID).ToList();
            var queryable =
                from t in _dbcontext.ErrorLog
                where idList.Contains(t.ErrorLogID)
                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<ErrorLogIdentifier> ids)
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

        public async Task<Response<MultiItemsCUDModel<ErrorLogIdentifier, ErrorLogDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ErrorLogIdentifier, ErrorLogDataModel> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDModel<ErrorLogIdentifier, ErrorLogDataModel>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDModel<ErrorLogIdentifier, ErrorLogDataModel>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<ErrorLog> newEFItems = new List<ErrorLog>();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new ErrorLog
                        {
                            ErrorTime = item.ErrorTime,
                            UserName = item.UserName,
                            ErrorNumber = item.ErrorNumber,
                            ErrorSeverity = item.ErrorSeverity,
                            ErrorState = item.ErrorState,
                            ErrorProcedure = item.ErrorProcedure,
                            ErrorLine = item.ErrorLine,
                            ErrorMessage = item.ErrorMessage,
                        };
                        _dbcontext.ErrorLog.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.ErrorLog
                             where

                             t.ErrorLogID == item.ErrorLogID
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.ErrorTime = item.ErrorTime;
                existing.UserName = item.UserName;
                existing.ErrorNumber = item.ErrorNumber;
                existing.ErrorSeverity = item.ErrorSeverity;
                existing.ErrorState = item.ErrorState;
                existing.ErrorProcedure = item.ErrorProcedure;
                existing.ErrorLine = item.ErrorLine;
                existing.ErrorMessage = item.ErrorMessage;
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
                        select t.ErrorLogID);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.ErrorLogID);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.ErrorLog
                    where identifierListToloadResponseItems.Contains(t.ErrorLogID)

                    select new ErrorLogDataModel
                    {

                        ErrorLogID = t.ErrorLogID,
                        ErrorTime = t.ErrorTime,
                        UserName = t.UserName,
                        ErrorNumber = t.ErrorNumber,
                        ErrorSeverity = t.ErrorSeverity,
                        ErrorState = t.ErrorState,
                        ErrorProcedure = t.ErrorProcedure,
                        ErrorLine = t.ErrorLine,
                        ErrorMessage = t.ErrorMessage,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDModel<ErrorLogIdentifier, ErrorLogDataModel>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDModel<ErrorLogIdentifier, ErrorLogDataModel>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.ErrorLogID == t.ErrorLogID)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.ErrorLogID == t.ErrorLogID)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDModel<ErrorLogIdentifier, ErrorLogDataModel>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<ErrorLogDataModel>> Update(ErrorLogIdentifier id, ErrorLogDataModel input)
        {
            if (input == null)
                return await Task<Response<ErrorLogDataModel>>.FromResult(new Response<ErrorLogDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ErrorLog
                     where

                    t.ErrorLogID == id.ErrorLogID
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ErrorLogDataModel>>.FromResult(new Response<ErrorLogDataModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.ErrorTime = input.ErrorTime;
                existing.UserName = input.UserName;
                existing.ErrorNumber = input.ErrorNumber;
                existing.ErrorSeverity = input.ErrorSeverity;
                existing.ErrorState = input.ErrorState;
                existing.ErrorProcedure = input.ErrorProcedure;
                existing.ErrorLine = input.ErrorLine;
                existing.ErrorMessage = input.ErrorMessage;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ErrorLogDataModel>>.FromResult(
                    new Response<ErrorLogDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ErrorLogDataModel
                        {
                            ErrorLogID = existing.ErrorLogID,
                            ErrorTime = existing.ErrorTime,
                            UserName = existing.UserName,
                            ErrorNumber = existing.ErrorNumber,
                            ErrorSeverity = existing.ErrorSeverity,
                            ErrorState = existing.ErrorState,
                            ErrorProcedure = existing.ErrorProcedure,
                            ErrorLine = existing.ErrorLine,
                            ErrorMessage = existing.ErrorMessage,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ErrorLogDataModel>>.FromResult(new Response<ErrorLogDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ErrorLogDataModel>> Get(ErrorLogIdentifier id)
        {
            if (id == null)
                return await Task<Response<ErrorLogDataModel>>.FromResult(new Response<ErrorLogDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ErrorLog.SingleOrDefault(
                    t =>

                    t.ErrorLogID == id.ErrorLogID
                );

                if (existing == null)
                    return await Task<Response<ErrorLogDataModel>>.FromResult(new Response<ErrorLogDataModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ErrorLogDataModel>>.FromResult(
                    new Response<ErrorLogDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ErrorLogDataModel
                        {
                            ErrorLogID = existing.ErrorLogID,
                            ErrorTime = existing.ErrorTime,
                            UserName = existing.UserName,
                            ErrorNumber = existing.ErrorNumber,
                            ErrorSeverity = existing.ErrorSeverity,
                            ErrorState = existing.ErrorState,
                            ErrorProcedure = existing.ErrorProcedure,
                            ErrorLine = existing.ErrorLine,
                            ErrorMessage = existing.ErrorMessage,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ErrorLogDataModel>>.FromResult(new Response<ErrorLogDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ErrorLogDataModel>> Create(ErrorLogDataModel input)
        {
            if (input == null)
                return await Task<Response<ErrorLogDataModel>>.FromResult(new Response<ErrorLogDataModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ErrorLog
                {
                            ErrorTime = input.ErrorTime,
                            UserName = input.UserName,
                            ErrorNumber = input.ErrorNumber,
                            ErrorSeverity = input.ErrorSeverity,
                            ErrorState = input.ErrorState,
                            ErrorProcedure = input.ErrorProcedure,
                            ErrorLine = input.ErrorLine,
                            ErrorMessage = input.ErrorMessage,
                };
                await _dbcontext.ErrorLog.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ErrorLogDataModel>>.FromResult(
                    new Response<ErrorLogDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ErrorLogDataModel
                        {
                            ErrorLogID = toInsert.ErrorLogID,
                            ErrorTime = toInsert.ErrorTime,
                            UserName = toInsert.UserName,
                            ErrorNumber = toInsert.ErrorNumber,
                            ErrorSeverity = toInsert.ErrorSeverity,
                            ErrorState = toInsert.ErrorState,
                            ErrorProcedure = toInsert.ErrorProcedure,
                            ErrorLine = toInsert.ErrorLine,
                            ErrorMessage = toInsert.ErrorMessage,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ErrorLogDataModel>>.FromResult(new Response<ErrorLogDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ErrorLogIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ErrorLog
                     where

                    t.ErrorLogID == id.ErrorLogID
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ErrorLog.Remove(existing);
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
            ErrorLogAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ErrorLog

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.UserName!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.ErrorProcedure!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.ErrorMessage!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.UserName!, query.TextSearch + "%") || EF.Functions.Like(t.ErrorProcedure!, query.TextSearch + "%") || EF.Functions.Like(t.ErrorMessage!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.UserName!, "%" + query.TextSearch) || EF.Functions.Like(t.ErrorProcedure!, "%" + query.TextSearch) || EF.Functions.Like(t.ErrorMessage!, "%" + query.TextSearch)))
                    &&

                    (!query.ErrorTimeRangeLower.HasValue && !query.ErrorTimeRangeUpper.HasValue || (!query.ErrorTimeRangeLower.HasValue || t.ErrorTime >= query.ErrorTimeRangeLower) && (!query.ErrorTimeRangeLower.HasValue || t.ErrorTime <= query.ErrorTimeRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.UserName) ||
                            query.UserNameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.UserName!, "%" + query.UserName + "%") ||
                            query.UserNameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.UserName!, query.UserName + "%") ||
                            query.UserNameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.UserName!, "%" + query.UserName))
                    &&
                    (string.IsNullOrEmpty(query.ErrorProcedure) ||
                            query.ErrorProcedureSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.ErrorProcedure!, "%" + query.ErrorProcedure + "%") ||
                            query.ErrorProcedureSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.ErrorProcedure!, query.ErrorProcedure + "%") ||
                            query.ErrorProcedureSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.ErrorProcedure!, "%" + query.ErrorProcedure))
                    &&
                    (string.IsNullOrEmpty(query.ErrorMessage) ||
                            query.ErrorMessageSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.ErrorMessage!, "%" + query.ErrorMessage + "%") ||
                            query.ErrorMessageSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.ErrorMessage!, query.ErrorMessage + "%") ||
                            query.ErrorMessageSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.ErrorMessage!, "%" + query.ErrorMessage))

                select new NameValuePair
                {

                        Value = t.ErrorLogID.ToString(),
                        Name = t.UserName,
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
            ErrorLogAdvancedQuery query)
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

