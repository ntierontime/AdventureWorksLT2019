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
    public partial class SalesOrderDetailApiController : BaseApiController
    {
        private readonly ClaimService _claimService;
        private readonly ISalesOrderDetailService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SalesOrderDetailApiController> _logger;

        public SalesOrderDetailApiController(
            ISalesOrderDetailService thisService
            , ClaimService claimService
            , IServiceProvider serviceProvider
            , ILogger<SalesOrderDetailApiController> logger)
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
        public async Task<ActionResult<ListResponse<SalesOrderDetailDataModel.DefaultView[]>>> Search(
            SalesOrderDetailAdvancedQuery query)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Search(query, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/BulkUpdate")]
        public async Task<ActionResult<ListResponse<SalesOrderDetailDataModel.DefaultView[]>>> BulkUpdate([FromBody]BatchActionRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> data)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.BulkUpdate(data, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/MultiItemsCUD")]
        public async Task<ActionResult<Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>>>> MultiItemsCUD(
            MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.MultiItemsCUD(input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/Put/{SalesOrderID}/{SalesOrderDetailID}")]
        public async Task<ActionResult<Response<SalesOrderDetailDataModel.DefaultView>>> Put([FromRoute]SalesOrderDetailIdentifier id, [FromBody]SalesOrderDetailDataModel.DefaultView input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Update(id, input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpGet]
        [Route("/adminapi/[controller]/Get/{SalesOrderID}/{SalesOrderDetailID}")]
        public async Task<ActionResult<Response<SalesOrderDetailDataModel.DefaultView>>> Get([FromRoute]SalesOrderDetailIdentifier id)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Get(id, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPost]
        [Route("/adminapi/[controller]/Post")]
        public async Task<ActionResult<Response<SalesOrderDetailDataModel.DefaultView>>> Post(SalesOrderDetailDataModel.DefaultView input)
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

