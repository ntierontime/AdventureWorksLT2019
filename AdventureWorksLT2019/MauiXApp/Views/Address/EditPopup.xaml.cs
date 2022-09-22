using AdventureWorksLT2019.MauiXApp.ViewModels.Address;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.Address;

public partial class EditPopup : Popup
{
    public EditPopup()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachEditViewCommands(new Command(OnCancelled));
        viewModel.RequestItem(ViewItemTemplates.Edit);

        InitializeComponent();
        IDeviceDisplay deviceDisplay = ServiceHelper.GetService<IDeviceDisplay>();
        Size = new(0.8 * (deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density), 0.8 * (deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density));
    }

    protected void OnCancelled()
    {
        Close();
    }
}

