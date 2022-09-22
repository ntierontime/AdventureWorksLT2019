using AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader;

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
        Size = new(deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density, deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density);
    }

    protected void OnCancelled()
    {
        Close();
    }
}

