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
    public partial class ProductCategoryApiController : BaseApiController
    {
        private readonly ClaimService _claimService;
        private readonly IProductCategoryService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductCategoryApiController> _logger;

        public ProductCategoryApiController(
            IProductCategoryService thisService
            , ClaimService claimService
            , IServiceProvider serviceProvider
            , ILogger<ProductCategoryApiController> logger)
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
        public async Task<ActionResult<ListResponse<ProductCategoryDataModel.DefaultView[]>>> Search(
            ProductCategoryAdvancedQuery query)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Search(query, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/Put/{ProductCategoryID}")]
        public async Task<ActionResult<Response<ProductCategoryDataModel.DefaultView>>> Put([FromRoute]ProductCategoryIdentifier id, [FromBody]ProductCategoryDataModel.DefaultView input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Update(id, input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpGet]
        [Route("/adminapi/[controller]/Get/{ProductCategoryID}")]
        public async Task<ActionResult<Response<ProductCategoryDataModel.DefaultView>>> Get([FromRoute]ProductCategoryIdentifier id)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Get(id, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPost]
        [Route("/adminapi/[controller]/Post")]
        public async Task<ActionResult<Response<ProductCategoryDataModel.DefaultView>>> Post(ProductCategoryDataModel.DefaultView input)
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

