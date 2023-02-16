using Framework.Models.Account;
using Framework.Mvc.Identity.Data;
using Framework.Mvc.Identity;
using Framework.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AdventureWorksLT2019.WebApiControllers
{
    [Route("/api/[controller]/[action]")]
    public class AuthenticationApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IdentitySecret _identitySecret;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public AuthenticationApiController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IOptions<IdentitySecret> identitySecret,
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
        public async Task<AuthenticationResponse> Login([FromBody] LoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            return await GetAuthenticationResponse(user, result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<AuthenticationResponse> Logout([FromBody] LoginRequest model)
        {
            //// TODO: set a flag? last log out time
            //return new AuthenticationResponse { Succeeded = true };

            await _signInManager.SignOutAsync();
            return new AuthenticationResponse { Succeeded = true };
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [HttpPost]
        public async Task<AuthenticationResponse> Register([FromBody] RegisterRequest model)
        {
            var LoginRequest = new LoginRequest { Email = model.Email, Password = model.Password };
            return await Login(LoginRequest);

            if (!ModelState.IsValid)
            {
                return new AuthenticationResponse { Succeeded = false };
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new AuthenticationResponse { Succeeded = false };
            }
            else
            {
                // This is a copy from Register method in AccountController.
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                // TODO: load more data
            }

            var loginRequest = new LoginRequest { Email = model.Email, Password = model.Password };

            return await Login(loginRequest);
        }

        private async Task<AuthenticationResponse> GetAuthenticationResponse(
            ApplicationUser user
            , Microsoft.AspNetCore.Identity.SignInResult result)
        {
            var loginResponse = new AuthenticationResponse
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
            string tokenInString = CustomizedClaimTypes.GetJwtSecurityTokenInString(user.Id.ToLower(), _identitySecret.Secret);
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

