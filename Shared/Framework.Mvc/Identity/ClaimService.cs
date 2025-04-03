using Framework.Models;
using Framework.Mvc.Identity.Data;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Mvc.Identity
{
    public class ClaimService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ClaimService(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        public Claim[] GetClaims(ClaimsModel claimsModel)
        {
            return (new Claim[]
                {
                    new Claim(ClaimTypes.Name, claimsModel.AspNetUserName),
                    new Claim(ClaimTypes.NameIdentifier, claimsModel.AspNetUserID),
                    new Claim(ClaimTypes.Email, claimsModel.Email),
                    new Claim(ClaimTypes.SerialNumber, claimsModel.PersonID.ToString()),
                }
                .Union(
                    from t in claimsModel.Roles
                    select new Claim(ClaimTypes.Role, t)
                )).ToArray();
        }
        public async Task SaveToAspNetUserClaims(ApplicationUser user, ClaimsModel claimsModel)
        {
            var claims = new Claim[]
                {
                    //new Claim(ClaimTypes.Name, claimsModel.AspNetUserName),
                    //new Claim(ClaimTypes.NameIdentifier, claimsModel.AspNetUserID),
                    //new Claim(ClaimTypes.Email, claimsModel.Email),
                    new Claim(ClaimTypes.SerialNumber, claimsModel.PersonID.ToString()),
                };
            //.Union(
            //    from t in claimsModel.Roles
            //    select new Claim(ClaimTypes.Role, t)
            //);
            await _userManager.AddClaimsAsync(user, claims);
        }

        public async Task<ClaimsModel> GetFromAspNetUserClaims(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var claimsModel = new ClaimsModel
            {
                AspNetUserID = user.Id,
                AspNetUserName = user.UserName,
                Email= user.Email,
                Roles = (await _userManager.GetRolesAsync(user)).ToArray(),
                PersonID = long.Parse(claims.First(t=>t.Type == ClaimTypes.SerialNumber).Value)
            };
            return claimsModel;
        }

        public ClaimsModel? GetFromClaimPrinciple(ClaimsPrincipal user)
        {
            if (user == null)
                return null;
            var claims = user.Claims;
            var aspNetUserNameClaim = claims.FirstOrDefault(t => t.Type == ClaimTypes.Name);
            var aspNetUserName = aspNetUserNameClaim?.Value;
            var roleClaim = claims.FirstOrDefault(t => t.Type == ClaimTypes.Role);
            var roles = roleClaim == null ? Enumerable.Empty<string>() : roleClaim.Value.Split(',');
            // 1. Visitor
            // 2. Or no Role in Claim, e.g. 1st time external login.
            if(roles.Count() == 0 || roles.Contains("Visitor"))
            {
                return new ClaimsModel
                {
                    AspNetUserID = string.Empty,
                    Roles = roles.ToArray(),
                    AspNetUserName = aspNetUserName,
                    Email = aspNetUserName,
                    PersonID = -1,
                };
            }

            return new ClaimsModel
            {
                AspNetUserID = claims.First(t => t.Type == ClaimTypes.NameIdentifier).Value,
                AspNetUserName = claims.First(t => t.Type == ClaimTypes.Name).Value,
                Email = claims.First(t => t.Type == ClaimTypes.Email).Value,
                Roles = roles.ToArray(),
                PersonID = long.Parse(claims.First(t => t.Type == ClaimTypes.SerialNumber).Value)
            };
        }
    }
}
