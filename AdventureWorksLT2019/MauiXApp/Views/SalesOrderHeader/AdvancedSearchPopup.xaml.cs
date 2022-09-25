using AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader;

/// <summary>
/// This Popup is called by the Filter Button beside the Search Entry.
/// </summary>
public partial class AdvancedSearchPopup : CommunityToolkit.Maui.Views.Popup
{
    public AdvancedSearchPopup()
    {
        var viewModel = ServiceHelper.GetService<ListVM>();
        BindingContext = viewModel;
        viewModel.AttachAdvancedSearchPopupCommands(new Command(OnCancelled));
        InitializeComponent();
        // WinUI Size is not correct.
        Size = PopupHelper.GetPopupSize();
    }

    private void OnCancelled()
    {
        Close();
    }
}

