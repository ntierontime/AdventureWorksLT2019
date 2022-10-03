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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.CustomerAddress;

public class ListVM : ListVMBase<CustomerAddressAdvancedQuery, CustomerAddressIdentifier, CustomerAddressDataModel, CustomerAddressService, CustomerAddressItemChangedMessage>
{
    #region AdvancedQuery.Start ForeignKey SelectLists and DateTimeRanges

    // AdvancedQuery.ForeignKeys.1. AddressIDList
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
                if(EditingQuery != null)
                    EditingQuery.AddressID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.AddressID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.2. CustomerIDList
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

    // AdvancedQuery.DateTimeRange.2 ModifiedDateRange
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

    public ListVM(CustomerAddressService dataService)
        : base(dataService, true)
    {
        // AdvancedQuery.Start DateTimeRanges
        // AdvancedQuery.DateTimeRangeList: DateTimeRangeListPast/DateTimeRangeListFuture/DateTimeRangeListAll
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        // AdvancedQuery.DateTimeRange.2 ModifiedDateRange
        SelectedModifiedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        /*
        SelectedModifiedDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        SelectedModifiedDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        */
        // AdvancedQuery.End DateTimeRanges

        // 1. Init LaunchCustomerAddressCreatePageCommand
        LaunchCreatePageCommand = LaunchViewCommandsHelper.GetLaunchCustomerAddressCreatePageCommand(AppShellRoutes.CustomerAddressListPage);
        // 2. Init LaunchCustomerAddressDeletePageCommand
        LaunchDeletePageCommand = LaunchViewCommandsHelper.GetLaunchCustomerAddressDeletePageCommand(AppShellRoutes.CustomerAddressListPage);
        // 3. Init LaunchCustomerAddressDetailsPageCommand
        LaunchDetailsPageCommand = LaunchViewCommandsHelper.GetLaunchCustomerAddressDetailsPageCommand(AppShellRoutes.CustomerAddressListPage);
        // 4. Init LaunchCustomerAddressEditPageCommand
        LaunchEditPageCommand = LaunchViewCommandsHelper.GetLaunchCustomerAddressEditPageCommand(AppShellRoutes.CustomerAddressListPage);
        // 5. Init LaunchCustomerAddressCreatePopupCommand
        LaunchCreatePopupCommand = LaunchViewCommandsHelper.GetLaunchCustomerAddressCreatePopupCommand();
        // 6. Init LaunchCustomerAddressDeletePopupCommand
        LaunchDeletePopupCommand = LaunchViewCommandsHelper.GetLaunchCustomerAddressDeletePopupCommand();
        // 7. Init LaunchCustomerAddressDetailsPopupCommand
        LaunchDetailsPopupCommand = LaunchViewCommandsHelper.GetLaunchCustomerAddressDetailsPopupCommand();
        // 8. Init LaunchCustomerAddressEditPopupCommand
        LaunchEditPopupCommand = LaunchViewCommandsHelper.GetLaunchCustomerAddressEditPopupCommand();
        // 9. Init LaunchCustomerAddressAdvancedSearchPopupCommand
        AdvancedSearchLaunchCommand = LaunchViewCommandsHelper.GetLaunchCustomerAddressAdvancedSearchPopupCommand();
        // 10. Init LaunchCustomerAddressListBulkActionsPopupCommand
        ListBulkActionsLaunchCommand = new Command<string>(
            (currentBulkActionName) =>
            {
                BulkUpdateItem = _dataService.GetDefault();
                CurrentBulkActionName = currentBulkActionName;
                var launchCommand = LaunchViewCommandsHelper.GetLaunchCustomerAddressListBulkActionsPopupCommand();
                launchCommand.Execute(null);
            },
            (currentBulkActionName) => EnableMultiSelectCommands()
            );
        // 11. Init LaunchCustomerAddressListOrderBysPopupCommand
        ListOrderBysLaunchCommand = LaunchViewCommandsHelper.GetLaunchCustomerAddressListOrderBysPopupCommand();

    }

    protected override async Task LoadCodeListsIfAny()
    {

        // // ForeignKeys.1. AddressIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetAddressCodeList(new AddressAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                AddressIDList = new List<NameValuePair<int>>(response.ResponseBody);
                SelectedAddressID = AddressIDList.FirstOrDefault(t=>t.Value == EditingQuery.AddressID);
            }
        }

        // // ForeignKeys.2. CustomerIDList
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

