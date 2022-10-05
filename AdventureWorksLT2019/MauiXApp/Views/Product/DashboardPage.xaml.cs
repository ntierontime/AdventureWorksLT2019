namespace AdventureWorksLT2019.MauiXApp.Views.Product;

public partial class DashboardPage : ContentPage
{
	public DashboardPage()
	{
        var viewModel = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.Product.DashboardVM>();
        BindingContext = viewModel;
        InitializeComponent();
	}
}