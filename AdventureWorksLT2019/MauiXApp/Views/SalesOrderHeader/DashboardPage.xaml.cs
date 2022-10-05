using AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        var viewModel = ServiceHelper.GetService<DashboardVM>();
        BindingContext = viewModel;
        InitializeComponent();
    }
}

