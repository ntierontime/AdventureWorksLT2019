using Framework.Models.Account;
using Framework.MauiX;

namespace AdventureWorksLT2019.MauiXApp.Common.WebApiClients;

public partial class AuthenticationApiClient : WebApiClientBase
{
    public AuthenticationApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "AuthenticationApi")
    {
    }

    public async Task<AuthenticationResponse> LogInAsync(string userName, string password)
    {
        const string ActionName = "Login";
        var model = new LoginRequest
        {
            Email = userName,
            Password = password
        };

        string url = GetHttpRequestUrl(ActionName);
        var response = await Post<LoginRequest, AuthenticationResponse>(url, model, false);
        response ??= new AuthenticationResponse { Succeeded = false, IsLockedOut = false, IsNotAllowed = false, RequiresTwoFactor = false, };

        return response;
    }

    public async Task<AuthenticationResponse> LogoutAsync(string userName)
    {
        const string ActionName = "Logout";
        var model = new LoginRequest
        {
            Email = userName,
        };

        string url = GetHttpRequestUrl(ActionName);
        var response = await Post<LoginRequest, AuthenticationResponse>(url, model);
        response ??= new AuthenticationResponse { Succeeded = false, IsLockedOut = false, IsNotAllowed = false, RequiresTwoFactor = false, };

        return response;
    }

    public async Task<AuthenticationResponse> RegisterUserAsync(string email, string password, string confirmPassword)
    {
        const string ActionName = "Register";
        string url = GetHttpRequestUrl(ActionName);

        var model = new RegisterRequest
        {
            Email = email,
            Password = password,
            ConfirmPassword = confirmPassword
        };
        return await Post<RegisterRequest, AuthenticationResponse>(url, model, false);
    }
}

