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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader;

public class ListVM : ListVMBase<SalesOrderHeaderAdvancedQuery, SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel, SalesOrderHeaderService, SalesOrderHeaderItemChangedMessage>
{
    #region AdvancedQuery.Start ForeignKey SelectLists and DateTimeRanges

    // AdvancedQuery.ForeignKeys.1. BillToAddressIDList
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
            if (value != null)
            {
                SetProperty(ref m_SelectedBillToAddressID, value);
                if(EditingQuery != null)
                    EditingQuery.BillToAddressID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.BillToAddressID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.2. ShipToAddressIDList
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
            if (value != null)
            {
                SetProperty(ref m_SelectedShipToAddressID, value);
                if(EditingQuery != null)
                    EditingQuery.ShipToAddressID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.ShipToAddressID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.3. CustomerIDList
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

    // AdvancedQuery.DateTimeRange.4 OrderDateRange
    private NameValuePair m_SelectedOrderDateRange;
    public NameValuePair SelectedOrderDateRange
    {
        get => m_SelectedOrderDateRange;
        set
        {
            SetProperty(ref m_SelectedOrderDateRange, value);
            EditingQuery.OrderDateRange = value.Value;
            EditingQuery.OrderDateRangeLower = PreDefinedDateTimeRangesHelper.GetLowerBound(value.Value);
            EditingQuery.OrderDateRangeLower = PreDefinedDateTimeRangesHelper.GetUpperBound(value.Value);
        }
    }

    // AdvancedQuery.DateTimeRange.5 DueDateRange
    private NameValuePair m_SelectedDueDateRange;
    public NameValuePair SelectedDueDateRange
    {
        get => m_SelectedDueDateRange;
        set
        {
            SetProperty(ref m_SelectedDueDateRange, value);
            EditingQuery.DueDateRange = value.Value;
            EditingQuery.DueDateRangeLower = PreDefinedDateTimeRangesHelper.GetLowerBound(value.Value);
            EditingQuery.DueDateRangeLower = PreDefinedDateTimeRangesHelper.GetUpperBound(value.Value);
        }
    }

    // AdvancedQuery.DateTimeRange.6 ShipDateRange
    private NameValuePair m_SelectedShipDateRange;
    public NameValuePair SelectedShipDateRange
    {
        get => m_SelectedShipDateRange;
        set
        {
            SetProperty(ref m_SelectedShipDateRange, value);
            EditingQuery.ShipDateRange = value.Value;
            EditingQuery.ShipDateRangeLower = PreDefinedDateTimeRangesHelper.GetLowerBound(value.Value);
            EditingQuery.ShipDateRangeLower = PreDefinedDateTimeRangesHelper.GetUpperBound(value.Value);
        }
    }

    // AdvancedQuery.DateTimeRange.7 ModifiedDateRange
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

    public ListVM(SalesOrderHeaderService dataService)
        : base(dataService, true)
    {
        // AdvancedQuery.Start DateTimeRanges
        // AdvancedQuery.DateTimeRangeList: DateTimeRangeListPast/DateTimeRangeListFuture/DateTimeRangeListAll
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        // AdvancedQuery.DateTimeRange.4 OrderDateRange
        SelectedOrderDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.OrderDateRange);
        /*
        SelectedOrderDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.OrderDateRange);
        SelectedOrderDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.OrderDateRange);
        */

        // AdvancedQuery.DateTimeRange.5 DueDateRange
        SelectedDueDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.DueDateRange);
        /*
        SelectedDueDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.DueDateRange);
        SelectedDueDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.DueDateRange);
        */

        // AdvancedQuery.DateTimeRange.6 ShipDateRange
        SelectedShipDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ShipDateRange);
        /*
        SelectedShipDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ShipDateRange);
        SelectedShipDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ShipDateRange);
        */

        // AdvancedQuery.DateTimeRange.7 ModifiedDateRange
        SelectedModifiedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        /*
        SelectedModifiedDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        SelectedModifiedDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        */
        // AdvancedQuery.End DateTimeRanges

        // 1. Init LaunchSalesOrderHeaderCreatePageCommand
        LaunchCreatePageCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderCreatePageCommand(AppShellRoutes.SalesOrderHeaderListPage);
        // 2. Init LaunchSalesOrderHeaderDeletePageCommand
        LaunchDeletePageCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderDeletePageCommand(AppShellRoutes.SalesOrderHeaderListPage);
        // 3. Init LaunchSalesOrderHeaderDetailsPageCommand
        LaunchDetailsPageCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderDetailsPageCommand(AppShellRoutes.SalesOrderHeaderListPage);
        // 4. Init LaunchSalesOrderHeaderEditPageCommand
        LaunchEditPageCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderEditPageCommand(AppShellRoutes.SalesOrderHeaderListPage);
        // 5. Init LaunchSalesOrderHeaderDashboardPageCommand
        LaunchDashboardPageCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderDashboardPageCommand(AppShellRoutes.SalesOrderHeaderListPage);
        // 6. Init LaunchSalesOrderHeaderCreatePopupCommand
        LaunchCreatePopupCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderCreatePopupCommand();
        // 7. Init LaunchSalesOrderHeaderDeletePopupCommand
        LaunchDeletePopupCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderDeletePopupCommand();
        // 8. Init LaunchSalesOrderHeaderDetailsPopupCommand
        LaunchDetailsPopupCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderDetailsPopupCommand();
        // 9. Init LaunchSalesOrderHeaderEditPopupCommand
        LaunchEditPopupCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderEditPopupCommand();
        // 10. Init LaunchSalesOrderHeaderAdvancedSearchPopupCommand
        AdvancedSearchLaunchCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderAdvancedSearchPopupCommand();
        // 11. Init LaunchSalesOrderHeaderListBulkActionsPopupCommand
        ListBulkActionsLaunchCommand = new Command<string>(
            (currentBulkActionName) =>
            {
                BulkUpdateItem = _dataService.GetDefault();
                CurrentBulkActionName = currentBulkActionName;
                var launchCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderListBulkActionsPopupCommand();
                launchCommand.Execute(null);
            },
            (currentBulkActionName) => EnableMultiSelectCommands()
            );
        // 12. Init LaunchSalesOrderHeaderListOrderBysPopupCommand
        ListOrderBysLaunchCommand = LaunchViewCommandsHelper.GetLaunchSalesOrderHeaderListOrderBysPopupCommand();

    }

    protected override async Task LoadCodeListsIfAny()
    {

        // // ForeignKeys.1. BillToAddressIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetAddressCodeList(new AddressAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                BillToAddressIDList = new List<NameValuePair<int>>(response.ResponseBody);
                SelectedBillToAddressID = BillToAddressIDList.FirstOrDefault(t=>t.Value == EditingQuery.BillToAddressID);
            }
        }

        // // ForeignKeys.2. ShipToAddressIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetAddressCodeList(new AddressAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ShipToAddressIDList = new List<NameValuePair<int>>(response.ResponseBody);
                SelectedShipToAddressID = ShipToAddressIDList.FirstOrDefault(t=>t.Value == EditingQuery.ShipToAddressID);
            }
        }

        // // ForeignKeys.3. CustomerIDList
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

    protected override void CopyBulkUpdateResult(SalesOrderHeaderDataModel source, SalesOrderHeaderDataModel destination)
    {

        if(CurrentBulkActionName == "OnlineOrderFlag")
        {
            destination.OnlineOrderFlag = source.OnlineOrderFlag;
            return;
        }
    }

    public override void RegisterItemDataChangedMessage()
    {
        RegisterItemDataChangedMessage<ListVM>(this);
    }
}

