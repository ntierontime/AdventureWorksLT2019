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
        private readonly IProductModelService _thisService;
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
        public async Task<ActionResult<ProductModelCompositeModel>> GetCompositeModel([FromRoute]ProductModelIdentifier id)
        {
            var listItemRequests = new Dictionary<ProductModelCompositeModel.__DataOptions__, CompositeListItemRequest>();

            listItemRequests.Add(ProductModelCompositeModel.__DataOptions__.Products_Via_ProductModelID,
                new CompositeListItemRequest()
                {
                    PageSize = 100,
                    OrderBys = "SellStartDate",
                    PaginationOption = PaginationOptions.NoPagination,
                });

            listItemRequests.Add(ProductModelCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductModelID,
                new CompositeListItemRequest()
                {
                    PageSize = 100,
                    OrderBys = "ModifiedDate",
                    PaginationOption = PaginationOptions.NoPagination,
                });

            var serviceResponse = await _thisService.GetCompositeModel(id, listItemRequests);
            return Ok(serviceResponse);
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

