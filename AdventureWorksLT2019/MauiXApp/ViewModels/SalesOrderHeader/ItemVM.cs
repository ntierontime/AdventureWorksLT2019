using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using Framework.MauiX.Helpers;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader;

public class ItemVM : ItemVMBase<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel, SalesOrderHeaderService, SalesOrderHeaderItemChangedMessage, SalesOrderHeaderItemRequestMessage>
{

    // ForeignKeys.1. CustomerIDList
    private List<NameValuePair<int>> m_CustomerIDList;
    public List<NameValuePair<int>> CustomerIDList
    {
        get => m_CustomerIDList;
        set => SetProperty(ref m_CustomerIDList, value);
    }

    private NameValuePair<int> m_SelectedCustomerID;
    public NameValuePair<int> SelectedCustomerID
    {
        get => m_SelectedCustomerID;
        set
        {
            SetProperty(ref m_SelectedCustomerID, value);
            Item.CustomerID = value.Value;
        }
    }

    // ForeignKeys.2. ShipToAddressIDList
    private List<NameValuePair<int>> m_ShipToAddressIDList;
    public List<NameValuePair<int>> ShipToAddressIDList
    {
        get => m_ShipToAddressIDList;
        set => SetProperty(ref m_ShipToAddressIDList, value);
    }

    private NameValuePair<int> m_SelectedShipToAddressID;
    public NameValuePair<int> SelectedShipToAddressID
    {
        get => m_SelectedShipToAddressID;
        set
        {
            SetProperty(ref m_SelectedShipToAddressID, value);
            Item.ShipToAddressID = value.Value;
        }
    }

    // ForeignKeys.3. BillToAddressIDList
    private List<NameValuePair<int>> m_BillToAddressIDList;
    public List<NameValuePair<int>> BillToAddressIDList
    {
        get => m_BillToAddressIDList;
        set => SetProperty(ref m_BillToAddressIDList, value);
    }

    private NameValuePair<int> m_SelectedBillToAddressID;
    public NameValuePair<int> SelectedBillToAddressID
    {
        get => m_SelectedBillToAddressID;
        set
        {
            SetProperty(ref m_SelectedBillToAddressID, value);
            Item.BillToAddressID = value.Value;
        }
    }
    public ItemVM(SalesOrderHeaderService dataService)
        : base(dataService)
    {
    }

    protected override async Task LoadCodeListsIfAny(ViewItemTemplates itemView)
    {

        // ForeignKeys.1. CustomerIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetCustomerCodeList(new CustomerAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                CustomerIDList = new List<NameValuePair<int>>(response.ResponseBody);
                if (itemView == ViewItemTemplates.Create)
                {
                    SelectedCustomerID = CustomerIDList.FirstOrDefault();
                }
                else if (itemView == ViewItemTemplates.Edit)
                {
                    SelectedCustomerID = CustomerIDList.FirstOrDefault(t=>t.Value == Item.CustomerID);
                }
            }
        }

        // ForeignKeys.2. ShipToAddressIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetAddressCodeList(new AddressAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ShipToAddressIDList = new List<NameValuePair<int>>(response.ResponseBody);
                if (itemView == ViewItemTemplates.Create)
                {
                    SelectedShipToAddressID = ShipToAddressIDList.FirstOrDefault();
                }
                else if (itemView == ViewItemTemplates.Edit)
                {
                    SelectedShipToAddressID = ShipToAddressIDList.FirstOrDefault(t=>t.Value == Item.ShipToAddressID);
                }
            }
        }

        // ForeignKeys.3. BillToAddressIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetAddressCodeList(new AddressAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                BillToAddressIDList = new List<NameValuePair<int>>(response.ResponseBody);
                if (itemView == ViewItemTemplates.Create)
                {
                    SelectedBillToAddressID = BillToAddressIDList.FirstOrDefault();
                }
                else if (itemView == ViewItemTemplates.Edit)
                {
                    SelectedBillToAddressID = BillToAddressIDList.FirstOrDefault(t=>t.Value == Item.BillToAddressID);
                }
            }
        }
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<SalesOrderHeaderItemChangedMessage>(new SalesOrderHeaderItemChangedMessage(Item, itemView));
    }
}

