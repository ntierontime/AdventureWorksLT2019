using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

public partial class DeletePopup : Popup
{
	public DeletePopup()
	{
        var viewModel = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachDeleteViewCommands(new Command(OnCancelled));
        viewModel.RequestItem(Framework.Models.ViewItemTemplates.Delete);

        InitializeComponent();
	}

    protected void OnCancelled()
    {
        Close();
    }
}