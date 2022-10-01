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
    public partial class ProductModelApiController : BaseApiController
    {
        IProductModelService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductModelApiController> _logger;

        public ProductModelApiController(IProductModelService thisService, IServiceProvider serviceProvider, ILogger<ProductModelApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<ProductModelDataModel[]>>> Search(
            ProductModelAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductModelID}")]
        [HttpGet]
        public async Task<ActionResult<ProductModelCompositeModel>> GetCompositeModel(ProductModelIdentifier id)
        {
            var serviceResponse = await _thisService.GetCompositeModel(id, null);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult> BulkDelete(List<ProductModelIdentifier> ids)
        {
            var serviceResponse = await _thisService.BulkDelete(ids);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductModelID}")]
        [HttpPut]
        public async Task<ActionResult<Response<ProductModelDataModel>>> Put([FromRoute]ProductModelIdentifier id, [FromBody]ProductModelDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductModelID}")]
        [HttpGet]
        public async Task<ActionResult<Response<ProductModelDataModel>>> Get([FromRoute]ProductModelIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<ProductModelDataModel>>> Post(ProductModelDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductModelID}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute]ProductModelIdentifier id)
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

