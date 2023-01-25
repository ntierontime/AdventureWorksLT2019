using AdventureWorksLT2019.MauiXApp.ViewModels.ProductModelProductDescription;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        var viewModel = ServiceHelper.GetService<DashboardVM>();
        BindingContext = viewModel;
        InitializeComponent();
    }
}

