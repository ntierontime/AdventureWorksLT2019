using AdventureWorksLT2019.MauiXApp.ViewModels.ProductCategory;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.ProductCategory;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        var viewModel = ServiceHelper.GetService<DashboardVM>();
        BindingContext = viewModel;
        InitializeComponent();
    }
}

