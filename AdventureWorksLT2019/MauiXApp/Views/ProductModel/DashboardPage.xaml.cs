using AdventureWorksLT2019.MauiXApp.ViewModels.ProductModel;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.ProductModel;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        var viewModel = ServiceHelper.GetService<DashboardVM>();
        BindingContext = viewModel;
        InitializeComponent();
    }
}

