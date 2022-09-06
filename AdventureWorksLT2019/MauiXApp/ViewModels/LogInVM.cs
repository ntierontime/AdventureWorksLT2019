using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.ViewModels;

public class LogInVM: Framework.MauiX.ComponentModels.ObservableValidatorExt
{
    string m_Email;
    [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(AdventureWorksLT2019.Resx.Resources.UIStrings))]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceName = "EmailFormatError", ErrorMessageResourceType = typeof(AdventureWorksLT2019.Resx.Resources.UIStrings))]
    public string Email
    {
        get => m_Email;
        set
        {
            SetProperty(ref m_Email, value);
            ValidateProperty(value, nameof(Email));
        }
    }

    string m_Password;
    [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(AdventureWorksLT2019.Resx.Resources.UIStrings))]
    //Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessageResourceName = "PasswordPatternError", ErrorMessageResourceType = typeof(AdventureWorksLT2019.Resx.Resources.UIStrings))]
    public string Password
    {
        get => m_Password;
        set
        {
            SetProperty(ref m_Password, value);
            ValidateProperty(value, nameof(Password));
        }
    }

    string m_ErrorMessage;
    public string ErrorMessage
    {
        get => m_ErrorMessage;
        set => SetProperty(ref m_ErrorMessage, value);
    }

    //private bool m_RememberMe;
    //public bool RememberMe
    //{
    //    get => m_RememberMe;
    //    set => SetProperty(ref m_RememberMe, value);
    //}

    //private bool m_AutoSignIn;
    //public bool AutoSignIn
    //{
    //    get => m_AutoSignIn;
    //    set => SetProperty(ref m_AutoSignIn, value);
    //}

    private readonly AdventureWorksLT2019.MauiX.Services.AuthenticationService _authenticationService;
    private readonly AdventureWorksLT2019.MauiXApp.ViewModels.AppVM _appVM;

    public LogInVM(
        AdventureWorksLT2019.MauiX.Services.AuthenticationService authenticationService,
        AdventureWorksLT2019.MauiXApp.ViewModels.AppVM appVM
        )
    {
        _authenticationService = authenticationService;
        _appVM = appVM;
        ValidateAllProperties();
    }

    /// <summary>
    /// 1. Login Asp.Net Identity
    /// </summary>
    public ICommand LogInCommand => new Command(OnLogInAsync);

    private async void OnLogInAsync()
    {
        // RemeberMe always "true"
        var signInData = await _authenticationService.LogInAsync(Email, Password, true);
        if(signInData.IsAuthenticated())
        {
            // Application.Current.MainPage = new AdventureWorksLT2019.MauiXApp.Pages.AppLoadingPage();
            await _appVM.OnAuthenticated(true, false);
        }
    }

    //public ICommand GoogleLogInCommand => new Command(OnGoogleLogin);

    //private async void OnGoogleLogin()
    //{
    //    string clientId = null;
    //    string redirectUri = null;

    //    switch (Device.RuntimePlatform)
    //    {
    //        case Device.iOS:
    //            clientId = Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.iOSClientId;
    //            redirectUri = Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.iOSRedirectUrl;
    //            break;

    //        case Device.Android:
    //            clientId = Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AndroidClientId;
    //            redirectUri = Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AndroidRedirectUrl;
    //            break;
    //    }

    //    var account = await Framework.Xamariner.Helpers.SecureStorageAccountStoreHelper.FindAccountsForServiceAsync(Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AppName);

    //    var authenticator = new OAuth2Authenticator(
    //        clientId,
    //        null,
    //        Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.Scope,
    //        new Uri(Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AuthorizeUrl),
    //        new Uri(redirectUri),
    //        new Uri(Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AccessTokenUrl),
    //        null,
    //        true);

    //    authenticator.Completed += OnAuthCompleted_Google;
    //    authenticator.Error += OnAuthError;

    //    Elmah.XamarinForms.Authentication.AuthenticationState.Authenticator = authenticator;

    //    var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
    //    presenter.Login(authenticator);
    //}

    //#region 2. Google OAuth2 Login OnAuthCompleted/OnAuthError

    //async void OnAuthCompleted_Google(object sender, AuthenticatorCompletedEventArgs e)
    //{
    //    var authenticator = sender as OAuth2Authenticator;

    //    if (authenticator != null)
    //    {
    //        authenticator.Completed -= OnAuthCompleted_Google;
    //        authenticator.Error -= OnAuthError;
    //    }

    //    Elmah.XamarinForms.Authentication.GoogleUser googleUser = null;
    //    if (e.IsAuthenticated)
    //    {
    //        var account = await Framework.Xamariner.Helpers.SecureStorageAccountStoreHelper.FindAccountsForServiceAsync(Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AppName);

    //        // If the user is authenticated, request their basic user data from Google
    //        // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
    //        var request = new OAuth2Request("GET", new Uri(Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.UserInfoUrl), null, e.Account);
    //        var response = await request.GetResponseAsync();
    //        if (response != null)
    //        {
    //            // Deserialize the data and store it in the account store
    //            // The users email address will be used to identify data in SimpleDB
    //            string userJson = await response.GetResponseTextAsync();
    //            googleUser = JsonConvert.DeserializeObject<Elmah.XamarinForms.Authentication.GoogleUser>(userJson);
    //            var userTokenIdModel = new Framework.WebApi.UserTokenIdModel
    //            {
    //                AuthenticationProvider = Framework.Models.AuthenticationProvider.Google,
    //                Name = googleUser.Name,
    //                FamilyName = googleUser.FamilyName,
    //                Email = googleUser.Email,
    //                Gender = googleUser.Gender,
    //                GivenName = googleUser.GivenName,
    //                Id = googleUser.Id,
    //                JwtToken = e.Account.Properties["id_token"],
    //                Picture = googleUser.Picture,
    //                VerifiedEmail = googleUser.VerifiedEmail
    //            };

    //            var authenticationApiClient = Elmah.MVVMLightViewModels.WebApiClientFactory.CreateAuthenticationApiClient();
    //            var apiResponse = await authenticationApiClient.SignInWithOAuth2(userTokenIdModel, Framework.Models.AuthenticationProvider.Google);
    //        }

    //        if (account != null)
    //        {
    //            await Framework.Xamariner.Helpers.SecureStorageAccountStoreHelper.DeleteAsync(account.FirstOrDefault(), Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AppName);
    //        }

    //        await Framework.Xamariner.Helpers.SecureStorageAccountStoreHelper.SaveAsync(e.Account, Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AppName);
    //    }
    //}

    //#endregion 2. Google OAuth2 Login OnAuthCompleted/OnAuthError

    //void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
    //{
    //    var authenticator = sender as OAuth2Authenticator;
    //    if (authenticator != null)
    //    {
    //        authenticator.Completed -= OnAuthCompleted_Google;
    //        authenticator.Error -= OnAuthError;
    //    }

    //    Debug.WriteLine("Authentication error: " + e.Message);
    //}
}

