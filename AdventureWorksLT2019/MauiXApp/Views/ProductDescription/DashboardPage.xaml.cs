using AdventureWorksLT2019.MauiXApp.ViewModels.ProductDescription;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.ProductDescription;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        var viewModel = ServiceHelper.GetService<DashboardVM>();
        BindingContext = viewModel;
        InitializeComponent();
    }
}

