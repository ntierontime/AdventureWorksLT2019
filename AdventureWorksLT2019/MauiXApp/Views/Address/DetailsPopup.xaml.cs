using AdventureWorksLT2019.MauiXApp.ViewModels.Address;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.Address;

public partial class DetailsPopup : Popup
{
    public DetailsPopup()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachDetailsViewCommands(new Command(OnCancelled));
        viewModel.RequestItem(ViewItemTemplates.Details);

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

