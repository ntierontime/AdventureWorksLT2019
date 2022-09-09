using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.Services.Common
{
    public class UserService
    {

/* Unmerged change from project 'AdventureWorksLT2019.MauiXApp (net6.0-windows10.0.19041.0)'
Before:
        private readonly AdventureWorksLT2019.MauiXApp.WebApiClients.AuthenticationApiClient _authenticationApiClient;
After:
        private readonly AuthenticationApiClient _authenticationApiClient;
*/
        private readonly WebApiClients.Common.AuthenticationApiClient _authenticationApiClient;
        private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;

        public UserService(

/* Unmerged change from project 'AdventureWorksLT2019.MauiXApp (net6.0-windows10.0.19041.0)'
Before:
            AdventureWorksLT2019.MauiXApp.WebApiClients.AuthenticationApiClient authenticationApiClient,
After:
            AuthenticationApiClient authenticationApiClient,
*/
            WebApiClients.Common.AuthenticationApiClient authenticationApiClient,
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
                WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.Common.AuthenticatedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.Common.AuthenticatedMessage(false));
            // TODO, review on how to keep TOKEN
            Preferences.Default.Remove("Token");
            return response.Succeeded;
        }

        public async Task SetUserProfileCompletedAsync()
        {
            var signInData = await _secureStorageService.GetSignInData();
            signInData.UserProfileCompleted = true;
            await _secureStorageService.SetSignInData(signInData);
        }
    }
}
