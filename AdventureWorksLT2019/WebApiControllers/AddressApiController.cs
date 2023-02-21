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
    public partial class AddressApiController : BaseApiController
    {
        private readonly IAddressService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AddressApiController> _logger;

        public AddressApiController(IAddressService thisService, IServiceProvider serviceProvider, ILogger<AddressApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<AddressDataModel[]>>> Search(
            AddressAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{AddressID}")]
        [HttpGet]
        public async Task<ActionResult<AddressCompositeModel>> GetCompositeModel([FromRoute]AddressIdentifier id)
        {
            var listItemRequests = new Dictionary<AddressCompositeModel.__DataOptions__, CompositeListItemRequest>();

            listItemRequests.Add(AddressCompositeModel.__DataOptions__.CustomerAddresses_Via_AddressID,
                new CompositeListItemRequest()
                {
                    PageSize = 100,
                    OrderBys = "ModifiedDate",
                    PaginationOption = PaginationOptions.NoPagination,
                });

            listItemRequests.Add(AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_BillToAddressID,
                new CompositeListItemRequest()
                {
                    PageSize = 100,
                    OrderBys = "OrderDate",
                    PaginationOption = PaginationOptions.NoPagination,
                });

            listItemRequests.Add(AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_ShipToAddressID,
                new CompositeListItemRequest()
                {
                    PageSize = 100,
                    OrderBys = "OrderDate",
                    PaginationOption = PaginationOptions.NoPagination,
                });

            var serviceResponse = await _thisService.GetCompositeModel(id, listItemRequests);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult> BulkDelete([FromBody]List<AddressIdentifier> ids)
        {
            var serviceResponse = await _thisService.BulkDelete(ids);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{AddressID}")]
        [HttpPut]
        public async Task<ActionResult<Response<AddressDataModel>>> Put([FromRoute]AddressIdentifier id, [FromBody]AddressDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{AddressID}")]
        [HttpGet]
        public async Task<ActionResult<Response<AddressDataModel>>> Get([FromRoute]AddressIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<AddressDataModel>>> Post(AddressDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{AddressID}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute]AddressIdentifier id)
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

