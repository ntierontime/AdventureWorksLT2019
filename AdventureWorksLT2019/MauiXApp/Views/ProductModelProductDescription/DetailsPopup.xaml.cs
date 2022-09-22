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
        viewModel.RequestItem(ViewItemTemplates.Details);

        InitializeComponent();
        IDeviceDisplay deviceDisplay = ServiceHelper.GetService<IDeviceDisplay>();
        Size = new(deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density, deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density);
    }

    protected void OnCancelled()
    {
        Close();
    }
}

