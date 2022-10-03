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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ProductModel;

public class ListVM : ListVMBase<ProductModelAdvancedQuery, ProductModelIdentifier, ProductModelDataModel, ProductModelService, ProductModelItemChangedMessage>
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

    // AdvancedQuery.DateTimeRange.0 ModifiedDateRange
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

    public ListVM(ProductModelService dataService)
        : base(dataService)
    {
        // AdvancedQuery.Start DateTimeRanges
        // AdvancedQuery.DateTimeRangeList: DateTimeRangeListPast/DateTimeRangeListFuture/DateTimeRangeListAll
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        // AdvancedQuery.DateTimeRange.0 ModifiedDateRange
        SelectedModifiedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        /*
        SelectedModifiedDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        SelectedModifiedDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        */
        // AdvancedQuery.End DateTimeRanges

        // 1. Init LaunchProductModelCreatePageCommand
        LaunchCreatePageCommand = LaunchViewCommandsHelper.GetLaunchProductModelCreatePageCommand(AppShellRoutes.ProductModelListPage);
        // 2. Init LaunchProductModelDeletePageCommand
        LaunchDeletePageCommand = LaunchViewCommandsHelper.GetLaunchProductModelDeletePageCommand(AppShellRoutes.ProductModelListPage);
        // 3. Init LaunchProductModelDetailsPageCommand
        LaunchDetailsPageCommand = LaunchViewCommandsHelper.GetLaunchProductModelDetailsPageCommand(AppShellRoutes.ProductModelListPage);
        // 4. Init LaunchProductModelEditPageCommand
        LaunchEditPageCommand = LaunchViewCommandsHelper.GetLaunchProductModelEditPageCommand(AppShellRoutes.ProductModelListPage);
        // 5. Init LaunchProductModelCreatePopupCommand
        LaunchCreatePopupCommand = LaunchViewCommandsHelper.GetLaunchProductModelCreatePopupCommand();
        // 6. Init LaunchProductModelDeletePopupCommand
        LaunchDeletePopupCommand = LaunchViewCommandsHelper.GetLaunchProductModelDeletePopupCommand();
        // 7. Init LaunchProductModelDetailsPopupCommand
        LaunchDetailsPopupCommand = LaunchViewCommandsHelper.GetLaunchProductModelDetailsPopupCommand();
        // 8. Init LaunchProductModelEditPopupCommand
        LaunchEditPopupCommand = LaunchViewCommandsHelper.GetLaunchProductModelEditPopupCommand();
        // 9. Init LaunchProductModelAdvancedSearchPopupCommand
        AdvancedSearchLaunchCommand = LaunchViewCommandsHelper.GetLaunchProductModelAdvancedSearchPopupCommand();
        // 10. Init LaunchProductModelListQuickActionsPopupCommand
        ListBulkActionsLaunchCommand = LaunchViewCommandsHelper.GetLaunchProductModelListQuickActionsPopupCommand();
        // 11. Init LaunchProductModelListOrderBysPopupCommand
        ListOrderBysLaunchCommand = LaunchViewCommandsHelper.GetLaunchProductModelListOrderBysPopupCommand();

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

    public override void RegisterItemDataChangedMessage()
    {
        RegisterItemDataChangedMessage<ListVM>(this);
    }
}

