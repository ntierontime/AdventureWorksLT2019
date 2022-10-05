using AdventureWorksLT2019.MauiXApp.ViewModels.ErrorLog;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.ErrorLog;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        var viewModel = ServiceHelper.GetService<DashboardVM>();
        BindingContext = viewModel;
        InitializeComponent();
    }
}

