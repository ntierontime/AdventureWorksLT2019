using AdventureWorksLT2019.MauiXApp.ViewModels.CustomerAddress;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.CustomerAddress;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        var viewModel = ServiceHelper.GetService<DashboardVM>();
        BindingContext = viewModel;
        InitializeComponent();
    }
}

