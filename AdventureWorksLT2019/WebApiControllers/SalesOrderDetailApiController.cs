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
    public partial class SalesOrderDetailApiController : BaseApiController
    {
        ISalesOrderDetailService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SalesOrderDetailApiController> _logger;

        public SalesOrderDetailApiController(ISalesOrderDetailService thisService, IServiceProvider serviceProvider, ILogger<SalesOrderDetailApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<SalesOrderDetailDataModel.DefaultView[]>>> Search(
            SalesOrderDetailAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{SalesOrderID}/{SalesOrderDetailID}")]
        [HttpGet]
        public async Task<ActionResult<SalesOrderDetailCompositeModel>> GetCompositeModel(SalesOrderDetailIdentifier id)
        {
            var serviceResponse = await _thisService.GetCompositeModel(id, null);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult> BulkDelete(List<SalesOrderDetailIdentifier> ids)
        {
            var serviceResponse = await _thisService.BulkDelete(ids);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{SalesOrderID}/{SalesOrderDetailID}")]
        [HttpPut]
        public async Task<ActionResult<SalesOrderDetailDataModel.DefaultView>> Put([FromRoute]SalesOrderDetailIdentifier id, [FromBody]SalesOrderDetailDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{SalesOrderID}/{SalesOrderDetailID}")]
        [HttpGet]
        public async Task<ActionResult<SalesOrderDetailDataModel.DefaultView>> Get([FromRoute]SalesOrderDetailIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<SalesOrderDetailDataModel.DefaultView>> Post(SalesOrderDetailDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{SalesOrderID}/{SalesOrderDetailID}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute]SalesOrderDetailIdentifier id)
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

