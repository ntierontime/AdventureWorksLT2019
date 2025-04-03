using Framework.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Mvc.Identity
{
    public class TokenService
    {
        public const string JwtToken = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/JwtToken";

        private readonly ClaimService _claimService;

        public TokenService(ClaimService claimService)
        {
            this._claimService = claimService;
        }

        public string GetJwtSecurityTokenInString(string secret, ClaimsModel claimsModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var claims = _claimService.GetClaims(claimsModel);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.Where(t=>t.Type != ClaimTypes.Role)),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            tokenDescriptor.Claims ??= new Dictionary<string, object>();
            tokenDescriptor.Claims.Add(ClaimTypes.Role, string.Join(",", claimsModel.Roles));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenInString = tokenHandler.WriteToken(token);
            return tokenInString;
        }

        public string GetTempJwtSecurityTokenInString(string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = DateTime.UtcNow.AddDays(200),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            tokenDescriptor.Claims ??= new Dictionary<string, object>();
            tokenDescriptor.Claims.Add(ClaimTypes.Role, "Visitor");
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenInString = tokenHandler.WriteToken(token);
            return tokenInString;
        }
    }
}

