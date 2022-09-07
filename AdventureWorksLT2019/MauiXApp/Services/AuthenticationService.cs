using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksLT2019.MauiXApp.Services
{
    public class AuthenticationService
    {
        private readonly AdventureWorksLT2019.MauiXApp.WebApiClients.AuthenticationApiClient _authenticationApiClient;
        private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;

        public AuthenticationService(
            AdventureWorksLT2019.MauiXApp.WebApiClients.AuthenticationApiClient authenticationApiClient,
            Framework.MauiX.Services.SecureStorageService secureStorageService)
        {
            _authenticationApiClient = authenticationApiClient;
            _secureStorageService = secureStorageService;
        }

        public async Task<Framework.MauiX.DataModels.SignInData> AutoLogInAsync()
        {
            var signInData = await _secureStorageService.GetSignInData();
            if(signInData != null && !string.IsNullOrEmpty(signInData.UserName) && !string.IsNullOrEmpty(signInData.Password))
            {
                return await LogInAsync(signInData.UserName, signInData.Password, true);
            }
            _secureStorageService.ClearSignInData();
            return new Framework.MauiX.DataModels.SignInData();
        }

        public async Task<Framework.MauiX.DataModels.SignInData> LogInAsync(string userName, string password, bool rememberMe)
        {
            var response = await _authenticationApiClient.LogInAsync(userName, password);
            if (response.Succeeded)
            {
                var signInData = new Framework.MauiX.DataModels.SignInData
                {
                    UserName = userName,
                    Password = password,
                    LastLoggedInDateTime = DateTime.Now,
                    Token = response.Token!,
                    TokenExpireDateTime = DateTime.Now.AddSeconds(response.ExpiresIn),
                    UserProfileCompleted = response.UserProfileCompleted,
                    ShortGuid = "", // should have an identifier returned in AuthenticationResponse
                };
                if (rememberMe)
                {
                    return await _secureStorageService.SetSignInData(signInData);
                }
                
                WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.AuthenticatedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.AuthenticatedMessage(true));

                return signInData;
            }
            _secureStorageService.ClearSignInData();
            return new Framework.MauiX.DataModels.SignInData();
        }

        public async Task<Framework.MauiX.DataModels.SignInData> RegisterUserAsync(string userName, string password, string confirmPassword, bool rememberMe)
        {
            var response = await _authenticationApiClient.RegisterUserAsync(userName, password, confirmPassword);
            if (response.Succeeded)
            {
                var signInData = new Framework.MauiX.DataModels.SignInData
                {
                    UserName = userName,
                    Password = password,
                    LastLoggedInDateTime = DateTime.Now,
                    Token = response.Token!,
                    TokenExpireDateTime = DateTime.Now.AddSeconds(response.ExpiresIn),
                    UserProfileCompleted = response.UserProfileCompleted,
                    ShortGuid = "", // should have an identifier returned in AuthenticationResponse
                };
                if (rememberMe)
                {
                    return await _secureStorageService.SetSignInData(signInData);
                }
                return signInData;
            }
            _secureStorageService.ClearSignInData();
            return new Framework.MauiX.DataModels.SignInData();
        }

    }
}