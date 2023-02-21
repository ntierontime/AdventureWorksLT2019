using AdventureWorksLT2019.MauiXApp.Common.Helpers;
using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ProductCategory;

public class DashboardVM : ObservableObject
{
    private string m_ReturnPath;
    /// <summary>
    /// where to go, if we have Close button
    /// </summary>
    public string ReturnPath
    {
        get => m_ReturnPath;
        set => SetProperty(ref m_ReturnPath, value);
    }

    private ProductCategoryDataModel m___Master__;
    public ProductCategoryDataModel __Master__
    {
        get => m___Master__;
        set => SetProperty(ref m___Master__, value);
    }

    // 4. ListTable = 4,
    private ObservableCollection<ProductDataModel> m_Products_Via_ProductCategoryID = new();
    public ObservableCollection<ProductDataModel> Products_Via_ProductCategoryID
    {
        get => m_Products_Via_ProductCategoryID;
        set => SetProperty(ref m_Products_Via_ProductCategoryID, value);
    }
    private ObservableCollection<ProductCategoryDataModel> m_ProductCategories_Via_ParentProductCategoryID = new();
    public ObservableCollection<ProductCategoryDataModel> ProductCategories_Via_ParentProductCategoryID
    {
        get => m_ProductCategories_Via_ParentProductCategoryID;
        set => SetProperty(ref m_ProductCategories_Via_ParentProductCategoryID, value);
    }

    private readonly ProductCategoryService _dataService;

    public ICommand LaunchMaster_ProductCategoryFKItemViewCommand { get; private set; }

    // 4. ListTable = 4,
    public ICommand LaunchList_ProductItemViewCommand { get; private set; }
    public ICommand LaunchList_ProductCategoryItemViewCommand { get; private set; }

    public ICommand CloseCommand { get; private set; }

    public DashboardVM(ProductCategoryService dataService)
    {
        _dataService = dataService;

        WeakReferenceMessenger.Default.Register<DashboardVM, ProductCategoryIdentifierMessage>(
            this, async (r, m) =>
            {
                if (m.ItemView != ViewItemTemplates.Dashboard)
                    return;
                ReturnPath = m.ReturnPath;
                await LoadData(m.Value);
            });

        LaunchMaster_ProductCategoryFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryDetailsPopupCommand();

    // 4. ListTable = 4,

        CloseCommand = AppShellService.ShellGotoAbsoluteCommand;

    }

    public async Task LoadData(ProductCategoryIdentifier identifier)
    {
        var response = await _dataService.GetCompositeModel(identifier);

        // 1. MasterData - ProductCategoryCompositeModel
        if (response == null || response.Responses == null ||
            !response.Responses.ContainsKey(ProductCategoryCompositeModel.__DataOptions__.__Master__))
        {
            //TODO: __Master__ Failed
            return;
        }

        var masterResponse = response.Responses[ProductCategoryCompositeModel.__DataOptions__.__Master__];
        if(masterResponse.Status != System.Net.HttpStatusCode.OK)
        {
            //TODO: __Master__ Failed
            return;
        }

        __Master__ = response.__Master__;

        // 4. ListTable = 4,

        if(response.Responses.ContainsKey(ProductCategoryCompositeModel.__DataOptions__.Products_Via_ProductCategoryID) &&
            response.Responses[ProductCategoryCompositeModel.__DataOptions__.Products_Via_ProductCategoryID].Status == System.Net.HttpStatusCode.OK)
        {
            Products_Via_ProductCategoryID = new ObservableCollection<ProductDataModel>(response.Products_Via_ProductCategoryID);
        }

        if(response.Responses.ContainsKey(ProductCategoryCompositeModel.__DataOptions__.ProductCategories_Via_ParentProductCategoryID) &&
            response.Responses[ProductCategoryCompositeModel.__DataOptions__.ProductCategories_Via_ParentProductCategoryID].Status == System.Net.HttpStatusCode.OK)
        {
            ProductCategories_Via_ParentProductCategoryID = new ObservableCollection<ProductCategoryDataModel>(response.ProductCategories_Via_ParentProductCategoryID);
        }

    }
}

