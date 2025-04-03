using AdventureWorksLT2019.ServiceContracts;
using AdventureWorksLT2019.Models;
using Framework.Mvc;
using Framework.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventureWorksLT2019.WebApiAdminControllers
{
    [ApiController]
    public partial class CodeListsApiController : BaseApiController
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CodeListsApiController> _logger;

        public CodeListsApiController(IServiceProvider serviceProvider, ILogger<CodeListsApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/GetBuildVersionCodeList")]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetBuildVersionCodeList(
            BuildVersionAdvancedQuery query)
        {
            var thisService = _serviceProvider.GetService<IBuildVersionService>();
            var serviceResponse = await thisService!.GetCodeList(query, null);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/GetErrorLogCodeList")]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetErrorLogCodeList(
            ErrorLogAdvancedQuery query)
        {
            var thisService = _serviceProvider.GetService<IErrorLogService>();
            var serviceResponse = await thisService!.GetCodeList(query, null);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/GetAddressCodeList")]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetAddressCodeList(
            AddressAdvancedQuery query)
        {
            var thisService = _serviceProvider.GetService<IAddressService>();
            var serviceResponse = await thisService!.GetCodeList(query, null);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/GetCustomerCodeList")]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetCustomerCodeList(
            CustomerAdvancedQuery query)
        {
            var thisService = _serviceProvider.GetService<ICustomerService>();
            var serviceResponse = await thisService!.GetCodeList(query, null);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/GetCustomerAddressCodeList")]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetCustomerAddressCodeList(
            CustomerAddressAdvancedQuery query)
        {
            var thisService = _serviceProvider.GetService<ICustomerAddressService>();
            var serviceResponse = await thisService!.GetCodeList(query, null);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/GetProductCodeList")]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetProductCodeList(
            ProductAdvancedQuery query)
        {
            var thisService = _serviceProvider.GetService<IProductService>();
            var serviceResponse = await thisService!.GetCodeList(query, null);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/GetProductCategoryCodeList")]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetProductCategoryCodeList(
            ProductCategoryAdvancedQuery query)
        {
            var thisService = _serviceProvider.GetService<IProductCategoryService>();
            var serviceResponse = await thisService!.GetCodeList(query, null);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/GetProductDescriptionCodeList")]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetProductDescriptionCodeList(
            ProductDescriptionAdvancedQuery query)
        {
            var thisService = _serviceProvider.GetService<IProductDescriptionService>();
            var serviceResponse = await thisService!.GetCodeList(query, null);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/GetProductModelCodeList")]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetProductModelCodeList(
            ProductModelAdvancedQuery query)
        {
            var thisService = _serviceProvider.GetService<IProductModelService>();
            var serviceResponse = await thisService!.GetCodeList(query, null);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/GetProductModelProductDescriptionCodeList")]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetProductModelProductDescriptionCodeList(
            ProductModelProductDescriptionAdvancedQuery query)
        {
            var thisService = _serviceProvider.GetService<IProductModelProductDescriptionService>();
            var serviceResponse = await thisService!.GetCodeList(query, null);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/GetSalesOrderDetailCodeList")]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetSalesOrderDetailCodeList(
            SalesOrderDetailAdvancedQuery query)
        {
            var thisService = _serviceProvider.GetService<ISalesOrderDetailService>();
            var serviceResponse = await thisService!.GetCodeList(query, null);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/GetSalesOrderHeaderCodeList")]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetSalesOrderHeaderCodeList(
            SalesOrderHeaderAdvancedQuery query)
        {
            var thisService = _serviceProvider.GetService<ISalesOrderHeaderService>();
            var serviceResponse = await thisService!.GetCodeList(query, null);
            return ReturnActionResult(serviceResponse);
        }

        /*
        // [Authorize]
        //[HttpGet, ActionName("HeartBeat")]
        [Route("/adminapi/[controller]/HeartBeat")]
        public Task<ActionResult> HeartBeat()
        {
            return Ok();
        }
        */
    }
}

