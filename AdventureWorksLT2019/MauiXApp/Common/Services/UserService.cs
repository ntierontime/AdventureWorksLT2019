using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.Common.Services
{
    public class UserService
    {
        private readonly AdventureWorksLT2019.MauiXApp.Common.WebApiClients.AuthenticationApiClient _authenticationApiClient;
        private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;

        public UserService(
            AdventureWorksLT2019.MauiXApp.Common.WebApiClients.AuthenticationApiClient authenticationApiClient,
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
                WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Common.Messages.AuthenticatedMessage>(new AdventureWorksLT2019.MauiXApp.Common.Messages.AuthenticatedMessage(false));
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
