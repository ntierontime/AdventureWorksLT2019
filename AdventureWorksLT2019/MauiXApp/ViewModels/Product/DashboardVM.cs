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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Product;

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

    private ProductDataModel m___Master__;
    public ProductDataModel __Master__
    {
        get => m___Master__;
        set => SetProperty(ref m___Master__, value);
    }

    // 4. ListTable = 4,
    private ObservableCollection<SalesOrderDetailDataModel> m_SalesOrderDetails_Via_ProductID = new();
    public ObservableCollection<SalesOrderDetailDataModel> SalesOrderDetails_Via_ProductID
    {
        get => m_SalesOrderDetails_Via_ProductID;
        set => SetProperty(ref m_SalesOrderDetails_Via_ProductID, value);
    }

    private readonly ProductService _dataService;

    public ICommand LaunchMaster_ProductCategoryFKItemViewCommand { get; private set; }

    public ICommand LaunchMaster_ProductModelFKItemViewCommand { get; private set; }

    // 4. ListTable = 4,
    public ICommand LaunchList_SalesOrderDetailItemViewCommand { get; private set; }

    public ICommand CloseCommand { get; private set; }

    public DashboardVM(ProductService dataService)
    {
        _dataService = dataService;

        WeakReferenceMessenger.Default.Register<DashboardVM, ProductIdentifierMessage>(
            this, async (r, m) =>
            {
                if (m.ItemView != ViewItemTemplates.Dashboard)
                    return;
                ReturnPath = m.ReturnPath;
                await LoadData(m.Value);
            });

        LaunchMaster_ProductCategoryFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryDetailsPopupCommand();

        LaunchMaster_ProductModelFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductModelDetailsPopupCommand();

    // 4. ListTable = 4,

        CloseCommand = AppShellService.ShellGotoAbsoluteCommand;

    }

    public async Task LoadData(ProductIdentifier identifier)
    {
        var response = await _dataService.GetCompositeModel(identifier);

        // 1. MasterData - ProductCompositeModel
        if (response == null || response.Responses == null ||
            !response.Responses.ContainsKey(ProductCompositeModel.__DataOptions__.__Master__))
        {
            //TODO: __Master__ Failed
            return;
        }

        var masterResponse = response.Responses[ProductCompositeModel.__DataOptions__.__Master__];
        if(masterResponse.Status != System.Net.HttpStatusCode.OK)
        {
            //TODO: __Master__ Failed
            return;
        }

        __Master__ = response.__Master__;

        // 4. ListTable = 4,

        if(response.Responses.ContainsKey(ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID) &&
            response.Responses[ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID].Status == System.Net.HttpStatusCode.OK)
        {
            SalesOrderDetails_Via_ProductID = new ObservableCollection<SalesOrderDetailDataModel>(response.SalesOrderDetails_Via_ProductID);
        }

    }
}

