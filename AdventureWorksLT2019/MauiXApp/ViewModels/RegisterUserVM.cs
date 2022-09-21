using AdventureWorksLT2019.MauiXApp.Common.Services;
using Framework.MauiX.ComponentModels;
using AdventureWorksLT2019.Resx.Resources;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.ViewModels;

public class RegisterUserVM : ObservableValidatorExt
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

    private string m_ConfirmPassword;
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "ConfirmPasswordRequired")]
    [Compare(nameof(Password), ErrorMessageResourceName = "ConfirmPasswordNotMatchError", ErrorMessageResourceType = typeof(UIStrings))]
    public string ConfirmPassword
    {
        get => m_ConfirmPassword;
        set
        {
            SetProperty(ref m_ConfirmPassword, value);
            ValidateProperty(value, nameof(ConfirmPassword));
        }
    }

    string m_ErrorMessage;
    public string ErrorMessage
    {
        get => m_ErrorMessage;
        set => SetProperty(ref m_ErrorMessage, value);
    }

    private readonly AuthenticationService _authenticationService;
    private readonly AppLoadingService _appLoadingService;

    public RegisterUserVM(
        AuthenticationService authenticationService,
        AppLoadingService appLoadingService)
    {
        _authenticationService = authenticationService;
        _appLoadingService = appLoadingService;
        ValidateAllProperties();
    }

    public ICommand RegisterUserCommand => new Command(OnRegisterUser);

    private async void OnRegisterUser()
    {
        // RemeberMe always "true"
        var signInData = await _authenticationService.RegisterUserAsync(Email, Password, ConfirmPassword, true);
        if(signInData.IsAuthenticated())
        {
            await _appLoadingService.Step2OnAuthenticated(true, signInData.GotoFirstTimeUserPage());
        }
    }
}

