using AdventureWorksLT2019.Models;
using AdventureWorksLT2019.RepositoryContracts;
using Framework.Models;

using Microsoft.AspNetCore.Mvc;

namespace AdventureWorksLT2019.MvcWebApp.ApiControllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class SelectListsApiController : Controller
    {
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<SelectListsApiController> _logger;

        public SelectListsApiController(
            IServiceScopeFactory serviceScopeFactor,
            ILogger<SelectListsApiController> logger)
        {
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetBuildVersionCodeList(
            [FromQuery]BuildVersionAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var buildVersionRepository = scope.ServiceProvider.GetRequiredService<IBuildVersionRepository>();
                var serviceResponse = await buildVersionRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetErrorLogCodeList(
            [FromQuery]ErrorLogAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var errorLogRepository = scope.ServiceProvider.GetRequiredService<IErrorLogRepository>();
                var serviceResponse = await errorLogRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetAddressCodeList(
            [FromQuery]AddressAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var addressRepository = scope.ServiceProvider.GetRequiredService<IAddressRepository>();
                var serviceResponse = await addressRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetCustomerCodeList(
            [FromQuery]CustomerAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                var serviceResponse = await customerRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetCustomerAddressCodeList(
            [FromQuery]CustomerAddressAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var customerAddressRepository = scope.ServiceProvider.GetRequiredService<ICustomerAddressRepository>();
                var serviceResponse = await customerAddressRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetProductCodeList(
            [FromQuery]ProductAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                var serviceResponse = await productRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetProductCategoryCodeList(
            [FromQuery]ProductCategoryAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var productCategoryRepository = scope.ServiceProvider.GetRequiredService<IProductCategoryRepository>();
                var serviceResponse = await productCategoryRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetProductDescriptionCodeList(
            [FromQuery]ProductDescriptionAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var productDescriptionRepository = scope.ServiceProvider.GetRequiredService<IProductDescriptionRepository>();
                var serviceResponse = await productDescriptionRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetProductModelCodeList(
            [FromQuery]ProductModelAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var productModelRepository = scope.ServiceProvider.GetRequiredService<IProductModelRepository>();
                var serviceResponse = await productModelRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetProductModelProductDescriptionCodeList(
            [FromQuery]ProductModelProductDescriptionAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var productModelProductDescriptionRepository = scope.ServiceProvider.GetRequiredService<IProductModelProductDescriptionRepository>();
                var serviceResponse = await productModelProductDescriptionRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetSalesOrderDetailCodeList(
            [FromQuery]SalesOrderDetailAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var salesOrderDetailRepository = scope.ServiceProvider.GetRequiredService<ISalesOrderDetailRepository>();
                var serviceResponse = await salesOrderDetailRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetSalesOrderHeaderCodeList(
            [FromQuery]SalesOrderHeaderAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var salesOrderHeaderRepository = scope.ServiceProvider.GetRequiredService<ISalesOrderHeaderRepository>();
                var serviceResponse = await salesOrderHeaderRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

    }
}

