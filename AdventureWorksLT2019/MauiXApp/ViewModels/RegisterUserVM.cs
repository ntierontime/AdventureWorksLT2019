using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.ViewModels;

public class RegisterUserVM : Framework.MauiX.ComponentModels.ObservableValidatorExt
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

    private string m_ConfirmPassword;
    [Required(ErrorMessageResourceType = typeof(AdventureWorksLT2019.Resx.Resources.UIStrings), ErrorMessageResourceName = "ConfirmPasswordRequired")]
    [Compare(nameof(Password), ErrorMessageResourceName = "ConfirmPasswordNotMatchError", ErrorMessageResourceType = typeof(AdventureWorksLT2019.Resx.Resources.UIStrings))]
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

    private readonly AdventureWorksLT2019.MauiX.Services.AuthenticationService _authenticationService;
    private readonly AdventureWorksLT2019.MauiXApp.ViewModels.AppVM _appVM;

    public RegisterUserVM(
        AdventureWorksLT2019.MauiX.Services.AuthenticationService authenticationService,
        AdventureWorksLT2019.MauiXApp.ViewModels.AppVM appVM
        )
    {
        _authenticationService = authenticationService;
        _appVM = appVM;
        ValidateAllProperties();
    }

    public ICommand RegisterUserCommand => new Command(OnRegisterUser);

    private async void OnRegisterUser()
    {
        // RemeberMe always "true"
        var signInData = await _authenticationService.RegisterUserAsync(Email, Password, ConfirmPassword, true);
        if(signInData.IsAuthenticated())
        {
            // Application.Current.MainPage = new AdventureWorksLT2019.MauiXApp.Pages.AppLoadingPage();
            await _appVM.OnAuthenticated(true, true);
        }
    }
}

