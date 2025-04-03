using AdventureWorksLT2019.ServiceContracts;
using Framework.Mvc;
using AdventureWorksLT2019.Models;
using Framework.Models;
using Framework.Mvc.Identity;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace AdventureWorksLT2019.WebApiAdminControllers
{
    [ApiController]
    public partial class ProductModelProductDescriptionApiController : BaseApiController
    {
        private readonly ClaimService _claimService;
        private readonly IProductModelProductDescriptionService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductModelProductDescriptionApiController> _logger;

        public ProductModelProductDescriptionApiController(
            IProductModelProductDescriptionService thisService
            , ClaimService claimService
            , IServiceProvider serviceProvider
            , ILogger<ProductModelProductDescriptionApiController> logger)
        {
            _thisService = thisService;
            _claimService = claimService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        [Authorize()]
        [HttpGet]
        [HttpPost]
        [Route("/adminapi/[controller]/Search")]
        public async Task<ActionResult<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>>> Search(
            ProductModelProductDescriptionAdvancedQuery query)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Search(query, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/BulkUpdate")]
        public async Task<ActionResult<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>>> BulkUpdate([FromBody]BatchActionRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView> data)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.BulkUpdate(data, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/MultiItemsCUD")]
        public async Task<ActionResult<Response<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView>>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView> input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.MultiItemsCUD(input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/Put/{ProductModelID}/{ProductDescriptionID}/{Culture}")]
        public async Task<ActionResult<Response<ProductModelProductDescriptionDataModel.DefaultView>>> Put([FromRoute]ProductModelProductDescriptionIdentifier id, [FromBody]ProductModelProductDescriptionDataModel.DefaultView input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Update(id, input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpGet]
        [Route("/adminapi/[controller]/Get/{ProductModelID}/{ProductDescriptionID}/{Culture}")]
        public async Task<ActionResult<Response<ProductModelProductDescriptionDataModel.DefaultView>>> Get([FromRoute]ProductModelProductDescriptionIdentifier id)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Get(id, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPost]
        [Route("/adminapi/[controller]/Post")]
        public async Task<ActionResult<Response<ProductModelProductDescriptionDataModel.DefaultView>>> Post(ProductModelProductDescriptionDataModel.DefaultView input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Create(input, claimsModel);
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

