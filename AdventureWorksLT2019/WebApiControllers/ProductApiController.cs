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
    public partial class ProductApiController : BaseApiController
    {
        private readonly IProductService _thisService;
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
        public async Task<ActionResult<ListResponse<ProductDataModel.DefaultView[]>>> Search(
            ProductAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductID}")]
        [HttpGet]
        public async Task<ActionResult<ProductCompositeModel>> GetCompositeModel([FromRoute]ProductIdentifier id)
        {
            var listItemRequests = new Dictionary<ProductCompositeModel.__DataOptions__, CompositeListItemRequest>();

            listItemRequests.Add(ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID,
                new CompositeListItemRequest()
                {
                    PageSize = 100,
                    OrderBys = "ModifiedDate",
                    PaginationOption = PaginationOptions.NoPagination,
                });

            var serviceResponse = await _thisService.GetCompositeModel(id, listItemRequests);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductID}")]
        [HttpPut]
        public async Task<ActionResult<Response<ProductDataModel.DefaultView>>> Put([FromRoute]ProductIdentifier id, [FromBody]ProductDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductID}")]
        [HttpGet]
        public async Task<ActionResult<Response<ProductDataModel.DefaultView>>> Get([FromRoute]ProductIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<ProductDataModel.DefaultView>>> Post(ProductDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnActionResult(serviceResponse);
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

