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
        private readonly ISalesOrderDetailService _thisService;
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

