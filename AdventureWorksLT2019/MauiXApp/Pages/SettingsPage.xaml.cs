namespace AdventureWorksLT2019.MauiXApp.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.SettingsVM>();
    }
}