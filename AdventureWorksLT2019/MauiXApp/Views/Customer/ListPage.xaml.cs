namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();


/* Unmerged change from project 'AdventureWorksLT2019.MauiXApp (net6.0-ios)'
Before:
        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.CustomerListVM>();
After:
        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<CustomerListVM>();
*/
        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<ViewModels.Customer.ListVM>();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(1000);

/* Unmerged change from project 'AdventureWorksLT2019.MauiXApp (net6.0-ios)'
Before:
        (BindingContext as AdventureWorksLT2019.MauiXApp.ViewModels.CustomerListVM).ApplyAdvancedSearchCommand.Execute(null);
After:
        (BindingContext as CustomerListVM).ApplyAdvancedSearchCommand.Execute(null);
*/
        (BindingContext as ViewModels.Customer.ListVM).ApplyAdvancedSearchCommand.Execute(null);
    }
}