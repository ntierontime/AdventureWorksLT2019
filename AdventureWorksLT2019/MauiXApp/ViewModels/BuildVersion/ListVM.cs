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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.BuildVersion;

public class ListVM : ListVMBase<BuildVersionAdvancedQuery, BuildVersionIdentifier, BuildVersionDataModel, BuildVersionService, BuildVersionItemChangedMessage>
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

    // AdvancedQuery.DateTimeRange.0 VersionDateRange
    private NameValuePair m_SelectedVersionDateRange;
    public NameValuePair SelectedVersionDateRange
    {
        get => m_SelectedVersionDateRange;
        set
        {
            SetProperty(ref m_SelectedVersionDateRange, value);
            EditingQuery.VersionDateRange = value.Value;
            EditingQuery.VersionDateRangeLower = PreDefinedDateTimeRangesHelper.GetLowerBound(value.Value);
            EditingQuery.VersionDateRangeLower = PreDefinedDateTimeRangesHelper.GetUpperBound(value.Value);
        }
    }

    // AdvancedQuery.DateTimeRange.1 ModifiedDateRange
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

    public ListVM(BuildVersionService dataService)
        : base(dataService, true)
    {
        // AdvancedQuery.Start DateTimeRanges
        // AdvancedQuery.DateTimeRangeList: DateTimeRangeListPast/DateTimeRangeListFuture/DateTimeRangeListAll
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        // AdvancedQuery.DateTimeRange.0 VersionDateRange
        SelectedVersionDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.VersionDateRange);
        /*
        SelectedVersionDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.VersionDateRange);
        SelectedVersionDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.VersionDateRange);
        */

        // AdvancedQuery.DateTimeRange.1 ModifiedDateRange
        SelectedModifiedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        /*
        SelectedModifiedDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        SelectedModifiedDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        */
        // AdvancedQuery.End DateTimeRanges

        // 1. Init LaunchBuildVersionCreatePageCommand
        LaunchCreatePageCommand = LaunchViewCommandsHelper.GetLaunchBuildVersionCreatePageCommand(AppShellRoutes.BuildVersionListPage);
        // 2. Init LaunchBuildVersionDeletePageCommand
        LaunchDeletePageCommand = LaunchViewCommandsHelper.GetLaunchBuildVersionDeletePageCommand(AppShellRoutes.BuildVersionListPage);
        // 3. Init LaunchBuildVersionDetailsPageCommand
        LaunchDetailsPageCommand = LaunchViewCommandsHelper.GetLaunchBuildVersionDetailsPageCommand(AppShellRoutes.BuildVersionListPage);
        // 4. Init LaunchBuildVersionEditPageCommand
        LaunchEditPageCommand = LaunchViewCommandsHelper.GetLaunchBuildVersionEditPageCommand(AppShellRoutes.BuildVersionListPage);
        // 5. Init LaunchBuildVersionCreatePopupCommand
        LaunchCreatePopupCommand = LaunchViewCommandsHelper.GetLaunchBuildVersionCreatePopupCommand();
        // 6. Init LaunchBuildVersionDeletePopupCommand
        LaunchDeletePopupCommand = LaunchViewCommandsHelper.GetLaunchBuildVersionDeletePopupCommand();
        // 7. Init LaunchBuildVersionDetailsPopupCommand
        LaunchDetailsPopupCommand = LaunchViewCommandsHelper.GetLaunchBuildVersionDetailsPopupCommand();
        // 8. Init LaunchBuildVersionEditPopupCommand
        LaunchEditPopupCommand = LaunchViewCommandsHelper.GetLaunchBuildVersionEditPopupCommand();
        // 9. Init LaunchBuildVersionAdvancedSearchPopupCommand
        AdvancedSearchLaunchCommand = LaunchViewCommandsHelper.GetLaunchBuildVersionAdvancedSearchPopupCommand();
        // 10. Init LaunchBuildVersionListBulkActionsPopupCommand
        ListBulkActionsLaunchCommand = new Command<string>(
            (currentBulkActionName) =>
            {
                BulkUpdateItem = _dataService.GetDefault();
                CurrentBulkActionName = currentBulkActionName;
                var launchCommand = LaunchViewCommandsHelper.GetLaunchBuildVersionListBulkActionsPopupCommand();
                launchCommand.Execute(null);
            },
            (currentBulkActionName) => EnableMultiSelectCommands()
            );
        // 11. Init LaunchBuildVersionListOrderBysPopupCommand
        ListOrderBysLaunchCommand = LaunchViewCommandsHelper.GetLaunchBuildVersionListOrderBysPopupCommand();

    }

    public override void RegisterItemDataChangedMessage()
    {
        RegisterItemDataChangedMessage<ListVM>(this);
    }
}

