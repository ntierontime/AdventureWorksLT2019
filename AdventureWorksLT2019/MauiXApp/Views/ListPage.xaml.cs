using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.Views.Customer;

public partial class ListPage : ContentPage
{
    private readonly AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ListVM viewModel;

    public ListPage()
	{
        viewModel = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ListVM>();
        BindingContext = viewModel;
        viewModel.AttachPopupLaunchCommands(
            new Command(OnLaunchAdvancedSearchPopup),
            new Command(OnLaunchListQuickActionsPopup),
            new Command<Framework.Models.ViewItemTemplates>(OnLaunchItemPopupView)
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
        var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.AdvancedSearchPopup();
        await this.ShowPopupAsync(popup);
    }
    public async void OnLaunchListQuickActionsPopup()
    {
        var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.ListQuickActionsPopup();
        await this.ShowPopupAsync(popup);
    }
    public async void OnLaunchItemPopupView(Framework.Models.ViewItemTemplates itemView)
    {
        if (itemView == Framework.Models.ViewItemTemplates.Details)
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.DetailsPopup();
            await this.ShowPopupAsync(popup);
            return;
        }

        if (itemView == Framework.Models.ViewItemTemplates.Edit)
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.EditPopup();
            await this.ShowPopupAsync(popup);
            return;
        }
        if (itemView == Framework.Models.ViewItemTemplates.Create)
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.CreatePopup();
            await this.ShowPopupAsync(popup);
            return;
        }
        if (itemView == Framework.Models.ViewItemTemplates.Delete)
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.DeletePopup();
            await this.ShowPopupAsync(popup);
            return;
        }
    }
}