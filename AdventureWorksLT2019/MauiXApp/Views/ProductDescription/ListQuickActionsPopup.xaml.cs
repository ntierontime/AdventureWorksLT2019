using AdventureWorksLT2019.MauiXApp.ViewModels.ProductDescription;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.ProductDescription;

public partial class ListQuickActionsPopup : CommunityToolkit.Maui.Views.Popup
{
    public ListQuickActionsPopup()
    {
        var viewModel = ServiceHelper.GetService<ListVM>();
        BindingContext = viewModel;
        viewModel.AttachListQuickActionsPopupCommands(new Command(OnCancelled));

        InitializeComponent();
        // WinUI Size is not correct.
        Size = PopupHelper.GetPopupSize();
    }

    private void OnCancelled()
    {
        Close();
    }
}

