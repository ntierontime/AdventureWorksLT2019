namespace AdventureWorksLT2019.MauiXApp.Views;

public partial class RegisterUserPage : ContentPage
{
	public RegisterUserPage()
	{
		InitializeComponent();

        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.RegisterUserVM>();
    }

	private async void GotoLogInButton_Clicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync(nameof(AdventureWorksLT2019.MauiXApp.Views.LogInPage));
    }
}