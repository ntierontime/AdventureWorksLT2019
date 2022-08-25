using AdventureWorksLT2019.ServiceContracts;
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
    public partial class ProductApiController : BaseApiController
    {
        IProductService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductApiController> _logger;

        public ProductApiController(IProductService thisService, IServiceProvider serviceProvider, ILogger<ProductApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<PagedResponse<ProductDataModel.DefaultView[]>>> Search(
            ProductAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductID}")]
        [HttpGet]
        public async Task<ActionResult<ProductCompositeModel>> GetCompositeModel(ProductIdentifier id)
        {
            var serviceResponse = await _thisService.GetCompositeModel(id, null);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductID}")]
        [HttpPut]
        public async Task<ActionResult<ProductDataModel.DefaultView>> Put([FromRoute]ProductIdentifier id, [FromBody]ProductDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductID}")]
        [HttpGet]
        public async Task<ActionResult<ProductDataModel.DefaultView>> Get([FromRoute]ProductIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductDataModel.DefaultView>> Post(ProductDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnResultOnlyActionResult(serviceResponse);
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

