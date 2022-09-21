using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

public partial class DetailsPopup : Popup
{
	public DetailsPopup()
	{
        var viewModel = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachDetailsViewCommands(new Command(OnCancelled));
        viewModel.RequestItem(Framework.Models.ViewItemTemplates.Details);

        InitializeComponent();
	}

    protected void OnCancelled()
    {
        Close();
    }
}