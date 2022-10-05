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
    public partial class ProductModelProductDescriptionApiController : BaseApiController
    {
        private readonly IProductModelProductDescriptionService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductModelProductDescriptionApiController> _logger;

        public ProductModelProductDescriptionApiController(IProductModelProductDescriptionService thisService, IServiceProvider serviceProvider, ILogger<ProductModelProductDescriptionApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>>> Search(
            ProductModelProductDescriptionAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductModelID}/{ProductDescriptionID}/{Culture}")]
        [HttpGet]
        public async Task<ActionResult<ProductModelProductDescriptionCompositeModel>> GetCompositeModel([FromRoute]ProductModelProductDescriptionIdentifier id)
        {
            var listItemRequests = new Dictionary<ProductModelProductDescriptionCompositeModel.__DataOptions__, CompositeListItemRequest>();

            var serviceResponse = await _thisService.GetCompositeModel(id, listItemRequests);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult> BulkDelete([FromBody]List<ProductModelProductDescriptionIdentifier> ids)
        {
            var serviceResponse = await _thisService.BulkDelete(ids);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductModelID}/{ProductDescriptionID}/{Culture}")]
        [HttpPut]
        public async Task<ActionResult<Response<ProductModelProductDescriptionDataModel.DefaultView>>> Put([FromRoute]ProductModelProductDescriptionIdentifier id, [FromBody]ProductModelProductDescriptionDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductModelID}/{ProductDescriptionID}/{Culture}")]
        [HttpGet]
        public async Task<ActionResult<Response<ProductModelProductDescriptionDataModel.DefaultView>>> Get([FromRoute]ProductModelProductDescriptionIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<ProductModelProductDescriptionDataModel.DefaultView>>> Post(ProductModelProductDescriptionDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductModelID}/{ProductDescriptionID}/{Culture}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute]ProductModelProductDescriptionIdentifier id)
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

