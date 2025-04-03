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
    public partial class SalesOrderHeaderApiController : BaseApiController
    {
        private readonly ClaimService _claimService;
        private readonly ISalesOrderHeaderService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SalesOrderHeaderApiController> _logger;

        public SalesOrderHeaderApiController(
            ISalesOrderHeaderService thisService
            , ClaimService claimService
            , IServiceProvider serviceProvider
            , ILogger<SalesOrderHeaderApiController> logger)
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
        public async Task<ActionResult<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>>> Search(
            SalesOrderHeaderAdvancedQuery query)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Search(query, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/api/[controller]/Put/{SalesOrderID}")]
        public async Task<ActionResult<Response<SalesOrderHeaderDataModel.DefaultView>>> Put([FromRoute]SalesOrderHeaderIdentifier id, [FromBody]SalesOrderHeaderDataModel.DefaultView input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Update(id, input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpGet]
        [Route("/api/[controller]/Get/{SalesOrderID}")]
        public async Task<ActionResult<Response<SalesOrderHeaderDataModel.DefaultView>>> Get([FromRoute]SalesOrderHeaderIdentifier id)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Get(id, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPost]
        [Route("/api/[controller]/Post")]
        public async Task<ActionResult<Response<SalesOrderHeaderDataModel.DefaultView>>> Post(SalesOrderHeaderDataModel.DefaultView input)
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

