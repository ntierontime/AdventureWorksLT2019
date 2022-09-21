using AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderDetail;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;

namespace AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail;

public partial class DeletePopup : Popup
{
    public DeletePopup()
    {
        var viewModel = ServiceHelper.GetService<ItemVM>();
        BindingContext = viewModel;
        viewModel.AttachDeleteViewCommands(new Command(OnCancelled));
        viewModel.RequestItem(ViewItemTemplates.Delete);

        InitializeComponent();
    }

    protected void OnCancelled()
    {
        Close();
    }
}

