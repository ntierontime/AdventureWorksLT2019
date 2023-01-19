using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.EFCoreContext;
using AdventureWorksLT2019.Models;
//using Framework.Helpers;
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

        public async Task<ListResponse<ErrorLogDataModel[]>> Search(
            ErrorLogAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ErrorLogDataModel>();
                return new ListResponse<ErrorLogDataModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<ErrorLogDataModel[]>>.FromResult(new ListResponse<ErrorLogDataModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
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

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ErrorLogAdvancedQuery query)
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

