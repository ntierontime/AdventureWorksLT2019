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
                EditingQuery.BillToAddressID = value.Value;
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
                EditingQuery.ShipToAddressID = value.Value;
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
                EditingQuery.CustomerID = value.Value;
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

    public ICommand BulkDeleteCommand { get; private set; }

    public ListVM(SalesOrderHeaderService dataService)
        : base(dataService)
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

    public override void RefreshMultiSelectCommandsCanExecute()
    {
        base.RefreshMultiSelectCommandsCanExecute();
        ((Command)BulkDeleteCommand).ChangeCanExecute();
    }

    public override void RegisterItemDataChangedMessage()
    {
        RegisterItemDataChangedMessage<ListVM>(this);
    }
}

