using AdventureWorksLT2019.MauiXApp.ViewModels.Address;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.Address;

public partial class ListQuickActionsPopup : CommunityToolkit.Maui.Views.Popup
{
    public ListQuickActionsPopup()
    {
        var viewModel = ServiceHelper.GetService<ListVM>();
        BindingContext = viewModel;
        viewModel.AttachListQuickActionsPopupCommands(new Command(OnCancelled));

        InitializeComponent();
        IDeviceDisplay deviceDisplay = ServiceHelper.GetService<IDeviceDisplay>();
#if WINDOWS
        Size = new(0.5 * (deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density), 0.5 * (deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density));
#else
        // Full Screen On Android / IOs ...
        Size = new((deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density), (deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density));
#endif
    }

    protected void OnCancelled()
    {
        Close();
    }
}

