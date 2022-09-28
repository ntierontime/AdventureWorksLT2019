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
    public class ProductModelProductDescriptionService
        : IProductModelProductDescriptionService
    {
        private readonly IProductModelProductDescriptionRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ProductModelProductDescriptionService> _logger;

        public ProductModelProductDescriptionService(
            IProductModelProductDescriptionRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ProductModelProductDescriptionService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>> Search(
            ProductModelProductDescriptionAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ProductModelProductDescriptionCompositeModel> GetCompositeModel(
            ProductModelProductDescriptionIdentifier id,
            Dictionary<ProductModelProductDescriptionCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ProductModelProductDescriptionCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ProductModelProductDescriptionCompositeModel();
                failedResponse.Responses.Add(ProductModelProductDescriptionCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ProductModelProductDescriptionCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ProductModelProductDescriptionCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(ProductModelProductDescriptionCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            if (tasks.Count > 0)
            {
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                }
                catch { }
            }
            successResponse.Responses = new Dictionary<ProductModelProductDescriptionCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ProductModelProductDescriptionIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
        }

        public async Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Update(ProductModelProductDescriptionIdentifier id, ProductModelProductDescriptionDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Get(ProductModelProductDescriptionIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Create(ProductModelProductDescriptionDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ProductModelProductDescriptionDataModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new ProductModelProductDescriptionDataModel.DefaultView { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ProductModelProductDescriptionIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductModelProductDescriptionAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

