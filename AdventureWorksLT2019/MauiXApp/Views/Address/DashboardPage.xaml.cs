using AdventureWorksLT2019.MauiXApp.ViewModels.Address;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.Address;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        var viewModel = ServiceHelper.GetService<DashboardVM>();
        BindingContext = viewModel;
        InitializeComponent();
    }
}

