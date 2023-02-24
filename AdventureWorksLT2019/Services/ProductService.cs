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
    public class ProductService
        : IProductService
    {
        private readonly IProductRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IProductRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ProductService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ProductDataModel.DefaultView[]>> Search(
            ProductAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ProductCompositeModel> GetCompositeModel(
            ProductIdentifier id,
            Dictionary<ProductCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ProductCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ProductCompositeModel();
                failedResponse.Responses.Add(ProductCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ProductCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ProductCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(ProductCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _salesOrderDetailRepository = scope.ServiceProvider.GetRequiredService<ISalesOrderDetailRepository>();
                        var query = new SalesOrderDetailAdvancedQuery
                        {
                            ProductID = id.ProductID,
                            PageIndex = 1,
                            PageSize = listItemRequest[ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID].PageSize,
                            OrderBys= listItemRequest[ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID].OrderBys,
                            PaginationOption = listItemRequest[ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID].PaginationOption,
                        };
                        var response = await _salesOrderDetailRepository.Search(query);
                        responses.TryAdd(ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.SalesOrderDetails_Via_ProductID = response.ResponseBody;
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
            successResponse.Responses = new Dictionary<ProductCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ProductIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDRequest<ProductIdentifier, ProductDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductIdentifier, ProductDataModel.DefaultView> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
        }

        public async Task<Response<ProductDataModel.DefaultView>> Update(ProductIdentifier id, ProductDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ProductDataModel.DefaultView>> Get(ProductIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ProductDataModel.DefaultView>> Create(ProductDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ProductDataModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new ProductDataModel.DefaultView { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ProductIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }

        public async Task<Response<ProductDataModel.DefaultView>> CreateComposite(ProductCompositeModel input)
        {
            return await _thisRepository.CreateComposite(input);
        }
    }
}

