namespace AdventureWorksLT2019.MauiXApp.Pages.Common;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();


/* Unmerged change from project 'AdventureWorksLT2019.MauiXApp (net6.0-ios)'
Before:
        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.SettingsVM>();
After:
        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<SettingsVM>();
*/
        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<ViewModels.Common.SettingsVM>();
    }
}