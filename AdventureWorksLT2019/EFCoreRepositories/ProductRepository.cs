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
    public class ProductRepository
        : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ProductRepository(EFDbContext dbcontext, ILogger<ProductRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<ProductDataModel.DefaultView> SearchQuery(
            ProductAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.Product

                    join ProductCategory in _dbcontext.ProductCategory on t.ProductCategoryID equals ProductCategory.ProductCategoryID// \ProductCategoryID
                    join Parent in _dbcontext.ProductCategory on ProductCategory.ParentProductCategoryID equals Parent.ProductCategoryID// \ProductCategoryID\ParentProductCategoryID
                    join ProductModel in _dbcontext.ProductModel on t.ProductModelID equals ProductModel.ProductModelID// \ProductModelID
                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Name!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.ProductNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.Color!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.Size!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.ThumbnailPhotoFileName!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Name!, query.TextSearch + "%") || EF.Functions.Like(t.ProductNumber!, query.TextSearch + "%") || EF.Functions.Like(t.Color!, query.TextSearch + "%") || EF.Functions.Like(t.Size!, query.TextSearch + "%") || EF.Functions.Like(t.ThumbnailPhotoFileName!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Name!, "%" + query.TextSearch) || EF.Functions.Like(t.ProductNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.Color!, "%" + query.TextSearch) || EF.Functions.Like(t.Size!, "%" + query.TextSearch) || EF.Functions.Like(t.ThumbnailPhotoFileName!, "%" + query.TextSearch)))
                    &&

                    (!query.ProductCategoryID.HasValue || ProductCategory.ProductCategoryID == query.ProductCategoryID)
                    &&
                    (!query.ParentID.HasValue || Parent.ProductCategoryID == query.ParentID)
                    &&
                    (!query.ProductModelID.HasValue || ProductModel.ProductModelID == query.ProductModelID)
                    &&

                    (!query.SellStartDateRangeLower.HasValue && !query.SellStartDateRangeUpper.HasValue || (!query.SellStartDateRangeLower.HasValue || t.SellStartDate >= query.SellStartDateRangeLower) && (!query.SellStartDateRangeLower.HasValue || t.SellStartDate <= query.SellStartDateRangeUpper))
                    &&
                    (!query.SellEndDateRangeLower.HasValue && !query.SellEndDateRangeUpper.HasValue || (!query.SellEndDateRangeLower.HasValue || t.SellEndDate >= query.SellEndDateRangeLower) && (!query.SellEndDateRangeLower.HasValue || t.SellEndDate <= query.SellEndDateRangeUpper))
                    &&
                    (!query.DiscontinuedDateRangeLower.HasValue && !query.DiscontinuedDateRangeUpper.HasValue || (!query.DiscontinuedDateRangeLower.HasValue || t.DiscontinuedDate >= query.DiscontinuedDateRangeLower) && (!query.DiscontinuedDateRangeLower.HasValue || t.DiscontinuedDate <= query.DiscontinuedDateRangeUpper))
                    &&
                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Name) ||
                            query.NameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Name!, "%" + query.Name + "%") ||
                            query.NameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Name!, query.Name + "%") ||
                            query.NameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Name!, "%" + query.Name))
                    &&
                    (string.IsNullOrEmpty(query.ProductNumber) ||
                            query.ProductNumberSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.ProductNumber!, "%" + query.ProductNumber + "%") ||
                            query.ProductNumberSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.ProductNumber!, query.ProductNumber + "%") ||
                            query.ProductNumberSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.ProductNumber!, "%" + query.ProductNumber))
                    &&
                    (string.IsNullOrEmpty(query.Color) ||
                            query.ColorSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Color!, "%" + query.Color + "%") ||
                            query.ColorSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Color!, query.Color + "%") ||
                            query.ColorSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Color!, "%" + query.Color))
                    &&
                    (string.IsNullOrEmpty(query.Size) ||
                            query.SizeSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Size!, "%" + query.Size + "%") ||
                            query.SizeSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Size!, query.Size + "%") ||
                            query.SizeSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Size!, "%" + query.Size))
                    &&
                    (string.IsNullOrEmpty(query.ThumbnailPhotoFileName) ||
                            query.ThumbnailPhotoFileNameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.ThumbnailPhotoFileName!, "%" + query.ThumbnailPhotoFileName + "%") ||
                            query.ThumbnailPhotoFileNameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.ThumbnailPhotoFileName!, query.ThumbnailPhotoFileName + "%") ||
                            query.ThumbnailPhotoFileNameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.ThumbnailPhotoFileName!, "%" + query.ThumbnailPhotoFileName))

                select new ProductDataModel.DefaultView
                {

                        ProductID = t.ProductID,
                        Name = t.Name,
                        ProductNumber = t.ProductNumber,
                        Color = t.Color,
                        StandardCost = t.StandardCost,
                        ListPrice = t.ListPrice,
                        Size = t.Size,
                        Weight = t.Weight,
                        ProductCategoryID = t.ProductCategoryID,
                        ProductModelID = t.ProductModelID,
                        SellStartDate = t.SellStartDate,
                        SellEndDate = t.SellEndDate,
                        DiscontinuedDate = t.DiscontinuedDate,
                        ThumbNailPhoto = t.ThumbNailPhoto,
                        ThumbnailPhotoFileName = t.ThumbnailPhotoFileName,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        ProductCategory_Name = ProductCategory.Name,
                        ProductModel_Name = ProductModel.Name,
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

        public async Task<ListResponse<ProductDataModel.DefaultView[]>> Search(
            ProductAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ProductDataModel.DefaultView>();
                return new ListResponse<ProductDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<ProductDataModel.DefaultView[]>>.FromResult(new ListResponse<ProductDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<Product> GetIQueryableByPrimaryIdentifierList(
            List<ProductIdentifier> ids)
        {
            var idList = ids.Select(t => t.ProductID).ToList();
            var queryable =
                from t in _dbcontext.Product
                where idList.Contains(t.ProductID)
                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<ProductIdentifier> ids)
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

        public async Task<Response<MultiItemsCUDRequest<ProductIdentifier, ProductDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductIdentifier, ProductDataModel.DefaultView> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDRequest<ProductIdentifier, ProductDataModel.DefaultView>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDRequest<ProductIdentifier, ProductDataModel.DefaultView>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<Product> newEFItems = new();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new Product
                        {
                            Name = item.Name,
                            ProductNumber = item.ProductNumber,
                            Color = item.Color,
                            StandardCost = item.StandardCost,
                            ListPrice = item.ListPrice,
                            Size = item.Size,
                            Weight = item.Weight,
                            ProductCategoryID = item.ProductCategoryID,
                            ProductModelID = item.ProductModelID,
                            SellStartDate = item.SellStartDate,
                            SellEndDate = item.SellEndDate,
                            DiscontinuedDate = item.DiscontinuedDate,
                            ThumbNailPhoto = item.ThumbNailPhoto,
                            ThumbnailPhotoFileName = item.ThumbnailPhotoFileName,
                            ModifiedDate = item.ModifiedDate,
                        };
                        _dbcontext.Product.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.Product
                             where

                             t.ProductID == item.ProductID
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Name = item.Name;
                existing.ProductNumber = item.ProductNumber;
                existing.Color = item.Color;
                existing.StandardCost = item.StandardCost;
                existing.ListPrice = item.ListPrice;
                existing.Size = item.Size;
                existing.Weight = item.Weight;
                existing.ProductCategoryID = item.ProductCategoryID;
                existing.ProductModelID = item.ProductModelID;
                existing.SellStartDate = item.SellStartDate;
                existing.SellEndDate = item.SellEndDate;
                existing.DiscontinuedDate = item.DiscontinuedDate;
                existing.ThumbNailPhoto = item.ThumbNailPhoto;
                existing.ThumbnailPhotoFileName = item.ThumbnailPhotoFileName;
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
                        select t.ProductID);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.ProductID);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.Product
                    join ProductCategory in _dbcontext.ProductCategory on t.ProductCategoryID equals ProductCategory.ProductCategoryID// \ProductCategoryID
                    join ProductModel in _dbcontext.ProductModel on t.ProductModelID equals ProductModel.ProductModelID// \ProductModelID
                    where identifierListToloadResponseItems.Contains(t.ProductID)

                    select new ProductDataModel.DefaultView
                    {

                        ProductID = t.ProductID,
                        Name = t.Name,
                        ProductNumber = t.ProductNumber,
                        Color = t.Color,
                        StandardCost = t.StandardCost,
                        ListPrice = t.ListPrice,
                        Size = t.Size,
                        Weight = t.Weight,
                        ProductCategoryID = t.ProductCategoryID,
                        ProductModelID = t.ProductModelID,
                        SellStartDate = t.SellStartDate,
                        SellEndDate = t.SellEndDate,
                        DiscontinuedDate = t.DiscontinuedDate,
                        ThumbNailPhoto = t.ThumbNailPhoto,
                        ThumbnailPhotoFileName = t.ThumbnailPhotoFileName,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        ProductCategory_Name = ProductCategory.Name,
                        ProductModel_Name = ProductModel.Name,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDRequest<ProductIdentifier, ProductDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDRequest<ProductIdentifier, ProductDataModel.DefaultView>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.ProductID == t.ProductID)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.ProductID == t.ProductID)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDRequest<ProductIdentifier, ProductDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<ProductDataModel.DefaultView>> Update(ProductIdentifier id, ProductDataModel input)
        {
            if (input == null)
                return await Task<Response<ProductDataModel.DefaultView>>.FromResult(new Response<ProductDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.Product
                     where

                    t.ProductID == id.ProductID
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ProductDataModel.DefaultView>>.FromResult(new Response<ProductDataModel.DefaultView> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Name = input.Name;
                existing.ProductNumber = input.ProductNumber;
                existing.Color = input.Color;
                existing.StandardCost = input.StandardCost;
                existing.ListPrice = input.ListPrice;
                existing.Size = input.Size;
                existing.Weight = input.Weight;
                existing.ProductCategoryID = input.ProductCategoryID;
                existing.ProductModelID = input.ProductModelID;
                existing.SellStartDate = input.SellStartDate;
                existing.SellEndDate = input.SellEndDate;
                existing.DiscontinuedDate = input.DiscontinuedDate;
                existing.ThumbNailPhoto = input.ThumbNailPhoto;
                existing.ThumbnailPhotoFileName = input.ThumbnailPhotoFileName;
                existing.ModifiedDate = input.ModifiedDate;
                await _dbcontext.SaveChangesAsync();

                var responseBody =
                    (
                    from t in _dbcontext.Product

                    join ProductCategory in _dbcontext.ProductCategory on t.ProductCategoryID equals ProductCategory.ProductCategoryID// \ProductCategoryID
                    join ProductModel in _dbcontext.ProductModel on t.ProductModelID equals ProductModel.ProductModelID// \ProductModelID
                    where t.ProductID == existing.ProductID

                    select new ProductDataModel.DefaultView
                    {

                        ProductID = t.ProductID,
                        Name = t.Name,
                        ProductNumber = t.ProductNumber,
                        Color = t.Color,
                        StandardCost = t.StandardCost,
                        ListPrice = t.ListPrice,
                        Size = t.Size,
                        Weight = t.Weight,
                        ProductCategoryID = t.ProductCategoryID,
                        ProductModelID = t.ProductModelID,
                        SellStartDate = t.SellStartDate,
                        SellEndDate = t.SellEndDate,
                        DiscontinuedDate = t.DiscontinuedDate,
                        ThumbNailPhoto = t.ThumbNailPhoto,
                        ThumbnailPhotoFileName = t.ThumbnailPhotoFileName,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        ProductCategory_Name = ProductCategory.Name,
                        ProductModel_Name = ProductModel.Name,

                    }).First();

                return await Task<Response<ProductDataModel.DefaultView>>.FromResult(
                    new Response<ProductDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductDataModel.DefaultView>>.FromResult(new Response<ProductDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ProductDataModel.DefaultView>> Get(ProductIdentifier id)
        {
            if (id == null)
                return await Task<Response<ProductDataModel.DefaultView>>.FromResult(new Response<ProductDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {

                var responseBody =
                    (
                    from t in _dbcontext.Product

                    join ProductCategory in _dbcontext.ProductCategory on t.ProductCategoryID equals ProductCategory.ProductCategoryID// \ProductCategoryID
                    join ProductModel in _dbcontext.ProductModel on t.ProductModelID equals ProductModel.ProductModelID// \ProductModelID
                    where

                    t.ProductID == id.ProductID

                    select new ProductDataModel.DefaultView
                    {

                        ProductID = t.ProductID,
                        Name = t.Name,
                        ProductNumber = t.ProductNumber,
                        Color = t.Color,
                        StandardCost = t.StandardCost,
                        ListPrice = t.ListPrice,
                        Size = t.Size,
                        Weight = t.Weight,
                        ProductCategoryID = t.ProductCategoryID,
                        ProductModelID = t.ProductModelID,
                        SellStartDate = t.SellStartDate,
                        SellEndDate = t.SellEndDate,
                        DiscontinuedDate = t.DiscontinuedDate,
                        ThumbNailPhoto = t.ThumbNailPhoto,
                        ThumbnailPhotoFileName = t.ThumbnailPhotoFileName,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        ProductCategory_Name = ProductCategory.Name,
                        ProductModel_Name = ProductModel.Name,

                    }).First();
                if (responseBody == null)
                    return await Task<Response<ProductDataModel.DefaultView>>.FromResult(new Response<ProductDataModel.DefaultView> { Status = HttpStatusCode.NotFound });
                return await Task<Response<ProductDataModel.DefaultView>>.FromResult(
                    new Response<ProductDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductDataModel.DefaultView>>.FromResult(new Response<ProductDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ProductDataModel.DefaultView>> Create(ProductDataModel input)
        {
            if (input == null)
                return await Task<Response<ProductDataModel.DefaultView>>.FromResult(new Response<ProductDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new Product
                {
                            Name = input.Name,
                            ProductNumber = input.ProductNumber,
                            Color = input.Color,
                            StandardCost = input.StandardCost,
                            ListPrice = input.ListPrice,
                            Size = input.Size,
                            Weight = input.Weight,
                            ProductCategoryID = input.ProductCategoryID,
                            ProductModelID = input.ProductModelID,
                            SellStartDate = input.SellStartDate,
                            SellEndDate = input.SellEndDate,
                            DiscontinuedDate = input.DiscontinuedDate,
                            ThumbNailPhoto = input.ThumbNailPhoto,
                            ThumbnailPhotoFileName = input.ThumbnailPhotoFileName,
                            ModifiedDate = input.ModifiedDate,
                };
                await _dbcontext.Product.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                var responseBody =
                    (
                    from t in _dbcontext.Product

                    join ProductCategory in _dbcontext.ProductCategory on t.ProductCategoryID equals ProductCategory.ProductCategoryID// \ProductCategoryID
                    join ProductModel in _dbcontext.ProductModel on t.ProductModelID equals ProductModel.ProductModelID// \ProductModelID
                    where t.ProductID == toInsert.ProductID

                    select new ProductDataModel.DefaultView
                    {

                        ProductID = t.ProductID,
                        Name = t.Name,
                        ProductNumber = t.ProductNumber,
                        Color = t.Color,
                        StandardCost = t.StandardCost,
                        ListPrice = t.ListPrice,
                        Size = t.Size,
                        Weight = t.Weight,
                        ProductCategoryID = t.ProductCategoryID,
                        ProductModelID = t.ProductModelID,
                        SellStartDate = t.SellStartDate,
                        SellEndDate = t.SellEndDate,
                        DiscontinuedDate = t.DiscontinuedDate,
                        ThumbNailPhoto = t.ThumbNailPhoto,
                        ThumbnailPhotoFileName = t.ThumbnailPhotoFileName,
                        rowguid = t.rowguid,
                        ModifiedDate = t.ModifiedDate,
                        ProductCategory_Name = ProductCategory.Name,
                        ProductModel_Name = ProductModel.Name,

                    }).First();

                return await Task<Response<ProductDataModel.DefaultView>>.FromResult(
                    new Response<ProductDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ProductDataModel.DefaultView>>.FromResult(new Response<ProductDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ProductIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.Product
                     where

                    t.ProductID == id.ProductID
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.Product.Remove(existing);
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
            ProductAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.Product

                    join ProductCategory in _dbcontext.ProductCategory on t.ProductCategoryID equals ProductCategory.ProductCategoryID// \ProductCategoryID
                    join Parent in _dbcontext.ProductCategory on ProductCategory.ParentProductCategoryID equals Parent.ProductCategoryID// \ProductCategoryID\ParentProductCategoryID
                    join ProductModel in _dbcontext.ProductModel on t.ProductModelID equals ProductModel.ProductModelID// \ProductModelID
                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Name!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.ProductNumber!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.Color!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.Size!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.ThumbnailPhotoFileName!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Name!, query.TextSearch + "%") || EF.Functions.Like(t.ProductNumber!, query.TextSearch + "%") || EF.Functions.Like(t.Color!, query.TextSearch + "%") || EF.Functions.Like(t.Size!, query.TextSearch + "%") || EF.Functions.Like(t.ThumbnailPhotoFileName!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Name!, "%" + query.TextSearch) || EF.Functions.Like(t.ProductNumber!, "%" + query.TextSearch) || EF.Functions.Like(t.Color!, "%" + query.TextSearch) || EF.Functions.Like(t.Size!, "%" + query.TextSearch) || EF.Functions.Like(t.ThumbnailPhotoFileName!, "%" + query.TextSearch)))
                    &&

                    (!query.ProductCategoryID.HasValue || ProductCategory.ProductCategoryID == query.ProductCategoryID)
                    &&
                    (!query.ParentID.HasValue || Parent.ProductCategoryID == query.ParentID)
                    &&
                    (!query.ProductModelID.HasValue || ProductModel.ProductModelID == query.ProductModelID)
                    &&

                    (!query.SellStartDateRangeLower.HasValue && !query.SellStartDateRangeUpper.HasValue || (!query.SellStartDateRangeLower.HasValue || t.SellStartDate >= query.SellStartDateRangeLower) && (!query.SellStartDateRangeLower.HasValue || t.SellStartDate <= query.SellStartDateRangeUpper))
                    &&
                    (!query.SellEndDateRangeLower.HasValue && !query.SellEndDateRangeUpper.HasValue || (!query.SellEndDateRangeLower.HasValue || t.SellEndDate >= query.SellEndDateRangeLower) && (!query.SellEndDateRangeLower.HasValue || t.SellEndDate <= query.SellEndDateRangeUpper))
                    &&
                    (!query.DiscontinuedDateRangeLower.HasValue && !query.DiscontinuedDateRangeUpper.HasValue || (!query.DiscontinuedDateRangeLower.HasValue || t.DiscontinuedDate >= query.DiscontinuedDateRangeLower) && (!query.DiscontinuedDateRangeLower.HasValue || t.DiscontinuedDate <= query.DiscontinuedDateRangeUpper))
                    &&
                    (!query.ModifiedDateRangeLower.HasValue && !query.ModifiedDateRangeUpper.HasValue || (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate >= query.ModifiedDateRangeLower) && (!query.ModifiedDateRangeLower.HasValue || t.ModifiedDate <= query.ModifiedDateRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Name) ||
                            query.NameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Name!, "%" + query.Name + "%") ||
                            query.NameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Name!, query.Name + "%") ||
                            query.NameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Name!, "%" + query.Name))
                    &&
                    (string.IsNullOrEmpty(query.ProductNumber) ||
                            query.ProductNumberSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.ProductNumber!, "%" + query.ProductNumber + "%") ||
                            query.ProductNumberSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.ProductNumber!, query.ProductNumber + "%") ||
                            query.ProductNumberSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.ProductNumber!, "%" + query.ProductNumber))
                    &&
                    (string.IsNullOrEmpty(query.Color) ||
                            query.ColorSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Color!, "%" + query.Color + "%") ||
                            query.ColorSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Color!, query.Color + "%") ||
                            query.ColorSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Color!, "%" + query.Color))
                    &&
                    (string.IsNullOrEmpty(query.Size) ||
                            query.SizeSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Size!, "%" + query.Size + "%") ||
                            query.SizeSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Size!, query.Size + "%") ||
                            query.SizeSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Size!, "%" + query.Size))
                    &&
                    (string.IsNullOrEmpty(query.ThumbnailPhotoFileName) ||
                            query.ThumbnailPhotoFileNameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.ThumbnailPhotoFileName!, "%" + query.ThumbnailPhotoFileName + "%") ||
                            query.ThumbnailPhotoFileNameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.ThumbnailPhotoFileName!, query.ThumbnailPhotoFileName + "%") ||
                            query.ThumbnailPhotoFileNameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.ThumbnailPhotoFileName!, "%" + query.ThumbnailPhotoFileName))

                select new NameValuePair
                {

                        Name = t.Name,
                        Value = t.ProductID.ToString(),
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
            ProductAdvancedQuery query)
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

