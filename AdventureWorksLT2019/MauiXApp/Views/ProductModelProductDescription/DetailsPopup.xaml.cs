using AdventureWorksLT2019.MauiXApp.ViewModels.ProductModelProductDescription;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription;

public partial class DetailsPopup : Popup
{
    public DetailsPopup()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachDetailsViewCommands(new Command(OnCancelled));

        InitializeComponent();
        // WinUI Size is not correct.
        Size = PopupHelper.GetPopupSize();
    }

    private void OnCancelled()
    {
        Close();
    }
}

