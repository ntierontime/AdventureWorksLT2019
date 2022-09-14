using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

public partial class CreatePopup : Popup
{
	public CreatePopup()
	{
        var viewModel = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachCreateViewCommands(new Command(OnCancelled));
        viewModel.RequestItem(Framework.Models.ViewItemTemplates.Create);

        InitializeComponent();
	}

    protected void OnCancelled()
    {
        Close();
    }
}