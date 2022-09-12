namespace AdventureWorksLT2019.MauiXApp.Pages;

public partial class CustomerListPage : ContentPage
{
	public CustomerListPage()
	{
		InitializeComponent();

        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.CustomerListVM>();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(1000);
        (BindingContext as AdventureWorksLT2019.MauiXApp.ViewModels.CustomerListVM).ApplyAdvancedSearchCommand.Execute(null);
    }
}