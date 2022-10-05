using AdventureWorksLT2019.MauiXApp.ViewModels.BuildVersion;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.BuildVersion;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        var viewModel = ServiceHelper.GetService<DashboardVM>();
        BindingContext = viewModel;
        InitializeComponent();
    }
}

