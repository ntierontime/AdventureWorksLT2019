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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ProductModelProductDescription;

public class ListVM : ListVMBase<ProductModelProductDescriptionAdvancedQuery, ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel, ProductModelProductDescriptionService, ProductModelProductDescriptionItemChangedMessage>
{
    #region AdvancedQuery.Start ForeignKey SelectLists and DateTimeRanges

    // AdvancedQuery.ForeignKeys.1. ProductDescriptionIDList
    private List<NameValuePair<int>> m_ProductDescriptionIDList;
    public List<NameValuePair<int>> ProductDescriptionIDList
    {
        get => m_ProductDescriptionIDList;
        set => SetProperty(ref m_ProductDescriptionIDList, value);
    }

    private NameValuePair<int> m_SelectedProductDescriptionID;
    public NameValuePair<int> SelectedProductDescriptionID
    {
        get => m_SelectedProductDescriptionID;
        set
        {
            if (value != null)
            {
                SetProperty(ref m_SelectedProductDescriptionID, value);
                if(EditingQuery != null)
                    EditingQuery.ProductDescriptionID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.ProductDescriptionID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.2. ProductModelIDList
    private List<NameValuePair<int>> m_ProductModelIDList;
    public List<NameValuePair<int>> ProductModelIDList
    {
        get => m_ProductModelIDList;
        set => SetProperty(ref m_ProductModelIDList, value);
    }

    private NameValuePair<int> m_SelectedProductModelID;
    public NameValuePair<int> SelectedProductModelID
    {
        get => m_SelectedProductModelID;
        set
        {
            if (value != null)
            {
                SetProperty(ref m_SelectedProductModelID, value);
                if(EditingQuery != null)
                    EditingQuery.ProductModelID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.ProductModelID = value.Value;
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

    public ListVM(ProductModelProductDescriptionService dataService)
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

        // 1. Init LaunchProductModelProductDescriptionCreatePageCommand
        LaunchCreatePageCommand = LaunchViewCommandsHelper.GetLaunchProductModelProductDescriptionCreatePageCommand(AppShellRoutes.ProductModelProductDescriptionListPage);
        // 2. Init LaunchProductModelProductDescriptionDeletePageCommand
        LaunchDeletePageCommand = LaunchViewCommandsHelper.GetLaunchProductModelProductDescriptionDeletePageCommand(AppShellRoutes.ProductModelProductDescriptionListPage);
        // 3. Init LaunchProductModelProductDescriptionDetailsPageCommand
        LaunchDetailsPageCommand = LaunchViewCommandsHelper.GetLaunchProductModelProductDescriptionDetailsPageCommand(AppShellRoutes.ProductModelProductDescriptionListPage);
        // 4. Init LaunchProductModelProductDescriptionEditPageCommand
        LaunchEditPageCommand = LaunchViewCommandsHelper.GetLaunchProductModelProductDescriptionEditPageCommand(AppShellRoutes.ProductModelProductDescriptionListPage);
        // 5. Init LaunchProductModelProductDescriptionDashboardPageCommand
        LaunchDashboardPageCommand = LaunchViewCommandsHelper.GetLaunchProductModelProductDescriptionDashboardPageCommand(AppShellRoutes.ProductModelProductDescriptionListPage);
        // 6. Init LaunchProductModelProductDescriptionCreatePopupCommand
        LaunchCreatePopupCommand = LaunchViewCommandsHelper.GetLaunchProductModelProductDescriptionCreatePopupCommand();
        // 7. Init LaunchProductModelProductDescriptionDeletePopupCommand
        LaunchDeletePopupCommand = LaunchViewCommandsHelper.GetLaunchProductModelProductDescriptionDeletePopupCommand();
        // 8. Init LaunchProductModelProductDescriptionDetailsPopupCommand
        LaunchDetailsPopupCommand = LaunchViewCommandsHelper.GetLaunchProductModelProductDescriptionDetailsPopupCommand();
        // 9. Init LaunchProductModelProductDescriptionEditPopupCommand
        LaunchEditPopupCommand = LaunchViewCommandsHelper.GetLaunchProductModelProductDescriptionEditPopupCommand();
        // 10. Init LaunchProductModelProductDescriptionAdvancedSearchPopupCommand
        AdvancedSearchLaunchCommand = LaunchViewCommandsHelper.GetLaunchProductModelProductDescriptionAdvancedSearchPopupCommand();
        // 11. Init LaunchProductModelProductDescriptionListBulkActionsPopupCommand
        ListBulkActionsLaunchCommand = new Command<string>(
            (currentBulkActionName) =>
            {
                BulkUpdateItem = _dataService.GetDefault();
                CurrentBulkActionName = currentBulkActionName;
                var launchCommand = LaunchViewCommandsHelper.GetLaunchProductModelProductDescriptionListBulkActionsPopupCommand();
                launchCommand.Execute(null);
            },
            (currentBulkActionName) => EnableMultiSelectCommands()
            );
        // 12. Init LaunchProductModelProductDescriptionListOrderBysPopupCommand
        ListOrderBysLaunchCommand = LaunchViewCommandsHelper.GetLaunchProductModelProductDescriptionListOrderBysPopupCommand();

    }

    protected override async Task LoadCodeListsIfAny()
    {

        // // ForeignKeys.1. ProductDescriptionIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetProductDescriptionCodeList(new ProductDescriptionAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ProductDescriptionIDList = new List<NameValuePair<int>>(response.ResponseBody);
                SelectedProductDescriptionID = ProductDescriptionIDList.FirstOrDefault(t=>t.Value == EditingQuery.ProductDescriptionID);
            }
        }

        // // ForeignKeys.2. ProductModelIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetProductModelCodeList(new ProductModelAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ProductModelIDList = new List<NameValuePair<int>>(response.ResponseBody);
                SelectedProductModelID = ProductModelIDList.FirstOrDefault(t=>t.Value == EditingQuery.ProductModelID);
            }
        }
    }

    public override void RegisterItemDataChangedMessage()
    {
        RegisterItemDataChangedMessage<ListVM>(this);
    }
}

