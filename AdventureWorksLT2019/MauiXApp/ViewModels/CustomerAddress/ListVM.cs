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
                EditingQuery.AddressID = value.Value;
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

    public ICommand BulkDeleteCommand { get; private set; }

    public ListVM(CustomerAddressService dataService)
        : base(dataService)
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

