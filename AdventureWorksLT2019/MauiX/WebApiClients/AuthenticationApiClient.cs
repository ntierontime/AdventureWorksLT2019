using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using AdventureWorksLT2019.MauiX.WebApiClients;

namespace AdventureWorksLT2019.MauiX.WebApiClients
{
    public partial class AuthenticationApiClient : Framework.MauiX.WebApiClientBase
    {
        public override string ControllerName => "AuthenticationApi";

        public AuthenticationApiClient(AdventureWorksLT2019.MauiX.WebApiClients.AuthenticationWebApiConfig webApiConfig) 
            : base(webApiConfig.WebApiRootUrl, webApiConfig.UseToken, webApiConfig.Token)
        {
        }

        public const string ActionName_Login = "Login";

        public async Task<Framework.Models.Account.AuthenticationResponse> LogInAsync(string userName, string password)
        {
            var model = new Framework.Models.Account.LoginRequest
            {
                Email = userName,
                Password = password
            };

            string url = GetHttpRequestUrl(ActionName_Login);
            var response = await Post<Framework.Models.Account.LoginRequest, Framework.Models.Account.AuthenticationResponse>(url, model);
            if (response == null)
            {
                response = new Framework.Models.Account.AuthenticationResponse { Succeeded = false, IsLockedOut = false, IsNotAllowed = false, RequiresTwoFactor = false, };
            }

            return response;
        }

        public const string ActionName_Logout = "Logout";

        public async Task<Framework.Models.Account.AuthenticationResponse> LogoutAsync(string userName)
        {
            var model = new Framework.Models.Account.LoginRequest
            {
                Email = userName,
            };

            string url = GetHttpRequestUrl(ActionName_Logout);
            var response = await Post<Framework.Models.Account.LoginRequest, Framework.Models.Account.AuthenticationResponse>(url, model);
            if (response == null)
            {
                response = new Framework.Models.Account.AuthenticationResponse { Succeeded = false, IsLockedOut = false, IsNotAllowed = false, RequiresTwoFactor = false, };
            }
            return response;
        }

        public async Task<Framework.Models.Account.AuthenticationResponse> RegisterUserAsync(string email, string password, string confirmPassword)
        {
            const string ActionName = "Register";
            string url = GetHttpRequestUrl(ActionName);

            var model = new Framework.Models.Account.RegisterRequest
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };
            return await Post<Framework.Models.Account.RegisterRequest, Framework.Models.Account.AuthenticationResponse>(url, model);
        }
    }
}
