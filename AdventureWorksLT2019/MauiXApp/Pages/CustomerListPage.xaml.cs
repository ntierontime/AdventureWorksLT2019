namespace AdventureWorksLT2019.MauiXApp.Pages;

public partial class CustomerListPage : ContentPage
{
	public CustomerListPage()
	{
		InitializeComponent();

        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.CustomerListVM>();
    }
}