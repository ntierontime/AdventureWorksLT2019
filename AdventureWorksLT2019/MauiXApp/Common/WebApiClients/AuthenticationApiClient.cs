namespace AdventureWorksLT2019.MauiXApp.Common.WebApiClients;

public partial class AuthenticationApiClient : Framework.MauiX.WebApiClientBase
{
    public AuthenticationApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "AuthenticationApi")
    {
    }

    public async Task<Framework.Models.Account.AuthenticationResponse> LogInAsync(string userName, string password)
    {
        const string ActionName = "Login";
        var model = new Framework.Models.Account.LoginRequest
        {
            Email = userName,
            Password = password
        };

        string url = GetHttpRequestUrl(ActionName);
        var response = await Post<Framework.Models.Account.LoginRequest, Framework.Models.Account.AuthenticationResponse>(url, model, false);
        response ??= new Framework.Models.Account.AuthenticationResponse { Succeeded = false, IsLockedOut = false, IsNotAllowed = false, RequiresTwoFactor = false, };

        return response;
    }

    public async Task<Framework.Models.Account.AuthenticationResponse> LogoutAsync(string userName)
    {
        const string ActionName = "Logout";
        var model = new Framework.Models.Account.LoginRequest
        {
            Email = userName,
        };

        string url = GetHttpRequestUrl(ActionName);
        var response = await Post<Framework.Models.Account.LoginRequest, Framework.Models.Account.AuthenticationResponse>(url, model);
        response ??= new Framework.Models.Account.AuthenticationResponse { Succeeded = false, IsLockedOut = false, IsNotAllowed = false, RequiresTwoFactor = false, };

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
        return await Post<Framework.Models.Account.RegisterRequest, Framework.Models.Account.AuthenticationResponse>(url, model, false);
    }
}
