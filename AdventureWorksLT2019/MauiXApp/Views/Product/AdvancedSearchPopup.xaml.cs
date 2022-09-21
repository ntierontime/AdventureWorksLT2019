using AdventureWorksLT2019.MauiXApp.ViewModels.Product;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views.Product;

/// <summary>
/// This Popup is called by the Filter Button beside the Search Entry.
/// </summary>
public partial class AdvancedSearchPopup : CommunityToolkit.Maui.Views.Popup
{
    public AdvancedSearchPopup()
    {
        var viewModel = ServiceHelper.GetService<ListVM>();
        BindingContext = viewModel;
        viewModel.AttachAdvancedSearchPopupCommands(new Command(OnCancelled));
        InitializeComponent();
        // WinUI Size is not correct.
        //IDeviceDisplay deviceDisplay = ServiceHelper.GetService<IDeviceDisplay>();
        //Size = new(0.9 * deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density, 0.9 * deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density);
    }

    protected void OnCancelled()
    {
        Close();
    }

    void OnFruitsRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        //RadioButton button = sender as RadioButton;
        //colorLabel.Text = $"You have chosen: {button.Content}";
    }
}

