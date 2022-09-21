using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.Helpers;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.Models;
using Framework.MauiX.ViewModels;
using CommunityToolkit.Mvvm.Messaging;

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

