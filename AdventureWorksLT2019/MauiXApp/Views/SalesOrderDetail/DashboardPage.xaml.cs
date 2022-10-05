using AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderDetail;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        var viewModel = ServiceHelper.GetService<DashboardVM>();
        BindingContext = viewModel;
        InitializeComponent();
    }
}

