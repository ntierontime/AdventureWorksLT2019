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
    public partial class CustomerAddressApiController : BaseApiController
    {
        private readonly ClaimService _claimService;
        private readonly ICustomerAddressService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CustomerAddressApiController> _logger;

        public CustomerAddressApiController(
            ICustomerAddressService thisService
            , ClaimService claimService
            , IServiceProvider serviceProvider
            , ILogger<CustomerAddressApiController> logger)
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
        public async Task<ActionResult<ListResponse<CustomerAddressDataModel.DefaultView[]>>> Search(
            CustomerAddressAdvancedQuery query)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Search(query, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/BulkUpdate")]
        public async Task<ActionResult<ListResponse<CustomerAddressDataModel.DefaultView[]>>> BulkUpdate([FromBody]BatchActionRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView> data)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.BulkUpdate(data, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/MultiItemsCUD")]
        public async Task<ActionResult<Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>>>> MultiItemsCUD(
            MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView> input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.MultiItemsCUD(input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/Put/{CustomerID}/{AddressID}")]
        public async Task<ActionResult<Response<CustomerAddressDataModel.DefaultView>>> Put([FromRoute]CustomerAddressIdentifier id, [FromBody]CustomerAddressDataModel.DefaultView input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Update(id, input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpGet]
        [Route("/adminapi/[controller]/Get/{CustomerID}/{AddressID}")]
        public async Task<ActionResult<Response<CustomerAddressDataModel.DefaultView>>> Get([FromRoute]CustomerAddressIdentifier id)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Get(id, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPost]
        [Route("/adminapi/[controller]/Post")]
        public async Task<ActionResult<Response<CustomerAddressDataModel.DefaultView>>> Post(CustomerAddressDataModel.DefaultView input)
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

