using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using Framework.MauiX.Helpers;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.CustomerAddress;

public class ItemVM : ItemVMBase<CustomerAddressIdentifier, CustomerAddressDataModel, CustomerAddressService, CustomerAddressItemChangedMessage, CustomerAddressItemRequestMessage>
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

    // ForeignKeys.2. AddressIDList
    private List<NameValuePair<int>> m_AddressIDList;
    public List<NameValuePair<int>> AddressIDList
    {
        get => m_AddressIDList;
        set => SetProperty(ref m_AddressIDList, value);
    }

    private NameValuePair<int> m_SelectedAddressID;
    public NameValuePair<int> SelectedAddressID
    {
        get => m_SelectedAddressID;
        set
        {
            SetProperty(ref m_SelectedAddressID, value);
            Item.AddressID = value.Value;
        }
    }
    public ItemVM(CustomerAddressService dataService)
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

        // ForeignKeys.2. AddressIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetAddressCodeList(new AddressAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                AddressIDList = new List<NameValuePair<int>>(response.ResponseBody);
                if (itemView == ViewItemTemplates.Create)
                {
                    SelectedAddressID = AddressIDList.FirstOrDefault();
                }
                else if (itemView == ViewItemTemplates.Edit)
                {
                    SelectedAddressID = AddressIDList.FirstOrDefault(t=>t.Value == Item.AddressID);
                }
            }
        }
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<CustomerAddressItemChangedMessage>(new CustomerAddressItemChangedMessage(Item, itemView));
    }
}

