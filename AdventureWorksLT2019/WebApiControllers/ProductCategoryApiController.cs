using AdventureWorksLT2019.ServiceContracts;
using Framework.Mvc;
using AdventureWorksLT2019.Models;
using Framework.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AdventureWorksLT2019.WebApiControllers
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public partial class ProductCategoryApiController : BaseApiController
    {
        IProductCategoryService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductCategoryApiController> _logger;

        public ProductCategoryApiController(IProductCategoryService thisService, IServiceProvider serviceProvider, ILogger<ProductCategoryApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }
        
        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetCodeList(
            ProductCategoryAdvancedQuery query)
        {
            var serviceResponse = await _thisService.GetCodeList(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<ProductCategoryDataModel.DefaultView[]>>> Search(
            ProductCategoryAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductCategoryID}")]
        [HttpGet]
        public async Task<ActionResult<ProductCategoryCompositeModel>> GetCompositeModel(ProductCategoryIdentifier id)
        {
            var serviceResponse = await _thisService.GetCompositeModel(id, null);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult> BulkDelete(List<ProductCategoryIdentifier> ids)
        {
            var serviceResponse = await _thisService.BulkDelete(ids);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductCategoryID}")]
        [HttpPut]
        public async Task<ActionResult<ProductCategoryDataModel.DefaultView>> Put([FromRoute]ProductCategoryIdentifier id, [FromBody]ProductCategoryDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductCategoryID}")]
        [HttpGet]
        public async Task<ActionResult<ProductCategoryDataModel.DefaultView>> Get([FromRoute]ProductCategoryIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductCategoryDataModel.DefaultView>> Post(ProductCategoryDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductCategoryID}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute]ProductCategoryIdentifier id)
        {
            var serviceResponse = await _thisService.Delete(id);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        /*
        // [Authorize]
        [HttpGet, ActionName("HeartBeat")]
        public Task<ActionResult> HeartBeat()
        {
            return Ok();
        }
        */
    }
}

