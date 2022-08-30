using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.ServiceContracts;
using AdventureWorksLT2019.Models;
using Framework.Models;
using Framework.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Collections.Concurrent;

namespace AdventureWorksLT2019.Services
{
    public class ProductModelService
        : IProductModelService
    {
        private readonly IProductModelRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ProductModelService> _logger;

        public ProductModelService(
            IProductModelRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ProductModelService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ProductModelDataModel[]>> Search(
            ProductModelAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ProductModelCompositeModel> GetCompositeModel(
            ProductModelIdentifier id,
            Dictionary<ProductModelCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ProductModelCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ProductModelCompositeModel();
                failedResponse.Responses.Add(ProductModelCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ProductModelCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ProductModelCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(ProductModelCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ProductModelCompositeModel.__DataOptions__.Products_Via_ProductModelID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                        var query = new ProductAdvancedQuery
                        {
                            ProductModelID = id.ProductModelID,
                            PageIndex = 1,
                            PageSize = listItemRequest[ProductModelCompositeModel.__DataOptions__.Products_Via_ProductModelID].PageSize,
                            OrderBys= listItemRequest[ProductModelCompositeModel.__DataOptions__.Products_Via_ProductModelID].OrderBys,
                            PaginationOption = listItemRequest[ProductModelCompositeModel.__DataOptions__.Products_Via_ProductModelID].PaginationOption,
                        };
                        var response = await _productRepository.Search(query);
                        responses.TryAdd(ProductModelCompositeModel.__DataOptions__.Products_Via_ProductModelID, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.Products_Via_ProductModelID = response.ResponseBody;
                        }
                    }
                }));
            }

            if (dataOptions == null || dataOptions.Contains(ProductModelCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductModelID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _productModelProductDescriptionRepository = scope.ServiceProvider.GetRequiredService<IProductModelProductDescriptionRepository>();
                        var query = new ProductModelProductDescriptionAdvancedQuery
                        {
                            ProductModelID = id.ProductModelID,
                            PageIndex = 1,
                            PageSize = listItemRequest[ProductModelCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductModelID].PageSize,
                            OrderBys= listItemRequest[ProductModelCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductModelID].OrderBys,
                            PaginationOption = listItemRequest[ProductModelCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductModelID].PaginationOption,
                        };
                        var response = await _productModelProductDescriptionRepository.Search(query);
                        responses.TryAdd(ProductModelCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductModelID, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.ProductModelProductDescriptions_Via_ProductModelID = response.ResponseBody;
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
            successResponse.Responses = new Dictionary<ProductModelCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ProductModelIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDRequest<ProductModelIdentifier, ProductModelDataModel>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductModelIdentifier, ProductModelDataModel> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
        }

        public async Task<Response<ProductModelDataModel>> Update(ProductModelIdentifier id, ProductModelDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ProductModelDataModel>> Get(ProductModelIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ProductModelDataModel>> Create(ProductModelDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ProductModelDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ProductModelDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ProductModelIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductModelAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

