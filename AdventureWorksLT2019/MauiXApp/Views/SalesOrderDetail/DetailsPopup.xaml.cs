using AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderDetail;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail;

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
        Size = new(0.5 * (deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density), 0.5 * (deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density));
    }

    protected void OnCancelled()
    {
        Close();
    }
}

