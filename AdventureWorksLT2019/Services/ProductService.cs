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

        public async Task<PagedResponse<ProductDataModel.DefaultView[]>> Search(
            ProductAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ProductCompositeModel> GetCompositeModel(ProductIdentifier id, ProductCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ProductCompositeModel();
                failedResponse.Responses.Add(ProductCompositeModel.__DataOptions__.__Master__, new Response { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ProductCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ProductCompositeModel.__DataOptions__, Response>();
            responses.TryAdd(ProductCompositeModel.__DataOptions__.__Master__, new Response { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 2. AncestorTable = 2,

            if (dataOptions == null || dataOptions.Contains(ProductCompositeModel.__DataOptions__.ProductCategory))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var productCategoryRepository = scope.ServiceProvider.GetRequiredService<IProductCategoryRepository>();
                        var idQuery = new ProductCategoryIdentifier { ProductCategoryID = id.ProductID };
                        var response = await productCategoryRepository.Get(idQuery);
                        responses.TryAdd(ProductCompositeModel.__DataOptions__.ProductCategory, new Response { Status = response.Status, StatusMessage = response.StatusMessage });
                        if(response.Status == HttpStatusCode.OK)
                        {
                            successResponse.ProductCategory = response.ResponseBody;
                        }
                    }
                }));
            }

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _salesOrderDetailRepository = scope.ServiceProvider.GetRequiredService<ISalesOrderDetailRepository>();
                        var query = new SalesOrderDetailAdvancedQuery { ProductID = id.ProductID, PageIndex = 1, PageSize = 5, OrderBys="ModifiedDate~DESC" };
                        var response = await _salesOrderDetailRepository.Search(query);
                        responses.TryAdd(ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID, new Response { Status = response.Status, StatusMessage = response.StatusMessage });
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
            successResponse.Responses = new Dictionary<ProductCompositeModel.__DataOptions__, Response>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ProductIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDModel<ProductIdentifier, ProductDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<ProductIdentifier, ProductDataModel.DefaultView> input)
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

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ProductAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

