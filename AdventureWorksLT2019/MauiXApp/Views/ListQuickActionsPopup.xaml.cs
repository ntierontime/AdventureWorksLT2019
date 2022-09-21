namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

public partial class ListQuickActionsPopup : CommunityToolkit.Maui.Views.Popup
{
    public ListQuickActionsPopup()
	{
        var viewModel = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ListVM>();
        BindingContext = viewModel;
        viewModel.AttachListQuickActionsPopupCommands(new Command(OnCancelled));

        InitializeComponent();
        IDeviceDisplay deviceDisplay = Framework.MauiX.Helpers.ServiceHelper.GetService<IDeviceDisplay>();
        Size = new(deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density, deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density);
    }

    protected void OnCancelled()
    {
        Close();
    }
}