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
    public partial class CustomerApiController : BaseApiController
    {
        private readonly ClaimService _claimService;
        private readonly ICustomerService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CustomerApiController> _logger;

        public CustomerApiController(
            ICustomerService thisService
            , ClaimService claimService
            , IServiceProvider serviceProvider
            , ILogger<CustomerApiController> logger)
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
        public async Task<ActionResult<ListResponse<CustomerDataModel[]>>> Search(
            CustomerAdvancedQuery query)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Search(query, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/Put/{CustomerID}")]
        public async Task<ActionResult<Response<CustomerDataModel>>> Put([FromRoute]CustomerIdentifier id, [FromBody]CustomerDataModel input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Update(id, input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpGet]
        [Route("/adminapi/[controller]/Get/{CustomerID}")]
        public async Task<ActionResult<Response<CustomerDataModel>>> Get([FromRoute]CustomerIdentifier id)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Get(id, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPost]
        [Route("/adminapi/[controller]/Post")]
        public async Task<ActionResult<Response<CustomerDataModel>>> Post(CustomerDataModel input)
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

