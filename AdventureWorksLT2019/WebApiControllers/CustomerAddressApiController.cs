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
    public partial class CustomerAddressApiController : BaseApiController
    {
        private readonly ICustomerAddressService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CustomerAddressApiController> _logger;

        public CustomerAddressApiController(ICustomerAddressService thisService, IServiceProvider serviceProvider, ILogger<CustomerAddressApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<CustomerAddressDataModel.DefaultView[]>>> Search(
            CustomerAddressAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{CustomerID}/{AddressID}")]
        [HttpGet]
        public async Task<ActionResult<CustomerAddressCompositeModel>> GetCompositeModel([FromRoute]CustomerAddressIdentifier id)
        {
            var listItemRequests = new Dictionary<CustomerAddressCompositeModel.__DataOptions__, CompositeListItemRequest>();

            var serviceResponse = await _thisService.GetCompositeModel(id, listItemRequests);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult> BulkDelete([FromBody]List<CustomerAddressIdentifier> ids)
        {
            var serviceResponse = await _thisService.BulkDelete(ids);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{CustomerID}/{AddressID}")]
        [HttpPut]
        public async Task<ActionResult<Response<CustomerAddressDataModel.DefaultView>>> Put([FromRoute]CustomerAddressIdentifier id, [FromBody]CustomerAddressDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{CustomerID}/{AddressID}")]
        [HttpGet]
        public async Task<ActionResult<Response<CustomerAddressDataModel.DefaultView>>> Get([FromRoute]CustomerAddressIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<CustomerAddressDataModel.DefaultView>>> Post(CustomerAddressDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{CustomerID}/{AddressID}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute]CustomerAddressIdentifier id)
        {
            var serviceResponse = await _thisService.Delete(id);
            return ReturnWithoutBodyActionResult(serviceResponse);
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

