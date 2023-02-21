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
    public partial class SalesOrderHeaderApiController : BaseApiController
    {
        private readonly ISalesOrderHeaderService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SalesOrderHeaderApiController> _logger;

        public SalesOrderHeaderApiController(ISalesOrderHeaderService thisService, IServiceProvider serviceProvider, ILogger<SalesOrderHeaderApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>>> Search(
            SalesOrderHeaderAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{SalesOrderID}")]
        [HttpGet]
        public async Task<ActionResult<SalesOrderHeaderCompositeModel>> GetCompositeModel([FromRoute]SalesOrderHeaderIdentifier id)
        {
            var listItemRequests = new Dictionary<SalesOrderHeaderCompositeModel.__DataOptions__, CompositeListItemRequest>();

            listItemRequests.Add(SalesOrderHeaderCompositeModel.__DataOptions__.SalesOrderDetails_Via_SalesOrderID,
                new CompositeListItemRequest()
                {
                    PageSize = 100,
                    OrderBys = "ModifiedDate",
                    PaginationOption = PaginationOptions.NoPagination,
                });

            var serviceResponse = await _thisService.GetCompositeModel(id, listItemRequests);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>>> BulkUpdate([FromBody]BatchActionRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView> data)
        {
            var serviceResponse = await _thisService.BulkUpdate(data);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{SalesOrderID}")]
        [HttpPut]
        public async Task<ActionResult<Response<SalesOrderHeaderDataModel.DefaultView>>> Put([FromRoute]SalesOrderHeaderIdentifier id, [FromBody]SalesOrderHeaderDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{SalesOrderID}")]
        [HttpGet]
        public async Task<ActionResult<Response<SalesOrderHeaderDataModel.DefaultView>>> Get([FromRoute]SalesOrderHeaderIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<SalesOrderHeaderDataModel.DefaultView>>> Post(SalesOrderHeaderDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
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

