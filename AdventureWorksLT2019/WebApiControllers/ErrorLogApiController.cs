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
    public partial class ErrorLogApiController : BaseApiController
    {
        IErrorLogService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ErrorLogApiController> _logger;

        public ErrorLogApiController(IErrorLogService thisService, IServiceProvider serviceProvider, ILogger<ErrorLogApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<ErrorLogDataModel[]>>> Search(
            ErrorLogAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ErrorLogID}")]
        [HttpGet]
        public async Task<ActionResult<ErrorLogCompositeModel>> GetCompositeModel(ErrorLogIdentifier id)
        {
            var serviceResponse = await _thisService.GetCompositeModel(id, null);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult> BulkDelete(List<ErrorLogIdentifier> ids)
        {
            var serviceResponse = await _thisService.BulkDelete(ids);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ErrorLogID}")]
        [HttpPut]
        public async Task<ActionResult<Response<ErrorLogDataModel>>> Put([FromRoute]ErrorLogIdentifier id, [FromBody]ErrorLogDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ErrorLogID}")]
        [HttpGet]
        public async Task<ActionResult<Response<ErrorLogDataModel>>> Get([FromRoute]ErrorLogIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<ErrorLogDataModel>>> Post(ErrorLogDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ErrorLogID}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute]ErrorLogIdentifier id)
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

