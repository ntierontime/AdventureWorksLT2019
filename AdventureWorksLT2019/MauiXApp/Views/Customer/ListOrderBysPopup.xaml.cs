using Framework.MauiX.Helpers;
using AdventureWorksLT2019.MauiXApp.ViewModels.Customer;

namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

public partial class ListOrderBysPopup : CommunityToolkit.Maui.Views.Popup
{
    public ListOrderBysPopup()
    {
        var viewModel = ServiceHelper.GetService<ListVM>();
        BindingContext = viewModel;
        viewModel.AttachListOrderBysPopupCommand(new Command(OnCancelled));

        InitializeComponent();
        Size = PopupHelper.GetPopupSize();
    }

    private void OnCancelled()
    {
        Close();
    }
}

