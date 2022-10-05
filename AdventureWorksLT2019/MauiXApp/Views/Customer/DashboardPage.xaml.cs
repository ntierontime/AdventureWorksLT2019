using AdventureWorksLT2019.MauiXApp.ViewModels.Customer;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        var viewModel = ServiceHelper.GetService<DashboardVM>();
        BindingContext = viewModel;
        InitializeComponent();
    }
}

