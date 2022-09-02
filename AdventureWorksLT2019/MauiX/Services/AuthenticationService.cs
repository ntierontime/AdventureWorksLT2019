using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksLT2019.MauiX.Services
{
    public class AuthenticationService
    {
        private readonly AdventureWorksLT2019.MauiX.WebApiClients.AuthenticationApiClient _authenticationApiClient;
        private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;

        public AuthenticationService(
            AdventureWorksLT2019.MauiX.WebApiClients.AuthenticationApiClient authenticationApiClient,
            Framework.MauiX.Services.SecureStorageService secureStorageService)
        {
            _authenticationApiClient = authenticationApiClient;
            _secureStorageService = secureStorageService;
        }

        public async Task<Framework.MauiX.DataModels.SignInData> AutoLogIn()
        {
            var signInData = await _secureStorageService.GetSignInData();
            if(!string.IsNullOrEmpty(signInData.UserName) && !string.IsNullOrEmpty(signInData.Password))
            {
                return await LogIn(signInData.UserName, signInData.Password, true);
            }
            _secureStorageService.ClearSignInData();
            return default;
        }

        public async Task<Framework.MauiX.DataModels.SignInData> LogIn(string userName, string password, bool rememberMe)
        {
            var response = await _authenticationApiClient.LogInAsync(userName, password);
            if (response.Succeeded && rememberMe)
            {
                var signInData = new Framework.MauiX.DataModels.SignInData
                {
                    UserName = userName,
                    Password = password,
                    LastLoggedInDateTime = DateTime.Now,
                    Token = response.Token!,
                    TokenExpireDateTime = DateTime.Now.AddSeconds(response.ExpiresIn),
                    GoToWelcomeWizard = false, // should be true when register
                    ShortGuid = "", // should have an identifier returned in AuthenticationResponse
                };
                return await _secureStorageService.SetSignInData(signInData);
            }
            _secureStorageService.ClearSignInData();
            return default;
        }
    }
}