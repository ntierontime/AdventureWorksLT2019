namespace AdventureWorksLT2019.MauiXApp.Views;

public partial class AppLoadingPage : ContentPage
{
	public AppLoadingPage()
	{
		InitializeComponent();
        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.AppLoadingVM>();
    }
}