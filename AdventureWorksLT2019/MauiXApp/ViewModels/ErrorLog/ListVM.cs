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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ErrorLog;

public class ListVM : ListVMBase<ErrorLogAdvancedQuery, ErrorLogIdentifier, ErrorLogDataModel, ErrorLogService, ErrorLogItemChangedMessage>
{
    #region AdvancedQuery.Start ForeignKey SelectLists and DateTimeRanges
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

    // AdvancedQuery.DateTimeRange.0 ErrorTimeRange
    private NameValuePair m_SelectedErrorTimeRange;
    public NameValuePair SelectedErrorTimeRange
    {
        get => m_SelectedErrorTimeRange;
        set
        {
            SetProperty(ref m_SelectedErrorTimeRange, value);
            EditingQuery.ErrorTimeRange = value.Value;
            EditingQuery.ErrorTimeRangeLower = PreDefinedDateTimeRangesHelper.GetLowerBound(value.Value);
            EditingQuery.ErrorTimeRangeLower = PreDefinedDateTimeRangesHelper.GetUpperBound(value.Value);
        }
    }

    #endregion AdvancedQuery.End ForeignKey SelectLists and DateTimeRanges

    public ListVM(ErrorLogService dataService)
        : base(dataService, true)
    {
        // AdvancedQuery.Start DateTimeRanges
        // AdvancedQuery.DateTimeRangeList: DateTimeRangeListPast/DateTimeRangeListFuture/DateTimeRangeListAll
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        // AdvancedQuery.DateTimeRange.0 ErrorTimeRange
        SelectedErrorTimeRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ErrorTimeRange);
        /*
        SelectedErrorTimeRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ErrorTimeRange);
        SelectedErrorTimeRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ErrorTimeRange);
        */
        // AdvancedQuery.End DateTimeRanges

        // 8. Init LaunchErrorLogDetailsPopupCommand
        LaunchDetailsPopupCommand = LaunchViewCommandsHelper.GetLaunchErrorLogDetailsPopupCommand();
        // 12. Init LaunchErrorLogListOrderBysPopupCommand
        ListOrderBysLaunchCommand = LaunchViewCommandsHelper.GetLaunchErrorLogListOrderBysPopupCommand();

    }

    public override void RegisterItemDataChangedMessage()
    {
        RegisterItemDataChangedMessage<ListVM>(this);
    }
}

