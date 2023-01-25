using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.ServiceContracts;
using AdventureWorksLT2019.Models;
using Framework.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Collections.Concurrent;

namespace AdventureWorksLT2019.Services
{
    public class ProductCategoryService
        : IProductCategoryService
    {
        private readonly IProductCategoryRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ProductCategoryService> _logger;

        public ProductCategoryService(
            IProductCategoryRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ProductCategoryService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ProductCategoryDataModel.DefaultView[]>> Search(
            ProductCategoryAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ProductCategoryCompositeModel> GetCompositeModel(
            ProductCategoryIdentifier id,
            Dictionary<ProductCategoryCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ProductCategoryCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ProductCategoryCompositeModel();
                failedResponse.Responses.Add(ProductCategoryCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ProductCategoryCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ProductCategoryCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(ProductCategoryCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ProductCategoryCompositeModel.__DataOptions__.Products_Via_ProductCategoryID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                        var query = new ProductAdvancedQuery
                        {
                            ProductCategoryID = id.ProductCategoryID,
                            PageIndex = 1,
                            PageSize = listItemRequest[ProductCategoryCompositeModel.__DataOptions__.Products_Via_ProductCategoryID].PageSize,
                            OrderBys= listItemRequest[ProductCategoryCompositeModel.__DataOptions__.Products_Via_ProductCategoryID].OrderBys,
                            PaginationOption = listItemRequest[ProductCategoryCompositeModel.__DataOptions__.Products_Via_ProductCategoryID].PaginationOption,
                        };
                        var response = await _productRepository.Search(query);
                        responses.TryAdd(ProductCategoryCompositeModel.__DataOptions__.Products_Via_ProductCategoryID, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.Products_Via_ProductCategoryID = response.ResponseBody;
                        }
                    }
                }));
            }

            if (dataOptions == null || dataOptions.Contains(ProductCategoryCompositeModel.__DataOptions__.ProductCategories_Via_ParentProductCategoryID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _productCategoryRepository = scope.ServiceProvider.GetRequiredService<IProductCategoryRepository>();
                        var query = new ProductCategoryAdvancedQuery
                        {
                            ParentProductCategoryID = id.ProductCategoryID,
                            PageIndex = 1,
                            PageSize = listItemRequest[ProductCategoryCompositeModel.__DataOptions__.ProductCategories_Via_ParentProductCategoryID].PageSize,
                            OrderBys= listItemRequest[ProductCategoryCompositeModel.__DataOptions__.ProductCategories_Via_ParentProductCategoryID].OrderBys,
                            PaginationOption = listItemRequest[ProductCategoryCompositeModel.__DataOptions__.ProductCategories_Via_ParentProductCategoryID].PaginationOption,
                        };
                        var response = await _productCategoryRepository.Search(query);
                        responses.TryAdd(ProductCategoryCompositeModel.__DataOptions__.ProductCategories_Via_ParentProductCategoryID, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.ProductCategories_Via_ParentProductCategoryID = response.ResponseBody;
                        }
                    }
                }));
            }

            if (tasks.Count > 0)
            {
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                }
                catch { }
            }
            successResponse.Responses = new Dictionary<ProductCategoryCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ProductCategoryIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDRequest<ProductCategoryIdentifier, ProductCategoryDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductCategoryIdentifier, ProductCategoryDataModel.DefaultView> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
        }

        public async Task<Response<ProductCategoryDataModel.DefaultView>> Update(ProductCategoryIdentifier id, ProductCategoryDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ProductCategoryDataModel.DefaultView>> Get(ProductCategoryIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ProductCategoryDataModel.DefaultView>> Create(ProductCategoryDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ProductCategoryDataModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new ProductCategoryDataModel.DefaultView { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ProductCategoryIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductCategoryAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

