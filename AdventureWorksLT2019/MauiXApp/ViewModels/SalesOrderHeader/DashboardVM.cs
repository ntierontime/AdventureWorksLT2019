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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader;

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

    private SalesOrderHeaderDataModel m___Master__;
    public SalesOrderHeaderDataModel __Master__
    {
        get => m___Master__;
        set => SetProperty(ref m___Master__, value);
    }

    // 4. ListTable = 4,
    private ObservableCollection<SalesOrderDetailDataModel> m_SalesOrderDetails_Via_SalesOrderID = new();
    public ObservableCollection<SalesOrderDetailDataModel> SalesOrderDetails_Via_SalesOrderID
    {
        get => m_SalesOrderDetails_Via_SalesOrderID;
        set => SetProperty(ref m_SalesOrderDetails_Via_SalesOrderID, value);
    }

    private readonly SalesOrderHeaderService _dataService;

    public ICommand LaunchMaster_CustomerFKItemViewCommand { get; private set; }

    public ICommand LaunchMaster_AddressFKItemViewCommand { get; private set; }

    // 4. ListTable = 4,
    public ICommand LaunchList_SalesOrderDetailItemViewCommand { get; private set; }

    public ICommand CloseCommand { get; private set; }

    public DashboardVM(SalesOrderHeaderService dataService)
    {
        _dataService = dataService;

        WeakReferenceMessenger.Default.Register<DashboardVM, SalesOrderHeaderIdentifierMessage>(
            this, async (r, m) =>
            {
                if (m.ItemView != ViewItemTemplates.Dashboard)
                    return;
                ReturnPath = m.ReturnPath;
                await LoadData(m.Value);
            });

        LaunchMaster_CustomerFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchCustomerDetailsPopupCommand();

        LaunchMaster_AddressFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchAddressDetailsPopupCommand();

    // 4. ListTable = 4,

        CloseCommand = AppShellService.ShellGotoAbsoluteCommand;

    }

    public async Task LoadData(SalesOrderHeaderIdentifier identifier)
    {
        var response = await _dataService.GetCompositeModel(identifier);

        // 1. MasterData - SalesOrderHeaderCompositeModel
        if (response == null || response.Responses == null ||
            !response.Responses.ContainsKey(SalesOrderHeaderCompositeModel.__DataOptions__.__Master__))
        {
            //TODO: __Master__ Failed
            return;
        }

        var masterResponse = response.Responses[SalesOrderHeaderCompositeModel.__DataOptions__.__Master__];
        if(masterResponse.Status != System.Net.HttpStatusCode.OK)
        {
            //TODO: __Master__ Failed
            return;
        }

        __Master__ = response.__Master__;

        // 4. ListTable = 4,

        if(response.Responses.ContainsKey(SalesOrderHeaderCompositeModel.__DataOptions__.SalesOrderDetails_Via_SalesOrderID) &&
            response.Responses[SalesOrderHeaderCompositeModel.__DataOptions__.SalesOrderDetails_Via_SalesOrderID].Status == System.Net.HttpStatusCode.OK)
        {
            SalesOrderDetails_Via_SalesOrderID = new ObservableCollection<SalesOrderDetailDataModel>(response.SalesOrderDetails_Via_SalesOrderID);
        }

    }
}

