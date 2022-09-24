using AdventureWorksLT2019.MauiXApp.ViewModels.Address;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.Address;

public partial class ListOrderBysPopup : CommunityToolkit.Maui.Views.Popup
{
    public ListOrderBysPopup()
    {
        var viewModel = ServiceHelper.GetService<ListVM>();
        BindingContext = viewModel;
        viewModel.AttachListOrderBysPopupCommand(new Command(OnCancelled));

        InitializeComponent();
        IDeviceDisplay deviceDisplay = ServiceHelper.GetService<IDeviceDisplay>();
#if WINDOWS
        Size = new(0.5 * (deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density), 0.5 * (deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density));
#else
        // Full Screen On Android / IOs ...
        Size = new(0.98 * (deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density), 0.89 * (deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density));
#endif
    }

    protected void OnCancelled()
    {
        Close();
    }
}

