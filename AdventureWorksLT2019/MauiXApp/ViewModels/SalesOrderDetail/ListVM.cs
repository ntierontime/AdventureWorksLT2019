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

public class ListVM : ListVMBase<SalesOrderDetailAdvancedQuery, SalesOrderDetailIdentifier, SalesOrderDetailDataModel, SalesOrderDetailService, SalesOrderDetailItemChangedMessage>
{
    #region AdvancedQuery.Start ForeignKey SelectLists and DateTimeRanges

    // AdvancedQuery.ForeignKeys.1. ProductIDList
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
            if (value != null)
            {
                SetProperty(ref m_SelectedProductID, value);
                if(EditingQuery != null)
                    EditingQuery.ProductID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.ProductID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.2. ProductCategoryIDList
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
            if (value != null)
            {
                SetProperty(ref m_SelectedProductCategoryID, value);
                if(EditingQuery != null)
                    EditingQuery.ProductCategoryID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.ProductCategoryID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.3. ProductCategory_ParentIDList
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
            if (value != null)
            {
                SetProperty(ref m_SelectedProductCategory_ParentID, value);
                if(EditingQuery != null)
                    EditingQuery.ProductCategory_ParentID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.ProductCategory_ParentID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.4. ProductModelIDList
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
            if (value != null)
            {
                SetProperty(ref m_SelectedProductModelID, value);
                if(EditingQuery != null)
                    EditingQuery.ProductModelID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.ProductModelID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.5. SalesOrderIDList
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
            if (value != null)
            {
                SetProperty(ref m_SelectedSalesOrderID, value);
                if(EditingQuery != null)
                    EditingQuery.SalesOrderID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.SalesOrderID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.6. BillToIDList
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
            if (value != null)
            {
                SetProperty(ref m_SelectedBillToID, value);
                if(EditingQuery != null)
                    EditingQuery.BillToID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.BillToID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.7. ShipToIDList
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
            if (value != null)
            {
                SetProperty(ref m_SelectedShipToID, value);
                if(EditingQuery != null)
                    EditingQuery.ShipToID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.ShipToID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.8. CustomerIDList
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
                if(EditingQuery != null)
                    EditingQuery.CustomerID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.CustomerID = value.Value;
            }
        }
    }

    // AdvancedQuery.DateTimeRangeList: DateTimeRangeListPast/DateTimeRangeListFuture/DateTimeRangeListAll
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

    // AdvancedQuery.DateTimeRange.8 ModifiedDateRange
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

    #endregion AdvancedQuery.End ForeignKey SelectLists and DateTimeRanges

    public ListVM(SalesOrderDetailService dataService)
        : base(dataService, true)
    {
        // AdvancedQuery.Start DateTimeRanges
        // AdvancedQuery.DateTimeRangeList: DateTimeRangeListPast/DateTimeRangeListFuture/DateTimeRangeListAll
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        // AdvancedQuery.DateTimeRange.8 ModifiedDateRange
        SelectedModifiedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        /*
        SelectedModifiedDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        SelectedModifiedDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        */
        // AdvancedQuery.End DateTimeRanges

        // 6. Init LaunchSalesOrderDetailCreatePopupCommand
        LaunchCreatePopupCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderDetailCreatePopupCommand();
        // 8. Init LaunchSalesOrderDetailDetailsPopupCommand
        LaunchDetailsPopupCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderDetailDetailsPopupCommand();
        // 9. Init LaunchSalesOrderDetailEditPopupCommand
        LaunchEditPopupCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderDetailEditPopupCommand();
        // 12. Init LaunchSalesOrderDetailListOrderBysPopupCommand
        ListOrderBysLaunchCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderDetailListOrderBysPopupCommand();

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

    public override void RegisterItemDataChangedMessage()
    {
        RegisterItemDataChangedMessage<ListVM>(this);
    }
}

