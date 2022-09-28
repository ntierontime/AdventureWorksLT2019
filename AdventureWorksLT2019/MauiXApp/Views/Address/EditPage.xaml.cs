using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp.ViewModels.Address;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.Views.Address;

public partial class EditPage : ContentPage
{
    public EditPage()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachEditViewCommands(AppShellService.ShellGotoAbsoluteCommand, AppShellRoutes.AddressListPage);
        viewModel.RequestItem(ViewItemTemplates.Edit);
        InitializeComponent();
    }
}

