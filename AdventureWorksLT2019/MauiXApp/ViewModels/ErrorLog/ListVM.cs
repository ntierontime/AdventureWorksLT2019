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

public class ListVM : ListVMBase<ErrorLogAdvancedQuery, ErrorLogIdentifier, ErrorLogDataModel, ErrorLogService, ErrorLogItemChangedMessage, ErrorLogItemRequestMessage>
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
    // AdvancedQuery.End

    public ICommand BulkDeleteCommand { get; private set; }

    public ListVM(ErrorLogService dataService)
        : base(dataService)
    {
        // AdvancedQuery.Start
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        SelectedErrorTimeRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ErrorTimeRange);
        /*
        SelectedErrorTimeRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ErrorTimeRange);
        SelectedErrorTimeRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ErrorTimeRange);
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

