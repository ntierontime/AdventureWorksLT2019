using AdventureWorksLT2019.MauiXApp.ViewModels.ProductModelProductDescription;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription;

public partial class EditPopup : Popup
{
    public EditPopup()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachEditViewCommands(new Command(OnCancelled));
        viewModel.RequestItem(ViewItemTemplates.Edit);

        InitializeComponent();
    }

    protected void OnCancelled()
    {
        Close();
    }
}

