using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.EFCoreContext;
using AdventureWorksLT2019.Models;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;

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

                    join ProductCategory_A in _dbcontext.ProductCategory on t.ProductCategoryID equals ProductCategory_A.ProductCategoryID into ProductCategory_G from ProductCategory in ProductCategory_G.DefaultIfEmpty()// \ProductCategoryID
                    join Parent_A in _dbcontext.ProductCategory on ProductCategory.ParentProductCategoryID equals Parent_A.ProductCategoryID into Parent_G from Parent in Parent_G.DefaultIfEmpty()// \ProductCategoryID\ParentProductCategoryID
                    join ProductModel_A in _dbcontext.ProductModel on t.ProductModelID equals ProductModel_A.ProductModelID into ProductModel_G from ProductModel in ProductModel_G.DefaultIfEmpty()// \ProductModelID
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
                        ParentID = Parent.ProductCategoryID,
                        Parent_Name = Parent.Name,
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

                    join ProductCategory_A in _dbcontext.ProductCategory on t.ProductCategoryID equals ProductCategory_A.ProductCategoryID into ProductCategory_G from ProductCategory in ProductCategory_G.DefaultIfEmpty()// \ProductCategoryID
                    join Parent_A in _dbcontext.ProductCategory on ProductCategory.ParentProductCategoryID equals Parent_A.ProductCategoryID into Parent_G from Parent in Parent_G.DefaultIfEmpty()// \ProductCategoryID\ParentProductCategoryID
                    join ProductModel_A in _dbcontext.ProductModel on t.ProductModelID equals ProductModel_A.ProductModelID into ProductModel_G from ProductModel in ProductModel_G.DefaultIfEmpty()// \ProductModelID
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
                        ParentID = Parent.ProductCategoryID,
                        Parent_Name = Parent.Name,
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

                    join ProductCategory_A in _dbcontext.ProductCategory on t.ProductCategoryID equals ProductCategory_A.ProductCategoryID into ProductCategory_G from ProductCategory in ProductCategory_G.DefaultIfEmpty()// \ProductCategoryID
                    join Parent_A in _dbcontext.ProductCategory on ProductCategory.ParentProductCategoryID equals Parent_A.ProductCategoryID into Parent_G from Parent in Parent_G.DefaultIfEmpty()// \ProductCategoryID\ParentProductCategoryID
                    join ProductModel_A in _dbcontext.ProductModel on t.ProductModelID equals ProductModel_A.ProductModelID into ProductModel_G from ProductModel in ProductModel_G.DefaultIfEmpty()// \ProductModelID
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
                        ParentID = Parent.ProductCategoryID,
                        Parent_Name = Parent.Name,
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

                    join ProductCategory_A in _dbcontext.ProductCategory on t.ProductCategoryID equals ProductCategory_A.ProductCategoryID into ProductCategory_G from ProductCategory in ProductCategory_G.DefaultIfEmpty()// \ProductCategoryID
                    join Parent_A in _dbcontext.ProductCategory on ProductCategory.ParentProductCategoryID equals Parent_A.ProductCategoryID into Parent_G from Parent in Parent_G.DefaultIfEmpty()// \ProductCategoryID\ParentProductCategoryID
                    join ProductModel_A in _dbcontext.ProductModel on t.ProductModelID equals ProductModel_A.ProductModelID into ProductModel_G from ProductModel in ProductModel_G.DefaultIfEmpty()// \ProductModelID
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
                        ParentID = Parent.ProductCategoryID,
                        Parent_Name = Parent.Name,
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

        private IQueryable<NameValuePair> GetCodeListQuery(
            ProductAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.Product

                    join ProductCategory_A in _dbcontext.ProductCategory on t.ProductCategoryID equals ProductCategory_A.ProductCategoryID into ProductCategory_G from ProductCategory in ProductCategory_G.DefaultIfEmpty()// \ProductCategoryID
                    join Parent_A in _dbcontext.ProductCategory on ProductCategory.ParentProductCategoryID equals Parent_A.ProductCategoryID into Parent_G from Parent in Parent_G.DefaultIfEmpty()// \ProductCategoryID\ParentProductCategoryID
                    join ProductModel_A in _dbcontext.ProductModel on t.ProductModelID equals ProductModel_A.ProductModelID into ProductModel_G from ProductModel in ProductModel_G.DefaultIfEmpty()// \ProductModelID
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

