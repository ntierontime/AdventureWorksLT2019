using AdventureWorksLT2019.MauiXApp.ViewModels;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views;

public partial class LogInPage : ContentPage
{
    public LogInPage()
    {
        InitializeComponent();

        BindingContext = ServiceHelper.GetService<LogInVM>();
    }

    private async void OnRegisterANewUserButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegisterUserPage));
    }
}

