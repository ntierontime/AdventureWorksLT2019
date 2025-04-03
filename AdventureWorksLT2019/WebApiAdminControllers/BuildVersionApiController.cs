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
    public partial class BuildVersionApiController : BaseApiController
    {
        private readonly ClaimService _claimService;
        private readonly IBuildVersionService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BuildVersionApiController> _logger;

        public BuildVersionApiController(
            IBuildVersionService thisService
            , ClaimService claimService
            , IServiceProvider serviceProvider
            , ILogger<BuildVersionApiController> logger)
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
        public async Task<ActionResult<ListResponse<BuildVersionDataModel[]>>> Search(
            BuildVersionAdvancedQuery query)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Search(query, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/BulkUpdate")]
        public async Task<ActionResult<ListResponse<BuildVersionDataModel[]>>> BulkUpdate([FromBody]BatchActionRequest<BuildVersionIdentifier, BuildVersionDataModel> data)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.BulkUpdate(data, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/MultiItemsCUD")]
        public async Task<ActionResult<Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>>>> MultiItemsCUD(
            MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel> input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.MultiItemsCUD(input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPut]
        [Route("/adminapi/[controller]/Put/{SystemInformationID}/{VersionDate}/{ModifiedDate}")]
        public async Task<ActionResult<Response<BuildVersionDataModel>>> Put([FromRoute]BuildVersionIdentifier id, [FromBody]BuildVersionDataModel input)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Update(id, input, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpGet]
        [Route("/adminapi/[controller]/Get/{SystemInformationID}/{VersionDate}/{ModifiedDate}")]
        public async Task<ActionResult<Response<BuildVersionDataModel>>> Get([FromRoute]BuildVersionIdentifier id)
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(User);
            var serviceResponse = await _thisService.Get(id, claimsModel);
            return ReturnActionResult(serviceResponse);
        }

        [Authorize()]
        [HttpPost]
        [Route("/adminapi/[controller]/Post")]
        public async Task<ActionResult<Response<BuildVersionDataModel>>> Post(BuildVersionDataModel input)
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

