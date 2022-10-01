using AdventureWorksLT2019.MauiXApp.ViewModels.Customer;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

public partial class EditPopup : Popup
{
    public EditPopup()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachEditViewCommands(new Command(OnCancelled));

        InitializeComponent();
        // WinUI Size is not correct.
        Size = PopupHelper.GetPopupSize();
    }

    private void OnCancelled()
    {
        Close();
    }
}

