using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using AdventureWorksLT2019.MauiXApp.Common.Messages;
using Framework.MauiX.Services;
using Framework.MauiX.DataModels;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.Common.Services;

public class AuthenticationService
{
    private readonly AuthenticationApiClient _authenticationApiClient;
    private readonly SecureStorageService _secureStorageService;

    public AuthenticationService(
        AuthenticationApiClient authenticationApiClient,
        SecureStorageService secureStorageService)
    {
        _authenticationApiClient = authenticationApiClient;
        _secureStorageService = secureStorageService;
    }

    public async Task<SignInData> AutoLogInAsync()
    {
        var signInData = await _secureStorageService.GetSignInData();
        if(signInData != null && !string.IsNullOrEmpty(signInData.UserName) && !string.IsNullOrEmpty(signInData.Password))
        {
            return await LogInAsync(signInData.UserName, signInData.Password, true);
        }
        _secureStorageService.ClearSignInData();
        return new SignInData();
    }

    public async Task<SignInData> LogInAsync(string userName, string password, bool rememberMe)
    {
        var response = await _authenticationApiClient.LogInAsync(userName, password);
        if (response.Succeeded)
        {
            var signInData = new SignInData
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

            WeakReferenceMessenger.Default.Send<AuthenticatedMessage>(new AuthenticatedMessage(true));
            // TODO, review on how to keep TOKEN
            Preferences.Default.Set<string>("Token", response.Token);
            return signInData;
        }
        _secureStorageService.ClearSignInData();
        return new SignInData();
    }

    public async Task<SignInData> RegisterUserAsync(string userName, string password, string confirmPassword, bool rememberMe)
    {
        var response = await _authenticationApiClient.RegisterUserAsync(userName, password, confirmPassword);
        if (response.Succeeded)
        {
            var signInData = new SignInData
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
            // TODO, review on how to keep TOKEN
            Preferences.Default.Set<string>("Token", response.Token);
            return signInData;
        }
        _secureStorageService.ClearSignInData();
        return new SignInData();
    }

}

