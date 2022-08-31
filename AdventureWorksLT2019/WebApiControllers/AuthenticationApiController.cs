using Framework.Mvc.Models.Account;
using Framework.Mvc.Identity.Data;
using Framework.Mvc.Identity;
using Framework.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static System.Collections.Specialized.BitVector32;

namespace AdventureWorksLT2019.WebApiControllers
{
    [Route("/api/[controller]/[action]")]
    public class AuthenticationApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ConfigurationManager _config;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public AuthenticationApiController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IServiceProvider serviceProvider,
            IEmailSender emailSender,
            ConfigurationManager config,
            ILogger<AuthenticationApiController> logger
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpPost]
        public async Task<AuthenticationResponse> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            return await GetAuthenticationResponse(user, result);
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

            var identitySecretSection = _config.GetSection("IdentitySecret");
            var identitySecret = identitySecretSection.Get<IdentitySecret>();
            // authentication successful, then generate jwt token
            string tokenInString = CustomizedClaimTypes.GetJwtSecurityTokenInString(user.Id.ToLower(), identitySecret.Secret);
            loginResponse.Token = tokenInString;

            // Load LogIn User related data
            if (loginResponse.Succeeded)
            {
                loginResponse.Roles = await _userManager.GetRolesAsync(user);

                #region TODO: Load more data to LoginResponse

                //// TODO: Load more data to LoginResponse
                //using (var scope = _serviceProvider.CreateScope())
                //{
                //    var criteria = new NTierOnTime.CommonBLLEntities.EntityChainedQueryCriteriaIdentifier();
                //    criteria.Identifier.EntityID.NullableValueToCompare = user.EntityID;

                //    var entityResponse = new NTierOnTime.AspNetMvcCoreViewModel.Entity.DashboardVM(); // TODO: how to IoC
                //    entityResponse.CriteriaOfMasterEntity = criteria;
                //    entityResponse.SetServiceProvider(this._serviceProvider);
                //    await entityResponse.LoadData(
                //        isToLoadFK_CourseCategory_Entity_ParentEntityID_List: false
                //        , isToLoadFK_Album_Entity_Owner_List: false
                //        , isToLoadFK_Comment_Entity_PostedByID_List: false
                //        , isToLoadFK_EntityAddress_Entity_EntityID_List: false
                //        , isToLoadFK_EntityAlbum_Entity_EntityID_List: false
                //        , isToLoadFK_EntityCalendarItem_Entity_EntityID_List: false
                //        , isToLoadFK_EntityCommentThread_Entity_EntityID_List: false
                //        , isToLoadFK_EntityContact_Entity_EntityID_List: false
                //        , isToLoadFK_EntityEmail_Entity_EntityID_List: false
                //        , isToLoadFK_EntityScheduleGroup_Entity_EntityID_List: false
                //        , isToLoadFK_EntityVirtualAddress_Entity_EntityID_List: false
                //        , isToLoadFK_Liking_Entity_EntityID_List: false
                //        , isToLoadFK_Liking_Entity_TheOtherSideEntityID_List: false
                //        , isToLoadFK_MemberProgram_Entity_ProgramEntityID_List: false
                //        , isToLoadFK_Membership_Entity_MasterEntityID_List: false
                //        , isToLoadFK_Membership_Entity_SlaveEntityID_List: true
                //        , isToLoadFK_ProgramScheduleCalendarItem_Entity_ProgramEntityID_List: false
                //        , isToLoadFK_BusinessEntity_Entity_EntityID_FormView: false
                //        , isToLoadFK_Class_Entity_EntityID_FormView: false
                //        , isToLoadFK_Course_Entity_EntityID_FormView: false
                //        , isToLoadFK_ActivitySummary_Entity_EntityID_FormView: false
                //        , isToLoadFK_Membership_Entity_MembershipID_FormView: true
                //        , isToLoadFK_Person_Entity_EntityID_FormView: true);

                //    // 1. Entity
                //    if (entityResponse.StatusOfMasterEntity == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || entityResponse.StatusOfMasterEntity == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                //    {
                //        loginResponse.Entity = entityResponse.MasterEntity;
                //    }

                //    // 2. Person
                //    if (entityResponse.StatusOfFK_Person_Entity_EntityID_FormView == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || entityResponse.StatusOfFK_Person_Entity_EntityID_FormView == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                //    {
                //        loginResponse.HasPerson = entityResponse.StatusOfFK_Person_Entity_EntityID_FormView == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || entityResponse.StatusOfFK_Person_Entity_EntityID_FormView == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady;
                //        loginResponse.Person = entityResponse.FK_Person_Entity_EntityID_FormView;
                //    }

                //    // 3. Joined Memberships
                //    if (entityResponse.StatusOfFK_Membership_Entity_SlaveEntityID_List == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || entityResponse.StatusOfFK_Membership_Entity_SlaveEntityID_List == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                //    {
                //        loginResponse.HasJoinedMemberShip = entityResponse.StatusOfFK_Membership_Entity_SlaveEntityID_List == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || entityResponse.StatusOfFK_Membership_Entity_SlaveEntityID_List == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady;
                //        loginResponse.JoinedMemberships = entityResponse.FK_Membership_Entity_SlaveEntityID_List;
                //    }

                //}

                #endregion TODO: Load more data to LoginResponse
            }
            return loginResponse;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<AuthenticationResponse> Logout([FromBody] LoginViewModel model)
        {

            // TODO: set a flag? last log out time
            return new AuthenticationResponse { Succeeded = true };
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [HttpPost]
        public async Task<AuthenticationResponse> Register([FromBody] RegisterViewModel model)
        {
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

                /*
                var service1 = _serviceProvider.GetRequiredService<NTierOnTime.WcfContracts.IEntityService>();

                var response = await NTierOnTime.CoreCommonBLL.Helpers.EntityHelper.CreateNewEntity(service1, model.Email, _logger);

                if (response.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || response.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                {
                    var applicationUser = await _userManager.FindByEmailAsync(model.Email);
                    if (applicationUser != null)
                    {
                        applicationUser.EntityID = response.Message[0].EntityID;
                        await _userManager.UpdateAsync(applicationUser);
                    }
                }
                */
            }

            var loginViewModel = new LoginViewModel { Email = model.Email, Password = model.Password };

            return await Login(loginViewModel);
        }

        private async Task<AuthenticationResponse> GetAuthenticationResponse(
            ApplicationUser user)
        {
            var loginResponse = new AuthenticationResponse
            {
                Succeeded = true
                ,
                IsLockedOut = false
                ,
                IsNotAllowed = false
                ,
                RequiresTwoFactor = false
                //,
                //EntityID = user != null ? user.EntityID : null
            };

            var identitySecretSection = _config.GetSection("IdentitySecret");
            var identitySecret = identitySecretSection.Get<IdentitySecret>();
            // authentication successful, then generate jwt token
            string tokenInString = CustomizedClaimTypes.GetJwtSecurityTokenInString(user.Id.ToLower(), identitySecret.Secret);
            loginResponse.Token = tokenInString;

            // Load LogIn User related data
            if (loginResponse.Succeeded)
            {
                loginResponse.Roles = await _userManager.GetRolesAsync(user);
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

