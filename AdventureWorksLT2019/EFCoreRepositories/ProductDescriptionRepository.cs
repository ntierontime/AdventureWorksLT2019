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
    public class ProductDescriptionRepository
        : IProductDescriptionRepository
    {
        private readonly ILogger<ProductDescriptionRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ProductDescriptionRepository(EFDbContext dbcontext, ILogger<ProductDescriptionRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<ProductDescriptionDataModel> SearchQuery(
            ProductDescriptionAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ProductDescription

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Description!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Description!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Description!, "%" + query.TextSearch)))
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Description) ||
                            query.DescriptionSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Description!, "%" + query.Description + "%") ||
                            query.DescriptionSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Description!, query.Description + "%") ||
                            query.DescriptionSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Description!, "%" + query.Description))

                select new ProductDescriptionDataModel
                {

                        ProductDescriptionID = t.ProductDescriptionID,
                        Description = t.Description,
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

        public async Task<PagedResponse<ProductDescriptionDataModel[]>> Search(
            ProductDescriptionAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ProductDescriptionDataModel>();
                return new PagedResponse<ProductDescriptionDataModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ProductDescriptionDataModel[]>>.FromResult(new PagedResponse<ProductDescriptionDataModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        public async Task<Response<ProductDescriptionDataModel>> Update(ProductDescriptionIdentifier id, ProductDescriptionDataModel input)
        {
            if (input == null)
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ProductDescription
                     where

                    t.ProductDescriptionID == id.ProductDescriptionID
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Description = input.Description;
                existing.ModifiedDate = input.ModifiedDate;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ProductDescriptionDataModel>>.FromResult(
                    new Response<ProductDescriptionDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ProductDescriptionDataModel
                        {
                            ProductDescriptionID = existing.ProductDescriptionID,
                            Description = existing.Description,
                            rowguid = existing.rowguid,
                            ModifiedDate = existing.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ProductDescriptionDataModel>> Get(ProductDescriptionIdentifier id)
        {
            if (id == null)
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ProductDescription.SingleOrDefault(
                    t =>

                    t.ProductDescriptionID == id.ProductDescriptionID
                );

                if (existing == null)
                    return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ProductDescriptionDataModel>>.FromResult(
                    new Response<ProductDescriptionDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ProductDescriptionDataModel
                        {
                            ProductDescriptionID = existing.ProductDescriptionID,
                            Description = existing.Description,
                            rowguid = existing.rowguid,
                            ModifiedDate = existing.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ProductDescriptionDataModel>> Create(ProductDescriptionDataModel input)
        {
            if (input == null)
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ProductDescription
                {
                            Description = input.Description,
                            ModifiedDate = input.ModifiedDate,
                };
                await _dbcontext.ProductDescription.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ProductDescriptionDataModel>>.FromResult(
                    new Response<ProductDescriptionDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ProductDescriptionDataModel
                        {
                            ProductDescriptionID = toInsert.ProductDescriptionID,
                            Description = toInsert.Description,
                            rowguid = toInsert.rowguid,
                            ModifiedDate = toInsert.ModifiedDate,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductDescriptionDataModel>>.FromResult(new Response<ProductDescriptionDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ProductDescriptionIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ProductDescription
                     where

                    t.ProductDescriptionID == id.ProductDescriptionID
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ProductDescription.Remove(existing);
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
            ProductDescriptionAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ProductDescription

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Description!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Description!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Description!, "%" + query.TextSearch)))
                    &&

                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Description) ||
                            query.DescriptionSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Description!, "%" + query.Description + "%") ||
                            query.DescriptionSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Description!, query.Description + "%") ||
                            query.DescriptionSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Description!, "%" + query.Description))

                select new NameValuePair
                {

                        Value = t.ProductDescriptionID.ToString(),
                        Name = t.Description,
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
            ProductDescriptionAdvancedQuery query)
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

