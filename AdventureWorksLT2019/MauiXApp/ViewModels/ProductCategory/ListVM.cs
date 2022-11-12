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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ProductCategory;

public class ListVM : ListVMBase<ProductCategoryAdvancedQuery, ProductCategoryIdentifier, ProductCategoryDataModel, ProductCategoryService, ProductCategoryItemChangedMessage>
{
    #region AdvancedQuery.Start ForeignKey SelectLists and DateTimeRanges

    // AdvancedQuery.ForeignKeys.1. ParentProductCategoryIDList
    private List<NameValuePair<int>> m_ParentProductCategoryIDList;
    public List<NameValuePair<int>> ParentProductCategoryIDList
    {
        get => m_ParentProductCategoryIDList;
        set => SetProperty(ref m_ParentProductCategoryIDList, value);
    }

    private NameValuePair<int> m_SelectedParentProductCategoryID;
    public NameValuePair<int> SelectedParentProductCategoryID
    {
        get => m_SelectedParentProductCategoryID;
        set
        {
            if (value != null)
            {
                SetProperty(ref m_SelectedParentProductCategoryID, value);
                if(EditingQuery != null)
                    EditingQuery.ParentProductCategoryID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.ParentProductCategoryID = value.Value;
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

    public ListVM(ProductCategoryService dataService)
        : base(dataService, true)
    {
        // AdvancedQuery.Start DateTimeRanges
        // AdvancedQuery.DateTimeRangeList: DateTimeRangeListPast/DateTimeRangeListFuture/DateTimeRangeListAll
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        // AdvancedQuery.DateTimeRange.1 ModifiedDateRange
        SelectedModifiedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        /*
        SelectedModifiedDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        SelectedModifiedDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        */
        // AdvancedQuery.End DateTimeRanges

        // 5. Init LaunchProductCategoryDashboardPageCommand
        LaunchDashboardPageCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryDashboardPageCommand(AppShellRoutes.ProductCategoryListPage);
        // 6. Init LaunchProductCategoryCreatePopupCommand
        LaunchCreatePopupCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryCreatePopupCommand();
        // 8. Init LaunchProductCategoryDetailsPopupCommand
        LaunchDetailsPopupCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryDetailsPopupCommand();
        // 9. Init LaunchProductCategoryEditPopupCommand
        LaunchEditPopupCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryEditPopupCommand();
        // 10. Init LaunchProductCategoryAdvancedSearchPopupCommand
        AdvancedSearchLaunchCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryAdvancedSearchPopupCommand();
        // 11. Init LaunchProductCategoryListBulkActionsPopupCommand
        ListBulkActionsLaunchCommand = new Command<string>(
            (currentBulkActionName) =>
            {
                BulkUpdateItem = _dataService.GetDefault();
                CurrentBulkActionName = currentBulkActionName;
                var launchCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryListBulkActionsPopupCommand();
                launchCommand.Execute(null);
            },
            (currentBulkActionName) => EnableMultiSelectCommands()
            );
        // 12. Init LaunchProductCategoryListOrderBysPopupCommand
        ListOrderBysLaunchCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryListOrderBysPopupCommand();

    }

    protected override async Task LoadCodeListsIfAny()
    {

        // // ForeignKeys.1. ParentProductCategoryIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetProductCategoryCodeList(new ProductCategoryAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ParentProductCategoryIDList = new List<NameValuePair<int>>(response.ResponseBody);
                SelectedParentProductCategoryID = ParentProductCategoryIDList.FirstOrDefault(t=>t.Value == EditingQuery.ParentProductCategoryID);
            }
        }
    }

    public override void RegisterItemDataChangedMessage()
    {
        RegisterItemDataChangedMessage<ListVM>(this);
    }
}

