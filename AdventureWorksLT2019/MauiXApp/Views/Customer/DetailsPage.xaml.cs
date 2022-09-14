using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

public partial class DetailsPage : ContentPage
{
    public DetailsPage()
	{
        var viewModel = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachDetailsViewCommands(AdventureWorksLT2019.MauiXApp.Common.Services.AppShellService.ShellGotoAbsoluteCommand, AdventureWorksLT2019.MauiXApp.Common.Services.AppShellRoutes.CustomerListPage);
        viewModel.RequestItem(Framework.Models.ViewItemTemplates.Details);
        InitializeComponent();
    }
}