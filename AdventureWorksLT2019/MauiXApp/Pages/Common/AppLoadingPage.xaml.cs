namespace AdventureWorksLT2019.MauiXApp.Pages.Common;

public partial class AppLoadingPage : ContentPage
{
	public AppLoadingPage()
	{
		InitializeComponent();


/* Unmerged change from project 'AdventureWorksLT2019.MauiXApp (net6.0-ios)'
Before:
        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.AppLoadingVM>();
After:
        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AppLoadingVM>();
*/
        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<ViewModels.Common.AppLoadingVM>();
    }
}