using AdventureWorksLT2019.MauiXApp.ViewModels.Customer;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

public partial class CreatePopup : Popup
{
    public CreatePopup()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachCreateViewCommands(new Command(OnCancelled));
        viewModel.RequestItem(ViewItemTemplates.Create);

        InitializeComponent();
    }

    protected void OnCancelled()
    {
        Close();
    }
}

