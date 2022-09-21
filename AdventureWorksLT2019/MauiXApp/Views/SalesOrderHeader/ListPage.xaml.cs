using AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader;
using Framework.MauiX.Helpers;
using Framework.Models;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader;

public partial class ListPage : ContentPage
{
    private readonly ListVM viewModel;

    public ListPage()
    {
        viewModel = ServiceHelper.GetService<ListVM>();
        BindingContext = viewModel;
        viewModel.AttachPopupLaunchCommands(
            new Command(OnLaunchAdvancedSearchPopup),
            new Command(OnLaunchListQuickActionsPopup),
            new Command<ViewItemTemplates>(OnLaunchItemPopupView)
            );
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.DoSearch(true, true);
    }
    public async void OnLaunchAdvancedSearchPopup()
    {
        var popup = new AdvancedSearchPopup();
        await this.ShowPopupAsync(popup);
    }
    public async void OnLaunchListQuickActionsPopup()
    {
        var popup = new ListQuickActionsPopup();
        await this.ShowPopupAsync(popup);
    }
    public async void OnLaunchItemPopupView(ViewItemTemplates itemView)
    {
        if (itemView == ViewItemTemplates.Details)
        {
            var popup = new DetailsPopup();
            await this.ShowPopupAsync(popup);
            return;
        }

        if (itemView == ViewItemTemplates.Edit)
        {
            var popup = new EditPopup();
            await this.ShowPopupAsync(popup);
            return;
        }
        if (itemView == ViewItemTemplates.Create)
        {
            var popup = new CreatePopup();
            await this.ShowPopupAsync(popup);
            return;
        }
        if (itemView == ViewItemTemplates.Delete)
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.DeletePopup();
            await this.ShowPopupAsync(popup);
            return;
        }
    }
}

