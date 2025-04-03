using AdventureWorksLT2019.Models;
using AdventureWorksLT2019.ServiceContracts;
using Framework.Models;
using Framework.Mvc;
using Framework.Mvc.Identity;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AdventureWorksLT2019.WebApiAdminControllers
{
    [ApiController]
    public partial class SiteDataApiController : BaseApiController
    {

        private readonly ILogger<SiteDataApiController> _logger;
        private readonly ClaimService _claimService;

        public SiteDataApiController(

            ILogger<SiteDataApiController> logger,
            ClaimService claimService)
        {

            _logger = logger;
            _claimService = claimService;
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

