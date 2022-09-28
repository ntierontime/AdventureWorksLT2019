using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader;

public partial class CreatePage : ContentPage
{
    public CreatePage()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachCreateViewCommands(AppShellService.ShellGotoAbsoluteCommand, AppShellRoutes.SalesOrderHeaderListPage);
        viewModel.RequestItem(ViewItemTemplates.Create);
        InitializeComponent();
    }
}

