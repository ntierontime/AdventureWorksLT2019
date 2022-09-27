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
    public class ProductDescriptionService
        : IProductDescriptionService
    {
        private readonly IProductDescriptionRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ProductDescriptionService> _logger;

        public ProductDescriptionService(
            IProductDescriptionRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ProductDescriptionService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ProductDescriptionDataModel[]>> Search(
            ProductDescriptionAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ProductDescriptionCompositeModel> GetCompositeModel(
            ProductDescriptionIdentifier id,
            Dictionary<ProductDescriptionCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ProductDescriptionCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ProductDescriptionCompositeModel();
                failedResponse.Responses.Add(ProductDescriptionCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ProductDescriptionCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ProductDescriptionCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(ProductDescriptionCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ProductDescriptionCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductDescriptionID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _productModelProductDescriptionRepository = scope.ServiceProvider.GetRequiredService<IProductModelProductDescriptionRepository>();
                        var query = new ProductModelProductDescriptionAdvancedQuery
                        {
                            ProductDescriptionID = id.ProductDescriptionID,
                            PageIndex = 1,
                            PageSize = listItemRequest[ProductDescriptionCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductDescriptionID].PageSize,
                            OrderBys= listItemRequest[ProductDescriptionCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductDescriptionID].OrderBys,
                            PaginationOption = listItemRequest[ProductDescriptionCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductDescriptionID].PaginationOption,
                        };
                        var response = await _productModelProductDescriptionRepository.Search(query);
                        responses.TryAdd(ProductDescriptionCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductDescriptionID, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.ProductModelProductDescriptions_Via_ProductDescriptionID = response.ResponseBody;
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
            successResponse.Responses = new Dictionary<ProductDescriptionCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ProductDescriptionIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
        }

        public async Task<Response<ProductDescriptionDataModel>> Update(ProductDescriptionIdentifier id, ProductDescriptionDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ProductDescriptionDataModel>> Get(ProductDescriptionIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ProductDescriptionDataModel>> Create(ProductDescriptionDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ProductDescriptionDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ProductDescriptionDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ProductDescriptionIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductDescriptionAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

