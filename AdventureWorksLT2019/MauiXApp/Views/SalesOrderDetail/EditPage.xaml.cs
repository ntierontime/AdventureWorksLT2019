using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderDetail;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail;

public partial class EditPage : ContentPage
{
    public EditPage()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachEditViewCommands(AppShellService.ShellGotoAbsoluteCommand, AppShellRoutes.SalesOrderDetailListPage);
        viewModel.RequestItem(ViewItemTemplates.Edit);
        InitializeComponent();
    }
}

