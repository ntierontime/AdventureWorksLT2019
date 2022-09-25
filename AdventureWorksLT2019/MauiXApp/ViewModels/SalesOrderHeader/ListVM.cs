using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.Helpers;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.Models;
using Framework.MauiX.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader;

public class ListVM : ListVMBase<SalesOrderHeaderAdvancedQuery, SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel, SalesOrderHeaderService, SalesOrderHeaderItemChangedMessage, SalesOrderHeaderItemRequestMessage>
{
    // AdvancedQuery.Start
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

    public ListVM(SalesOrderHeaderService dataService)
        : base(dataService)
    {
        // AdvancedQuery.Start
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        SelectedOrderDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.OrderDateRange);
        /*
        SelectedOrderDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.OrderDateRange);
        SelectedOrderDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.OrderDateRange);
        */

        SelectedDueDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.DueDateRange);
        /*
        SelectedDueDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.DueDateRange);
        SelectedDueDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.DueDateRange);
        */

        SelectedShipDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ShipDateRange);
        /*
        SelectedShipDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ShipDateRange);
        SelectedShipDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ShipDateRange);
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

