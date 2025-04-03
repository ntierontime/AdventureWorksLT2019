using Framework.Mvc.Identity.Data;
using Framework.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Framework.Mvc.Identity
{
    public class ApplicationClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        private readonly IOptions<IdentitySecret> _identitySecret;
        private readonly TokenService _tokenService;

        public ApplicationClaimsPrincipalFactory(
            TokenService tokenService
            , UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor
            , IOptions<IdentitySecret> identitySecret)
        : base(userManager, roleManager, optionsAccessor)
        {
            _identitySecret = identitySecret;
            _tokenService = tokenService;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            var claimsIdentity = (ClaimsIdentity)principal!.Identity!;

            if (claimsIdentity.IsAuthenticated)
            {
                var claimsModel = new ClaimsModel
                {
                    AspNetUserID = user.Id,
                    AspNetUserName = user.UserName,
                    Email = user.Email,
                    Roles = (await base.UserManager.GetRolesAsync(user)).ToArray(),
                    PersonID = -1 // we have to do something with PersonID in MvcCore, take it from Person Table
                };
                // 1. JwtToken
                string tokenInString = _tokenService.GetJwtSecurityTokenInString(_identitySecret.Value.Secret, claimsModel);
                claimsIdentity.AddClaims(new[] { new Claim(TokenService.JwtToken, tokenInString) });
            }

            return principal;
        }
    }
}

