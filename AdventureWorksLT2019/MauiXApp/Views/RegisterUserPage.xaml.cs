using AdventureWorksLT2019.MauiXApp.ViewModels;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views;

public partial class RegisterUserPage : ContentPage
{
    public RegisterUserPage()
    {
        InitializeComponent();

        BindingContext = ServiceHelper.GetService<RegisterUserVM>();
    }

    private async void GotoLogInButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LogInPage));
    }
}

