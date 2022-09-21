using AdventureWorksLT2019.MauiXApp.Common.Services;
using Framework.MauiX.ComponentModels;
using AdventureWorksLT2019.MauiXApp.Views;
using AdventureWorksLT2019.Resx.Resources;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.ViewModels;

public class LogInVM: ObservableValidatorExt
{
    string m_Email;
    [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(UIStrings))]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceName = "EmailFormatError", ErrorMessageResourceType = typeof(UIStrings))]
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
    [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(UIStrings))]
    //Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessageResourceName = "PasswordPatternError", ErrorMessageResourceType = typeof(UIStrings))]
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

    private readonly AuthenticationService _authenticationService;
    private readonly AppLoadingService _appLoadingService;

    public LogInVM(
        AuthenticationService authenticationService,
        AppLoadingService appLoadingService
        )
    {
        _authenticationService = authenticationService;
        _appLoadingService = appLoadingService;
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
            // Application.Current.MainPage = new AppLoadingPage();
            await _appLoadingService.Step2OnAuthenticated(true, signInData.GotoFirstTimeUserPage());
        }
    }
}

