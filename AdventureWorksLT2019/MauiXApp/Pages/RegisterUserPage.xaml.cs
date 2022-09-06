namespace AdventureWorksLT2019.MauiXApp.Pages;

public partial class RegisterUserPage : ContentPage
{
	public RegisterUserPage()
	{
		InitializeComponent();

        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.FirstTimeUserVM>();
    }
}