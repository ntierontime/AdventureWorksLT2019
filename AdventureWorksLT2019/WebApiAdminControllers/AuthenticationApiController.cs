using AdventureWorksLT2019.Models;
using AdventureWorksLT2019.Models.Definitions;
using Framework.Mvc.Identity;
using Framework.Mvc.Identity.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Security.Claims;

namespace AdventureWorksLT2019.WebApiAdminControllers
{
    public class UserInfo
    {
        public IList<string>? Roles { get; set; }
        public  string? Person { get; set; }
        public string[]? Notifications { get; set; }
        public UserData? UserData { get; set; }
    }

    [ApiController]
    public class AuthenticationApiController : Controller
    {
        private readonly ClaimService _claimService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public AuthenticationApiController(
            ClaimService claimService,
            UserManager<ApplicationUser> userManager,
            ILogger<AuthenticationApiController> logger
            )
        {
            _claimService = claimService;
            _userManager = userManager;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = "Identity.BearerAndApplication")]
        [HttpGet]
        [Route("/GetUserInfo")]
        public async Task<ActionResult<UserInfo?>> GetUserInfo()
        {
            var claimsModel = _claimService.GetFromClaimPrinciple(HttpContext.User);
            if (string.IsNullOrEmpty(claimsModel.AspNetUserName) ||
                claimsModel.PersonID == -1)
                return await Task.FromResult<UserInfo?>(new UserInfo
                {
                    // Roles = new string[] { AspNetRolesOptions.BasicUser.ToString() },
                });
            var curUser = await _userManager.FindByEmailAsync(claimsModel.AspNetUserName);
            var roles = await _userManager.GetRolesAsync(curUser!);
            var response = new UserInfo
            {
                Roles = roles,
            };

            //var personResponse = await _personService.Get(new PersonIdentifier { EntityID = claimsModel.PersonID }, null); // no ClaimsModel for now
            //if (personResponse.Status == System.Net.HttpStatusCode.OK)
            //{
            //    response.Person = personResponse.ResponseBody;

            //    // Please manually match which column is the PersonId in Notification table
            //    var notificationsResponse = await _notificationService.Search(new NotificationAdvancedQuery { EntityID = claimsModel.PersonID }, null); // no ClaimsModel for now
            //    if (notificationsResponse.Status == System.Net.HttpStatusCode.OK)
            //    {
            //        response.Notifications = notificationsResponse.ResponseBody;
            //    }
            //}

            var claims = await _userManager.GetClaimsAsync(curUser!);
            if (claims.Any(t => t.Type == ClaimTypes.UserData))
            {
                var userDataInString = claims.First(t => t.Type == ClaimTypes.UserData).Value;
                response.UserData = JsonSerializer.Deserialize<UserData>(userDataInString, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }

            return await Task.FromResult(Ok(response));
        }
    }
}

