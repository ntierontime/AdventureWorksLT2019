namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

/// <summary>
/// This Popup is called by the Filter Button beside the Search Entry.
/// </summary>
public partial class AdvancedSearchPopup : CommunityToolkit.Maui.Views.Popup
{
    public AdvancedSearchPopup()
	{
        var viewModel = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ListVM>();
        BindingContext = viewModel;
        viewModel.AttachAdvancedSearchPopupCommands(new Command(OnCancelled));
        InitializeComponent();
        IDeviceDisplay deviceDisplay = Framework.MauiX.Helpers.ServiceHelper.GetService<IDeviceDisplay>();
        Size = new(deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density, deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density);
    }

    protected void OnCancelled()
    {
        Close();
    }
}