using Framework.Mvc.Identity.Data;
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

        public ApplicationClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor
            , IOptions<IdentitySecret> identitySecret)
        : base(userManager, roleManager, optionsAccessor)
        {
            _identitySecret = identitySecret;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            var claimsIdentity = (ClaimsIdentity)principal!.Identity!;

            if (claimsIdentity.IsAuthenticated)
            {
                // 1. JwtToken
                string tokenInString = CustomizedClaimTypes.GetJwtSecurityTokenInString(user.Id.ToLower(), _identitySecret.Value.Secret);
                claimsIdentity.AddClaims(new[] { new Claim(CustomizedClaimTypes.JwtToken, tokenInString) });

                // TODO: load more Claims when logged in
                //claimsIdentity.AddClaims(new[] { new Claim(CustomizedClaimTypes.YourEntityId, user.EntityID.ToString()) });

                //if (user.EntityID.HasValue)
                //{
                //    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                //        new Claim(ClaimTypes.GivenName, user.UserName)
                //    });
                //}
            }

            return principal;
        }
    }
}

