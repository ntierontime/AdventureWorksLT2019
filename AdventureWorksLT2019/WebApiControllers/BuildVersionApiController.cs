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
    public partial class BuildVersionApiController : BaseApiController
    {
        private readonly IBuildVersionService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BuildVersionApiController> _logger;

        public BuildVersionApiController(IBuildVersionService thisService, IServiceProvider serviceProvider, ILogger<BuildVersionApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<BuildVersionDataModel[]>>> Search(
            BuildVersionAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{SystemInformationID}/{VersionDate}/{ModifiedDate}")]
        [HttpGet]
        public async Task<ActionResult<BuildVersionCompositeModel>> GetCompositeModel([FromRoute]BuildVersionIdentifier id)
        {
            var listItemRequests = new Dictionary<BuildVersionCompositeModel.__DataOptions__, CompositeListItemRequest>();

            var serviceResponse = await _thisService.GetCompositeModel(id, listItemRequests);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult> BulkDelete([FromBody]List<BuildVersionIdentifier> ids)
        {
            var serviceResponse = await _thisService.BulkDelete(ids);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{SystemInformationID}/{VersionDate}/{ModifiedDate}")]
        [HttpPut]
        public async Task<ActionResult<Response<BuildVersionDataModel>>> Put([FromRoute]BuildVersionIdentifier id, [FromBody]BuildVersionDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{SystemInformationID}/{VersionDate}/{ModifiedDate}")]
        [HttpGet]
        public async Task<ActionResult<Response<BuildVersionDataModel>>> Get([FromRoute]BuildVersionIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<BuildVersionDataModel>>> Post(BuildVersionDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{SystemInformationID}/{VersionDate}/{ModifiedDate}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute]BuildVersionIdentifier id)
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

