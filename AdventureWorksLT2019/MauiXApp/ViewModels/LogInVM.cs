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

    private readonly AdventureWorksLT2019.MauiXApp.Common.Services.AuthenticationService _authenticationService;
    private readonly AdventureWorksLT2019.MauiXApp.Common.Services.AppLoadingService _appLoadingService;

    public LogInVM(
        AdventureWorksLT2019.MauiXApp.Common.Services.AuthenticationService authenticationService,
        AdventureWorksLT2019.MauiXApp.Common.Services.AppLoadingService appLoadingService
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
            // Application.Current.MainPage = new AdventureWorksLT2019.MauiXApp.Views.AppLoadingPage();
            await _appLoadingService.Step2OnAuthenticated(true, signInData.GotoFirstTimeUserPage());
        }
    }
}

