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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderDetail;

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

    private SalesOrderDetailDataModel m___Master__;
    public SalesOrderDetailDataModel __Master__
    {
        get => m___Master__;
        set => SetProperty(ref m___Master__, value);
    }

    // 2. AncestorTable = 2,
    private ProductDataModel m_Product;
    public ProductDataModel Product
    {
        get => m_Product;
        set => SetProperty(ref m_Product, value);
    }
    private ProductCategoryDataModel m_ProductCategory;
    public ProductCategoryDataModel ProductCategory
    {
        get => m_ProductCategory;
        set => SetProperty(ref m_ProductCategory, value);
    }
    private SalesOrderHeaderDataModel m_SalesOrderHeader;
    public SalesOrderHeaderDataModel SalesOrderHeader
    {
        get => m_SalesOrderHeader;
        set => SetProperty(ref m_SalesOrderHeader, value);
    }

    private readonly SalesOrderDetailService _dataService;

    public ICommand LaunchMaster_SalesOrderHeaderFKItemViewCommand { get; private set; }

    public ICommand LaunchMaster_ProductFKItemViewCommand { get; private set; }

    public ICommand LaunchMaster_ProductCategoryFKItemViewCommand { get; private set; }

    public ICommand LaunchMaster_ProductModelFKItemViewCommand { get; private set; }

    public ICommand LaunchMaster_AddressFKItemViewCommand { get; private set; }

    public ICommand LaunchMaster_CustomerFKItemViewCommand { get; private set; }

    public ICommand CloseCommand { get; private set; }

    public DashboardVM(SalesOrderDetailService dataService)
    {
        _dataService = dataService;

        WeakReferenceMessenger.Default.Register<DashboardVM, SalesOrderDetailIdentifierMessage>(
            this, async (r, m) =>
            {
                if (m.ItemView != ViewItemTemplates.Dashboard)
                    return;
                ReturnPath = m.ReturnPath;
                await LoadData(m.Value);
            });

        LaunchMaster_SalesOrderHeaderFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderDetailsPopupCommand();
        //LaunchMaster_SalesOrderHeaderFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderDetailsPageCommand(AppShellRoutes.SalesOrderDetailListPage);

        LaunchMaster_ProductFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductDetailsPopupCommand();
        //LaunchMaster_ProductFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductDetailsPageCommand(AppShellRoutes.SalesOrderDetailListPage);

        LaunchMaster_ProductCategoryFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryDetailsPopupCommand();
        //LaunchMaster_ProductCategoryFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryDetailsPageCommand(AppShellRoutes.SalesOrderDetailListPage);

        LaunchMaster_ProductModelFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductModelDetailsPopupCommand();
        //LaunchMaster_ProductModelFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductModelDetailsPageCommand(AppShellRoutes.SalesOrderDetailListPage);

        LaunchMaster_AddressFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchAddressDetailsPopupCommand();
        //LaunchMaster_AddressFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchAddressDetailsPageCommand(AppShellRoutes.SalesOrderDetailListPage);

        LaunchMaster_CustomerFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchCustomerDetailsPopupCommand();
        //LaunchMaster_CustomerFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchCustomerDetailsPageCommand(AppShellRoutes.SalesOrderDetailListPage);

        CloseCommand = AppShellService.ShellGotoAbsoluteCommand;

    }

    public async Task LoadData(SalesOrderDetailIdentifier identifier)
    {
        var response = await _dataService.GetCompositeModel(identifier);

        // 1. MasterData - SalesOrderDetailCompositeModel
        if (response == null || response.Responses == null ||
            !response.Responses.ContainsKey(SalesOrderDetailCompositeModel.__DataOptions__.__Master__))
        {
            //TODO: __Master__ Failed
            return;
        }

        var masterResponse = response.Responses[SalesOrderDetailCompositeModel.__DataOptions__.__Master__];
        if(masterResponse.Status != System.Net.HttpStatusCode.OK)
        {
            //TODO: __Master__ Failed
            return;
        }

        __Master__ = response.__Master__;

        // 2. AncestorTable = 2,

        if(response.Responses.ContainsKey(SalesOrderDetailCompositeModel.__DataOptions__.Product) &&
            response.Responses[SalesOrderDetailCompositeModel.__DataOptions__.Product].Status == System.Net.HttpStatusCode.OK)
        {
            Product = response.Product;
        }

        if(response.Responses.ContainsKey(SalesOrderDetailCompositeModel.__DataOptions__.ProductCategory) &&
            response.Responses[SalesOrderDetailCompositeModel.__DataOptions__.ProductCategory].Status == System.Net.HttpStatusCode.OK)
        {
            ProductCategory = response.ProductCategory;
        }

        if(response.Responses.ContainsKey(SalesOrderDetailCompositeModel.__DataOptions__.SalesOrderHeader) &&
            response.Responses[SalesOrderDetailCompositeModel.__DataOptions__.SalesOrderHeader].Status == System.Net.HttpStatusCode.OK)
        {
            SalesOrderHeader = response.SalesOrderHeader;
        }

    }
}

