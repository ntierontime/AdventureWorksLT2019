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

    private AdventureWorksLT2019.MauiXApp.DataModels.ProductDataModel m___Master__;
    public AdventureWorksLT2019.MauiXApp.DataModels.ProductDataModel __Master__
    {
        get => m___Master__;
        set => SetProperty(ref m___Master__, value);
    }

    // 4. ListTable = 4,
    private ObservableCollection<AdventureWorksLT2019.MauiXApp.DataModels.SalesOrderDetailDataModel> m_SalesOrderDetails_Via_ProductID;
    public ObservableCollection<AdventureWorksLT2019.MauiXApp.DataModels.SalesOrderDetailDataModel> SalesOrderDetails_Via_ProductID
    {
        get => m_SalesOrderDetails_Via_ProductID;
        set => SetProperty(ref m_SalesOrderDetails_Via_ProductID, value);
    }

    private readonly AdventureWorksLT2019.MauiXApp.Services.ProductService _dataService;

    public ICommand LaunchProductCategoryFKItemViewCommand { get; private set; }

    public ICommand LaunchProductSalesOrderDetailItemViewCommand { get; private set; }
    public ICommand CloseCommand { get; private set; }

    public DashboardVM(AdventureWorksLT2019.MauiXApp.Services.ProductService dataService)
    {
        _dataService = dataService;

        WeakReferenceMessenger.Default.Register<DashboardVM, AdventureWorksLT2019.MauiXApp.Messages.ProductIdentifierMessage>(
            this, async (r, m) =>
            {
                if (m.ItemView != Framework.Models.ViewItemTemplates.Dashboard)
                    return;
                ReturnPath = m.ReturnPath;
                await LoadData(m.Value);
            });

        LaunchProductCategoryFKItemViewCommand = AdventureWorksLT2019.MauiXApp.Common.Helpers.LaunchViewCommandsHelper.GetLaunchProductCategoryDetailsPopupCommand();
        LaunchProductSalesOrderDetailItemViewCommand = AdventureWorksLT2019.MauiXApp.Common.Helpers.LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderDetailsPopupCommand();
        CloseCommand = AdventureWorksLT2019.MauiXApp.Common.Services.AppShellService.ShellGotoAbsoluteCommand;

    }

    public async Task LoadData(AdventureWorksLT2019.MauiXApp.DataModels.ProductIdentifier identifier)
    {
        var response = await _dataService.GetCompositeModel(identifier);

        // 1. MasterData - ProductCompositeModel
        if (response == null || response.Responses == null || 
            !response.Responses.ContainsKey(AdventureWorksLT2019.MauiXApp.DataModels.ProductCompositeModel.__DataOptions__.__Master__))
        {
            //TODO: __Master__ Failed
            return;
        }

        var masterResponse = response.Responses[AdventureWorksLT2019.MauiXApp.DataModels.ProductCompositeModel.__DataOptions__.__Master__];
        if(masterResponse.Status != System.Net.HttpStatusCode.OK)
        {
            //TODO: __Master__ Failed
            return;
        }

        __Master__ = response.__Master__;

        // 4. SalesOrderDetails_Via_ProductID
        if(response.Responses.ContainsKey(AdventureWorksLT2019.MauiXApp.DataModels.ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID) &&
            response.Responses[AdventureWorksLT2019.MauiXApp.DataModels.ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID].Status == System.Net.HttpStatusCode.OK)
        {
            SalesOrderDetails_Via_ProductID = new ObservableCollection<AdventureWorksLT2019.MauiXApp.DataModels.SalesOrderDetailDataModel>(response.SalesOrderDetails_Via_ProductID);
        }
    }
}
