using AdventureWorksLT2019.MauiXApp.ViewModels.ProductDescription;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.ProductDescription;

public partial class ListBulkActionsPopup : CommunityToolkit.Maui.Views.Popup
{
    public ListBulkActionsPopup()
    {
        var viewModel = ServiceHelper.GetService<ListVM>();
        BindingContext = viewModel;
        viewModel.AttachListBulkActionsPopupCommands(new Command(OnCancelled));

        InitializeComponent();
        // WinUI Size is not correct.
        Size = PopupHelper.GetPopupSize();
    }

    private void OnCancelled()
    {
        Close();
    }
}

