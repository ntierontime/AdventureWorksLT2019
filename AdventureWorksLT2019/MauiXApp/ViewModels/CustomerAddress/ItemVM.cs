using AdventureWorksLT2019.MauiXApp.Common.Helpers;
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

public class ItemVM : ItemVMBase<CustomerAddressIdentifier, CustomerAddressDataModel, CustomerAddressService, CustomerAddressItemChangedMessage>
{
    #region Foreign Key SelectLists

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
            if (value != null)
            {
                SetProperty(ref m_SelectedCustomerID, value);
                Item.CustomerID = value.Value;
            }
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
            if (value != null)
            {
                SetProperty(ref m_SelectedAddressID, value);
                Item.AddressID = value.Value;
            }
        }
    }
    #endregion Foreign Key SelectLists

    public ICommand LaunchCustomerFKItemViewCommand { get; private set; }

    public ICommand LaunchAddressFKItemViewCommand { get; private set; }
    public ItemVM(CustomerAddressService dataService)
        : base(dataService)
    {

        LaunchCustomerFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchCustomerEditPopupCommand();

        LaunchAddressFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchAddressEditPopupCommand();

        WeakReferenceMessenger.Default.Register<ItemVM, CustomerAddressIdentifierMessage>(
           this, async (r, m) =>
        {
            if (m.ItemView == ViewItemTemplates.Dashboard)
                return;

            ItemView = m.ItemView;
            ReturnPath = m.ReturnPath;

            if (m.ItemView == ViewItemTemplates.Create)
            {
                Item = _dataService.GetDefault();
            }
            else
            {
                var response = await _dataService.Get(m.Value);

                if (response.Status == System.Net.HttpStatusCode.OK)
                {
                    Item = response.ResponseBody;
                }
            }
            if (m.ItemView == ViewItemTemplates.Create || m.ItemView == ViewItemTemplates.Edit)
            {
                await LoadCodeListsIfAny(m.ItemView);
            }
        });
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

