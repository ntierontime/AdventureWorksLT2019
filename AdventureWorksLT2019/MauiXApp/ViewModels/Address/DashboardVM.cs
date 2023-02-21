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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Address;

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

    private AddressDataModel m___Master__;
    public AddressDataModel __Master__
    {
        get => m___Master__;
        set => SetProperty(ref m___Master__, value);
    }

    // 4. ListTable = 4,
    private ObservableCollection<CustomerAddressDataModel> m_CustomerAddresses_Via_AddressID = new();
    public ObservableCollection<CustomerAddressDataModel> CustomerAddresses_Via_AddressID
    {
        get => m_CustomerAddresses_Via_AddressID;
        set => SetProperty(ref m_CustomerAddresses_Via_AddressID, value);
    }
    private ObservableCollection<SalesOrderHeaderDataModel> m_SalesOrderHeaders_Via_BillToAddressID = new();
    public ObservableCollection<SalesOrderHeaderDataModel> SalesOrderHeaders_Via_BillToAddressID
    {
        get => m_SalesOrderHeaders_Via_BillToAddressID;
        set => SetProperty(ref m_SalesOrderHeaders_Via_BillToAddressID, value);
    }
    private ObservableCollection<SalesOrderHeaderDataModel> m_SalesOrderHeaders_Via_ShipToAddressID = new();
    public ObservableCollection<SalesOrderHeaderDataModel> SalesOrderHeaders_Via_ShipToAddressID
    {
        get => m_SalesOrderHeaders_Via_ShipToAddressID;
        set => SetProperty(ref m_SalesOrderHeaders_Via_ShipToAddressID, value);
    }

    private readonly AddressService _dataService;

    // 4. ListTable = 4,
    public ICommand LaunchList_CustomerAddressItemViewCommand { get; private set; }
    public ICommand LaunchList_SalesOrderHeaderItemViewCommand { get; private set; }

    public ICommand CloseCommand { get; private set; }

    public DashboardVM(AddressService dataService)
    {
        _dataService = dataService;

        WeakReferenceMessenger.Default.Register<DashboardVM, AddressIdentifierMessage>(
            this, async (r, m) =>
            {
                if (m.ItemView != ViewItemTemplates.Dashboard)
                    return;
                ReturnPath = m.ReturnPath;
                await LoadData(m.Value);
            });

    // 4. ListTable = 4,

        CloseCommand = AppShellService.ShellGotoAbsoluteCommand;

    }

    public async Task LoadData(AddressIdentifier identifier)
    {
        var response = await _dataService.GetCompositeModel(identifier);

        // 1. MasterData - AddressCompositeModel
        if (response == null || response.Responses == null ||
            !response.Responses.ContainsKey(AddressCompositeModel.__DataOptions__.__Master__))
        {
            //TODO: __Master__ Failed
            return;
        }

        var masterResponse = response.Responses[AddressCompositeModel.__DataOptions__.__Master__];
        if(masterResponse.Status != System.Net.HttpStatusCode.OK)
        {
            //TODO: __Master__ Failed
            return;
        }

        __Master__ = response.__Master__;

        // 4. ListTable = 4,

        if(response.Responses.ContainsKey(AddressCompositeModel.__DataOptions__.CustomerAddresses_Via_AddressID) &&
            response.Responses[AddressCompositeModel.__DataOptions__.CustomerAddresses_Via_AddressID].Status == System.Net.HttpStatusCode.OK)
        {
            CustomerAddresses_Via_AddressID = new ObservableCollection<CustomerAddressDataModel>(response.CustomerAddresses_Via_AddressID);
        }

        if(response.Responses.ContainsKey(AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_BillToAddressID) &&
            response.Responses[AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_BillToAddressID].Status == System.Net.HttpStatusCode.OK)
        {
            SalesOrderHeaders_Via_BillToAddressID = new ObservableCollection<SalesOrderHeaderDataModel>(response.SalesOrderHeaders_Via_BillToAddressID);
        }

        if(response.Responses.ContainsKey(AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_ShipToAddressID) &&
            response.Responses[AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_ShipToAddressID].Status == System.Net.HttpStatusCode.OK)
        {
            SalesOrderHeaders_Via_ShipToAddressID = new ObservableCollection<SalesOrderHeaderDataModel>(response.SalesOrderHeaders_Via_ShipToAddressID);
        }

    }
}

