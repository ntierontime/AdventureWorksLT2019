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
    public partial class CustomerApiController : BaseApiController
    {
        ICustomerService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CustomerApiController> _logger;

        public CustomerApiController(ICustomerService thisService, IServiceProvider serviceProvider, ILogger<CustomerApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<CustomerDataModel[]>>> Search(
            CustomerAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{CustomerID}")]
        [HttpGet]
        public async Task<ActionResult<CustomerCompositeModel>> GetCompositeModel(CustomerIdentifier id)
        {
            var serviceResponse = await _thisService.GetCompositeModel(id, null);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult> BulkDelete([FromBody]List<CustomerIdentifier> ids)
        {
            var serviceResponse = await _thisService.BulkDelete(ids);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<ListResponse<CustomerDataModel[]>>> BulkUpdate([FromBody(EmptyBodyBehavior = Microsoft.AspNetCore.Mvc.ModelBinding.EmptyBodyBehavior.Allow)]BatchActionRequest<CustomerIdentifier, CustomerDataModel> data)
        {
            var serviceResponse = await _thisService.BulkUpdate(data);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{CustomerID}")]
        [HttpPut]
        public async Task<ActionResult<Response<CustomerDataModel>>> Put([FromRoute]CustomerIdentifier id, [FromBody]CustomerDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{CustomerID}")]
        [HttpGet]
        public async Task<ActionResult<Response<CustomerDataModel>>> Get([FromRoute]CustomerIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<CustomerDataModel>>> Post(CustomerDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{CustomerID}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute]CustomerIdentifier id)
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

