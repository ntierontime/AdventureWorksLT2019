using AdventureWorksLT2019.MauiXApp.ViewModels.ProductModelProductDescription;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription;

public partial class DeletePopup : Popup
{
    public DeletePopup()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachDeleteViewCommands(new Command(OnCancelled));

        InitializeComponent();
        // WinUI Size is not correct.
        Size = PopupHelper.GetPopupSize();
    }

    private void OnCancelled()
    {
        Close();
    }
}

