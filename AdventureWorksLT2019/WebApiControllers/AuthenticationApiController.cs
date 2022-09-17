using Framework.Models.Account;
using Framework.Mvc;
using Framework.Mvc.Identity;
using Framework.Mvc.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AdventureWorksLT2019.WebApiControllers
{
    [Route("/api/[controller]/[action]")]
    public class AuthenticationApiController : Controller
    {
        private readonly UserManager<Framework.Mvc.Identity.Data.ApplicationUser> _userManager;
        private readonly SignInManager<Framework.Mvc.Identity.Data.ApplicationUser> _signInManager;
        private readonly Framework.Mvc.Identity.IdentitySecret _identitySecret;
        private readonly Framework.Mvc.IEmailSender _emailSender;
        private readonly ILogger _logger;

        public AuthenticationApiController(
            UserManager<Framework.Mvc.Identity.Data.ApplicationUser> userManager,
            SignInManager<Framework.Mvc.Identity.Data.ApplicationUser> signInManager,
            Framework.Mvc.IEmailSender emailSender,
            IOptions<Framework.Mvc.Identity.IdentitySecret> identitySecret,
            ILogger<AuthenticationApiController> logger
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identitySecret = identitySecret.Value;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpPost]
        public async Task<Framework.Models.Account.AuthenticationResponse> Login([FromBody] Framework.Models.Account.LoginRequest model)
        {
            return await Task.FromResult(new Framework.Models.Account.AuthenticationResponse
            {
                Succeeded = true,
                Token = "TestToken",
                ExpiresIn = 3600,
                RefreshToken = "TestRefreshToken",
                Roles = new[] { "Administrator" },
            });
            //var user = await _userManager.FindByNameAsync(model.Email);
            //var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            //return await GetAuthenticationResponse(user, result);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<Framework.Models.Account.AuthenticationResponse> Logout([FromBody] Framework.Models.Account.LoginRequest model)
        {
            // TODO: set a flag? last log out time
            return new Framework.Models.Account.AuthenticationResponse { Succeeded = true };

            //return await _signInManager.SignOutAsync();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [HttpPost]
        public async Task<Framework.Models.Account.AuthenticationResponse> Register([FromBody] Framework.Models.Account.RegisterRequest model)
        {
            var LoginRequest = new Framework.Models.Account.LoginRequest { Email = model.Email, Password = model.Password };
            return await Login(LoginRequest);

            if (!ModelState.IsValid)
            {
                return new Framework.Models.Account.AuthenticationResponse { Succeeded = false };
            }

            var user = new Framework.Mvc.Identity.Data.ApplicationUser() { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new Framework.Models.Account.AuthenticationResponse { Succeeded = false };
            }
            else
            {
                // This is a copy from Register method in AccountController.
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                // TODO: load more data
            }

            var loginRequest = new Framework.Models.Account.LoginRequest { Email = model.Email, Password = model.Password };

            return await Login(loginRequest);
        }

        private async Task<Framework.Models.Account.AuthenticationResponse> GetAuthenticationResponse(
            Framework.Mvc.Identity.Data.ApplicationUser user
            , Microsoft.AspNetCore.Identity.SignInResult result)
        {
            var loginResponse = new Framework.Models.Account.AuthenticationResponse
            {
                Succeeded = result.Succeeded
                ,
                IsLockedOut = result.IsLockedOut
                ,
                IsNotAllowed = result.IsNotAllowed
                ,
                RequiresTwoFactor = result.RequiresTwoFactor
            };

            // authentication successful, then generate jwt token
            string tokenInString = Framework.Mvc.Identity.CustomizedClaimTypes.GetJwtSecurityTokenInString(user.Id.ToLower(), _identitySecret.Secret);
            loginResponse.Token = tokenInString;

            // Load LogIn User related data
            if (loginResponse.Succeeded)
            {
                loginResponse.Roles = await _userManager.GetRolesAsync(user);

                //// TODO: Load more data to LoginResponse
            }
            return loginResponse;
        }

        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return StatusCode(500);
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}

