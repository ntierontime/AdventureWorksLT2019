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

namespace AdventureWorksLT2019.WebApiClientControllers
{
    [ApiController]
    public partial class ProductDescriptionApiController : BaseApiController
    {
        private readonly ClaimService _claimService;
        private readonly IProductDescriptionService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductDescriptionApiController> _logger;

        public ProductDescriptionApiController(
            IProductDescriptionService thisService
            , ClaimService claimService
            , IServiceProvider serviceProvider
            , ILogger<ProductDescriptionApiController> logger)
        {
            _thisService = thisService;
            _claimService = claimService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        [Authorize()]
        [HttpGet]
        [HttpPost]
        [Route("/api/[controller]/Search")]
        public async Task<ActionResult<ListResponse<ProductDescriptionDataModel[]>>> Search(
            ProductDescriptionAdvancedQuery query)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Search(query, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/api/[controller]/Put/{ProductDescriptionID}")]
        public async Task<ActionResult<Response<ProductDescriptionDataModel>>> Put([FromRoute]ProductDescriptionIdentifier id, [FromBody]ProductDescriptionDataModel input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Update(id, input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpGet]
        [Route("/api/[controller]/Get/{ProductDescriptionID}")]
        public async Task<ActionResult<Response<ProductDescriptionDataModel>>> Get([FromRoute]ProductDescriptionIdentifier id)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Get(id, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPost]
        [Route("/api/[controller]/Post")]
        public async Task<ActionResult<Response<ProductDescriptionDataModel>>> Post(ProductDescriptionDataModel input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Create(input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        /*
        // [Authorize]
        //[HttpGet, ActionName("HeartBeat")]
        [Route("/api/[controller]/HeartBeat")]
        public Task<ActionResult> HeartBeat()
        {
            return Ok();
        }
        */
    }
}

