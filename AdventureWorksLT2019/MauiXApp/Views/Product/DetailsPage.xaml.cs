using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp.ViewModels.Product;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.Views.Product;

public partial class DetailsPage : ContentPage
{
    public DetailsPage()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachDetailsViewCommands(AppShellService.ShellGotoAbsoluteCommand, AppShellRoutes.ProductListPage);
        InitializeComponent();
    }
}

