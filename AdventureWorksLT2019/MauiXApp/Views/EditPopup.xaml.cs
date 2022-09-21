using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

public partial class EditPopup : Popup
{
	public EditPopup()
	{
        var viewModel = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachEditViewCommands(new Command(OnCancelled));
        viewModel.RequestItem(Framework.Models.ViewItemTemplates.Edit);

        InitializeComponent();
	}

    protected void OnCancelled()
    {
        Close();
    }
}