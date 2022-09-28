using Framework.MauiX.Helpers;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.Helpers;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.Models;
using Framework.MauiX.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderDetail;

public class ListVM : ListVMBase<SalesOrderDetailAdvancedQuery, SalesOrderDetailIdentifier, SalesOrderDetailDataModel, SalesOrderDetailService, SalesOrderDetailItemChangedMessage, SalesOrderDetailItemRequestMessage>
{
    // AdvancedQuery.Start

    // ForeignKeys.1. ProductIDList
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
            EditingQuery.ProductID = value.Value;
        }
    }

    // ForeignKeys.2. ProductCategoryIDList
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
            EditingQuery.ProductCategoryID = value.Value;
        }
    }

    // ForeignKeys.3. ProductCategory_ParentIDList
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
            EditingQuery.ProductCategory_ParentID = value.Value;
        }
    }

    // ForeignKeys.4. ProductModelIDList
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
            EditingQuery.ProductModelID = value.Value;
        }
    }

    // ForeignKeys.5. SalesOrderIDList
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
            EditingQuery.SalesOrderID = value.Value;
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
            EditingQuery.BillToID = value.Value;
        }
    }

    // ForeignKeys.7. ShipToIDList
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
            EditingQuery.ShipToID = value.Value;
        }
    }

    // ForeignKeys.8. CustomerIDList
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
            EditingQuery.CustomerID = value.Value;
        }
    }

    private List<NameValuePair> m_DateTimeRangeListPast;
    public List<NameValuePair> DateTimeRangeListPast
    {
        get => m_DateTimeRangeListPast;
        set => SetProperty(ref m_DateTimeRangeListPast, value);
    }
    /*
    private List<NameValuePair> m_DateTimeRangeListFuture;
    public List<NameValuePair> DateTimeRangeListFuture
    {
        get => m_DateTimeRangeListFuture;
        set => SetProperty(ref m_DateTimeRangeListFuture, value);
    }
    private List<NameValuePair> m_DateTimeRangeListAll;
    public List<NameValuePair> DateTimeRangeListAll
    {
        get => m_DateTimeRangeListAll;
        set => SetProperty(ref m_DateTimeRangeListAll, value);
    }
    */

    private NameValuePair m_SelectedModifiedDateRange;
    public NameValuePair SelectedModifiedDateRange
    {
        get => m_SelectedModifiedDateRange;
        set
        {
            SetProperty(ref m_SelectedModifiedDateRange, value);
            EditingQuery.ModifiedDateRange = value.Value;
            EditingQuery.ModifiedDateRangeLower = PreDefinedDateTimeRangesHelper.GetLowerBound(value.Value);
            EditingQuery.ModifiedDateRangeLower = PreDefinedDateTimeRangesHelper.GetUpperBound(value.Value);
        }
    }
    // AdvancedQuery.End

    public ICommand BulkDeleteCommand { get; private set; }

    public ListVM(SalesOrderDetailService dataService)
        : base(dataService)
    {
        // AdvancedQuery.Start
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        SelectedModifiedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        /*
        SelectedModifiedDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        SelectedModifiedDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        */
        // AdvancedQuery.End

        BulkDeleteCommand = new Command(
            async () =>
            {
                // TODO: can add popup to confirm, and popup to show status OK/Failed
                var response = await _dataService.BulkDelete(SelectedItems.Select(t => t.GetIdentifier()).ToList());
                if (response.Status == System.Net.HttpStatusCode.OK)
                {
                    foreach (var item in SelectedItems)
                    {
                        Result.Remove(item);
                    }
                    SelectedItems.Clear();
                    RefreshMultiSelectCommandsCanExecute();
                }
            },
            () => EnableMultiSelectCommands()
        );
    }

    protected override async Task LoadCodeListsIfAny()
    {

        // // ForeignKeys.3. ProductCategory_ParentIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetProductCategoryCodeList(new ProductCategoryAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ProductCategory_ParentIDList = new List<NameValuePair<int>>(response.ResponseBody);
                SelectedProductCategory_ParentID = ProductCategory_ParentIDList.FirstOrDefault(t=>t.Value == EditingQuery.ProductCategory_ParentID);
            }
        }

        // // ForeignKeys.4. ProductModelIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetProductModelCodeList(new ProductModelAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ProductModelIDList = new List<NameValuePair<int>>(response.ResponseBody);
                SelectedProductModelID = ProductModelIDList.FirstOrDefault(t=>t.Value == EditingQuery.ProductModelID);
            }
        }

        // // ForeignKeys.6. BillToIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetAddressCodeList(new AddressAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                BillToIDList = new List<NameValuePair<int>>(response.ResponseBody);
                SelectedBillToID = BillToIDList.FirstOrDefault(t=>t.Value == EditingQuery.BillToID);
            }
        }

        // // ForeignKeys.7. ShipToIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetAddressCodeList(new AddressAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ShipToIDList = new List<NameValuePair<int>>(response.ResponseBody);
                SelectedShipToID = ShipToIDList.FirstOrDefault(t=>t.Value == EditingQuery.ShipToID);
            }
        }

        // // ForeignKeys.8. CustomerIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetCustomerCodeList(new CustomerAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                CustomerIDList = new List<NameValuePair<int>>(response.ResponseBody);
                SelectedCustomerID = CustomerIDList.FirstOrDefault(t=>t.Value == EditingQuery.CustomerID);
            }
        }
    }

    public override void RefreshMultiSelectCommandsCanExecute()
    {
        base.RefreshMultiSelectCommandsCanExecute();
        ((Command)BulkDeleteCommand).ChangeCanExecute();
    }

    public override void RegisterRequestSelectedItemMessage()
    {
        RegisterRequestSelectedItemMessage<ListVM>(this);
    }

    public override void RegisterItemDataChangedMessage()
    {
        RegisterItemDataChangedMessage<ListVM>(this);
    }
}

