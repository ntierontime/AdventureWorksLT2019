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
    public partial class ProductDescriptionApiController : BaseApiController
    {
        private readonly IProductDescriptionService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductDescriptionApiController> _logger;

        public ProductDescriptionApiController(IProductDescriptionService thisService, IServiceProvider serviceProvider, ILogger<ProductDescriptionApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<ProductDescriptionDataModel[]>>> Search(
            ProductDescriptionAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductDescriptionID}")]
        [HttpGet]
        public async Task<ActionResult<ProductDescriptionCompositeModel>> GetCompositeModel([FromRoute]ProductDescriptionIdentifier id)
        {
            var listItemRequests = new Dictionary<ProductDescriptionCompositeModel.__DataOptions__, CompositeListItemRequest>();

            listItemRequests.Add(ProductDescriptionCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductDescriptionID,
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
        [HttpPut]
        public async Task<ActionResult> BulkDelete([FromBody]List<ProductDescriptionIdentifier> ids)
        {
            var serviceResponse = await _thisService.BulkDelete(ids);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductDescriptionID}")]
        [HttpPut]
        public async Task<ActionResult<Response<ProductDescriptionDataModel>>> Put([FromRoute]ProductDescriptionIdentifier id, [FromBody]ProductDescriptionDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductDescriptionID}")]
        [HttpGet]
        public async Task<ActionResult<Response<ProductDescriptionDataModel>>> Get([FromRoute]ProductDescriptionIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<ProductDescriptionDataModel>>> Post(ProductDescriptionDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductDescriptionID}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute]ProductDescriptionIdentifier id)
        {
            var serviceResponse = await _thisService.Delete(id);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<Response<ProductDescriptionDataModel>>> CreateComposite(ProductDescriptionCompositeModel input)
        {
            var serviceResponse = await _thisService.CreateComposite(input);
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

