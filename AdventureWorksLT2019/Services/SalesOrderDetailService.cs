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
    public class SalesOrderDetailService
        : ISalesOrderDetailService
    {
        private readonly ISalesOrderDetailRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<SalesOrderDetailService> _logger;

        public SalesOrderDetailService(
            ISalesOrderDetailRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<SalesOrderDetailService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<PagedResponse<SalesOrderDetailDataModel.DefaultView[]>> Search(
            SalesOrderDetailAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<SalesOrderDetailCompositeModel> GetCompositeModel(
            SalesOrderDetailIdentifier id,
            Dictionary<SalesOrderDetailCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            SalesOrderDetailCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new SalesOrderDetailCompositeModel();
                failedResponse.Responses.Add(SalesOrderDetailCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new SalesOrderDetailCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<SalesOrderDetailCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(SalesOrderDetailCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 2. AncestorTable = 2,

            if (dataOptions == null || dataOptions.Contains(SalesOrderDetailCompositeModel.__DataOptions__.Product))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                        var idQuery = new ProductIdentifier { ProductID = id.SalesOrderID };
                        var response = await productRepository.Get(idQuery);
                        responses.TryAdd(SalesOrderDetailCompositeModel.__DataOptions__.Product, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage });
                        if(response.Status == HttpStatusCode.OK)
                        {
                            successResponse.Product = response.ResponseBody;
                        }
                    }
                }));
            }

            if (dataOptions == null || dataOptions.Contains(SalesOrderDetailCompositeModel.__DataOptions__.ProductCategory))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var productCategoryRepository = scope.ServiceProvider.GetRequiredService<IProductCategoryRepository>();
                        var idQuery = new ProductCategoryIdentifier { ProductCategoryID = id.SalesOrderID };
                        var response = await productCategoryRepository.Get(idQuery);
                        responses.TryAdd(SalesOrderDetailCompositeModel.__DataOptions__.ProductCategory, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage });
                        if(response.Status == HttpStatusCode.OK)
                        {
                            successResponse.ProductCategory = response.ResponseBody;
                        }
                    }
                }));
            }

            if (dataOptions == null || dataOptions.Contains(SalesOrderDetailCompositeModel.__DataOptions__.SalesOrderHeader))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var salesOrderHeaderRepository = scope.ServiceProvider.GetRequiredService<ISalesOrderHeaderRepository>();
                        var idQuery = new SalesOrderHeaderIdentifier { SalesOrderID = id.SalesOrderID };
                        var response = await salesOrderHeaderRepository.Get(idQuery);
                        responses.TryAdd(SalesOrderDetailCompositeModel.__DataOptions__.SalesOrderHeader, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage });
                        if(response.Status == HttpStatusCode.OK)
                        {
                            successResponse.SalesOrderHeader = response.ResponseBody;
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
            successResponse.Responses = new Dictionary<SalesOrderDetailCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<SalesOrderDetailIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDModel<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
        }

        public async Task<Response<SalesOrderDetailDataModel.DefaultView>> Update(SalesOrderDetailIdentifier id, SalesOrderDetailDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<SalesOrderDetailDataModel.DefaultView>> Get(SalesOrderDetailIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<SalesOrderDetailDataModel.DefaultView>> Create(SalesOrderDetailDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public SalesOrderDetailDataModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new SalesOrderDetailDataModel.DefaultView { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(SalesOrderDetailIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            SalesOrderDetailAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

