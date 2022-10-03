using AdventureWorksLT2019.MauiXApp.ViewModels.ProductModelProductDescription;
using Framework.MauiX.Helpers;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription;

public partial class ListPage : ContentPage
{
    private readonly ListVM viewModel;

    public ListPage()
    {
        viewModel = ServiceHelper.GetService<ListVM>();
        BindingContext = viewModel;
        // TODO: this is a workaround of SelectedItems binding not working
        // https://github.com/dotnet/maui/issues/8435
        // SelectedItems="{Binding Path=SelectedItems, Mode=TwoWay}"
        viewModel.AttachClearSelectedItemsCommand(new Command(OnClearSelectedItems, ()=> viewModel.EnableMultiSelectCommands()));

        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.DoSearch(true, true);
    }

    /// <summary>
    /// TODO: this is a workaround of SelectedItems binding not working
    /// https://github.com/dotnet/maui/issues/8435
    /// SelectedItems="{Binding Path=SelectedItems, Mode=TwoWay}"
    /// </summary>
    private void OnClearSelectedItems()
    {
        collectionView.SelectedItems.Clear();
        viewModel.SelectedItems.Clear();
        viewModel.RefreshMultiSelectCommandsCanExecute();
    }
}

