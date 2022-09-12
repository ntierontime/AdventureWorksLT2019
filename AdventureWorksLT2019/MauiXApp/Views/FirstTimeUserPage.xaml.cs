namespace AdventureWorksLT2019.MauiXApp.Views;

public partial class FirstTimeUserPage : ContentPage
{
	public FirstTimeUserPage()
	{
		InitializeComponent();
        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.FirstTimeUserVM>();
    }
}