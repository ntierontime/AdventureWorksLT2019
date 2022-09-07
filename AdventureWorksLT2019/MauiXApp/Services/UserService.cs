using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksLT2019.MauiXApp.Services
{
    public class UserService
    {
        private readonly AdventureWorksLT2019.MauiXApp.WebApiClients.AuthenticationApiClient _authenticationApiClient;
        private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;

        public UserService(
            AdventureWorksLT2019.MauiXApp.WebApiClients.AuthenticationApiClient authenticationApiClient,
            Framework.MauiX.Services.SecureStorageService secureStorageService
            )
        {
            _authenticationApiClient = authenticationApiClient;
            _secureStorageService = secureStorageService;
        }

        public async Task<bool> LogOutAsync()
        {
            var signInData = await _secureStorageService.GetSignInData();
            var response = await _authenticationApiClient.LogoutAsync(signInData.UserName);
            _secureStorageService.ClearSignInData();
            if (response.Succeeded)
                WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.AuthenticatedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.AuthenticatedMessage(false));
            return response.Succeeded;
        }
    }
}
