using AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderDetail;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail;

public partial class ListQuickActionsPopup : CommunityToolkit.Maui.Views.Popup
{
    public ListQuickActionsPopup()
    {
        var viewModel = ServiceHelper.GetService<ListVM>();
        BindingContext = viewModel;
        viewModel.AttachListQuickActionsPopupCommands(new Command(OnCancelled));

        InitializeComponent();
        IDeviceDisplay deviceDisplay = ServiceHelper.GetService<IDeviceDisplay>();
        Size = new(0.5 * (deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density), 0.5 * (deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density));
    }

    protected void OnCancelled()
    {
        Close();
    }
}

