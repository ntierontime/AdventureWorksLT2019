using AdventureWorksLT2019.MauiXApp.ViewModels;
using Framework.MauiX.Helpers;

namespace AdventureWorksLT2019.MauiXApp.Views;

public partial class WebAuthenticatorPage : ContentPage
{
	public WebAuthenticatorPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<WebAuthenticatorViewModel>();
    }
}