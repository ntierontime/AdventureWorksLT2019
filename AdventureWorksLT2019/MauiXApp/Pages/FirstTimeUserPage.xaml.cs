namespace AdventureWorksLT2019.MauiXApp.Pages;

public partial class FirstTimeUserPage : ContentPage
{
	public FirstTimeUserPage()
	{
		InitializeComponent();

        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.FirstTimeUserVM>();
    }
}