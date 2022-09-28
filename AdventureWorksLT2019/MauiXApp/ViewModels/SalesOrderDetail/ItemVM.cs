using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using Framework.MauiX.Helpers;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderDetail;

public class ItemVM : ItemVMBase<SalesOrderDetailIdentifier, SalesOrderDetailDataModel, SalesOrderDetailService, SalesOrderDetailItemChangedMessage, SalesOrderDetailItemRequestMessage>
{

    // ForeignKeys.1. SalesOrderIDList
    private List<NameValuePair<int>> m_SalesOrderIDList;
    public List<NameValuePair<int>> SalesOrderIDList
    {
        get => m_SalesOrderIDList;
        set => SetProperty(ref m_SalesOrderIDList, value);
    }

    private NameValuePair<int> m_SelectedSalesOrderID;
    public NameValuePair<int> SelectedSalesOrderID
    {
        get => m_SelectedSalesOrderID;
        set
        {
            SetProperty(ref m_SelectedSalesOrderID, value);
            Item.SalesOrderID = value.Value;
        }
    }

    // ForeignKeys.2. ProductIDList
    private List<NameValuePair<int>> m_ProductIDList;
    public List<NameValuePair<int>> ProductIDList
    {
        get => m_ProductIDList;
        set => SetProperty(ref m_ProductIDList, value);
    }

    private NameValuePair<int> m_SelectedProductID;
    public NameValuePair<int> SelectedProductID
    {
        get => m_SelectedProductID;
        set
        {
            SetProperty(ref m_SelectedProductID, value);
            Item.ProductID = value.Value;
        }
    }

    // ForeignKeys.3. ProductCategoryIDList
    private List<NameValuePair<int>> m_ProductCategoryIDList;
    public List<NameValuePair<int>> ProductCategoryIDList
    {
        get => m_ProductCategoryIDList;
        set => SetProperty(ref m_ProductCategoryIDList, value);
    }

    private NameValuePair<int> m_SelectedProductCategoryID;
    public NameValuePair<int> SelectedProductCategoryID
    {
        get => m_SelectedProductCategoryID;
        set
        {
            SetProperty(ref m_SelectedProductCategoryID, value);
            Item.ProductCategoryID = value.Value;
        }
    }

    // ForeignKeys.4. ProductCategory_ParentIDList
    private List<NameValuePair<int>> m_ProductCategory_ParentIDList;
    public List<NameValuePair<int>> ProductCategory_ParentIDList
    {
        get => m_ProductCategory_ParentIDList;
        set => SetProperty(ref m_ProductCategory_ParentIDList, value);
    }

    private NameValuePair<int> m_SelectedProductCategory_ParentID;
    public NameValuePair<int> SelectedProductCategory_ParentID
    {
        get => m_SelectedProductCategory_ParentID;
        set
        {
            SetProperty(ref m_SelectedProductCategory_ParentID, value);
            Item.ProductCategory_ParentID = value.Value;
        }
    }

    // ForeignKeys.5. ProductModelIDList
    private List<NameValuePair<int>> m_ProductModelIDList;
    public List<NameValuePair<int>> ProductModelIDList
    {
        get => m_ProductModelIDList;
        set => SetProperty(ref m_ProductModelIDList, value);
    }

    private NameValuePair<int> m_SelectedProductModelID;
    public NameValuePair<int> SelectedProductModelID
    {
        get => m_SelectedProductModelID;
        set
        {
            SetProperty(ref m_SelectedProductModelID, value);
            Item.ProductModelID = value.Value;
        }
    }

    // ForeignKeys.6. BillToIDList
    private List<NameValuePair<int>> m_BillToIDList;
    public List<NameValuePair<int>> BillToIDList
    {
        get => m_BillToIDList;
        set => SetProperty(ref m_BillToIDList, value);
    }

    private NameValuePair<int> m_SelectedBillToID;
    public NameValuePair<int> SelectedBillToID
    {
        get => m_SelectedBillToID;
        set
        {
            SetProperty(ref m_SelectedBillToID, value);
            Item.BillToID = value.Value;
        }
    }

    // ForeignKeys.7. CustomerIDList
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

    // ForeignKeys.8. ShipToIDList
    private List<NameValuePair<int>> m_ShipToIDList;
    public List<NameValuePair<int>> ShipToIDList
    {
        get => m_ShipToIDList;
        set => SetProperty(ref m_ShipToIDList, value);
    }

    private NameValuePair<int> m_SelectedShipToID;
    public NameValuePair<int> SelectedShipToID
    {
        get => m_SelectedShipToID;
        set
        {
            SetProperty(ref m_SelectedShipToID, value);
            Item.ShipToID = value.Value;
        }
    }
    public ItemVM(SalesOrderDetailService dataService)
        : base(dataService)
    {
    }

    protected override async Task LoadCodeListsIfAny(ViewItemTemplates itemView)
    {

        // ForeignKeys.4. ProductCategory_ParentIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetProductCategoryCodeList(new ProductCategoryAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ProductCategory_ParentIDList = new List<NameValuePair<int>>(response.ResponseBody);
                if (itemView == ViewItemTemplates.Create)
                {
                    SelectedProductCategory_ParentID = ProductCategory_ParentIDList.FirstOrDefault();
                }
                else if (itemView == ViewItemTemplates.Edit)
                {
                    SelectedProductCategory_ParentID = ProductCategory_ParentIDList.FirstOrDefault(t=>t.Value == Item.ProductCategory_ParentID);
                }
            }
        }

        // ForeignKeys.5. ProductModelIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetProductModelCodeList(new ProductModelAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ProductModelIDList = new List<NameValuePair<int>>(response.ResponseBody);
                if (itemView == ViewItemTemplates.Create)
                {
                    SelectedProductModelID = ProductModelIDList.FirstOrDefault();
                }
                else if (itemView == ViewItemTemplates.Edit)
                {
                    SelectedProductModelID = ProductModelIDList.FirstOrDefault(t=>t.Value == Item.ProductModelID);
                }
            }
        }

        // ForeignKeys.6. BillToIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetAddressCodeList(new AddressAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                BillToIDList = new List<NameValuePair<int>>(response.ResponseBody);
                if (itemView == ViewItemTemplates.Create)
                {
                    SelectedBillToID = BillToIDList.FirstOrDefault();
                }
                else if (itemView == ViewItemTemplates.Edit)
                {
                    SelectedBillToID = BillToIDList.FirstOrDefault(t=>t.Value == Item.BillToID);
                }
            }
        }

        // ForeignKeys.7. CustomerIDList
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

        // ForeignKeys.8. ShipToIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetAddressCodeList(new AddressAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ShipToIDList = new List<NameValuePair<int>>(response.ResponseBody);
                if (itemView == ViewItemTemplates.Create)
                {
                    SelectedShipToID = ShipToIDList.FirstOrDefault();
                }
                else if (itemView == ViewItemTemplates.Edit)
                {
                    SelectedShipToID = ShipToIDList.FirstOrDefault(t=>t.Value == Item.ShipToID);
                }
            }
        }
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<SalesOrderDetailItemChangedMessage>(new SalesOrderDetailItemChangedMessage(Item, itemView));
    }
}

