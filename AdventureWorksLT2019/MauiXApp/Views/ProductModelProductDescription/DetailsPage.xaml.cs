using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp.ViewModels.ProductModelProductDescription;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription;

public partial class DetailsPage : ContentPage
{
    public DetailsPage()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachDetailsViewCommands(AppShellService.ShellGotoAbsoluteCommand, AppShellRoutes.ProductModelProductDescriptionListPage);
        InitializeComponent();
    }
}

