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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Product;

public class ListVM : ListVMBase<ProductAdvancedQuery, ProductIdentifier, ProductDataModel, ProductService, ProductItemChangedMessage>
{
    #region AdvancedQuery.Start ForeignKey SelectLists and DateTimeRanges

    // AdvancedQuery.ForeignKeys.1. ProductCategoryIDList
    private List<NameValuePair<int>> m_ProductCategoryIDList;
    public List<NameValuePair<int>> ProductCategoryIDList
    {
        get => m_ProductCategoryIDList;
        set => SetProperty(ref m_ProductCategoryIDList, value);
    }

    private NameValuePair<int> m_SelectedProductCategoryID;
    public NameValuePair<int> SelectedProductCategoryID
    {
        get => m_SelectedProductCategoryID;
        set
        {
            if (value != null)
            {
                SetProperty(ref m_SelectedProductCategoryID, value);
                if(EditingQuery != null)
                    EditingQuery.ProductCategoryID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.ProductCategoryID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.2. ParentIDList
    private List<NameValuePair<int>> m_ParentIDList;
    public List<NameValuePair<int>> ParentIDList
    {
        get => m_ParentIDList;
        set => SetProperty(ref m_ParentIDList, value);
    }

    private NameValuePair<int> m_SelectedParentID;
    public NameValuePair<int> SelectedParentID
    {
        get => m_SelectedParentID;
        set
        {
            if (value != null)
            {
                SetProperty(ref m_SelectedParentID, value);
                if(EditingQuery != null)
                    EditingQuery.ParentID = value.Value;
                if(BulkUpdateItem != null)
                    BulkUpdateItem.ParentID = value.Value;
            }
        }
    }

    // AdvancedQuery.ForeignKeys.3. ProductModelIDList
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

    // AdvancedQuery.DateTimeRange.3 SellStartDateRange
    private NameValuePair m_SelectedSellStartDateRange;
    public NameValuePair SelectedSellStartDateRange
    {
        get => m_SelectedSellStartDateRange;
        set
        {
            SetProperty(ref m_SelectedSellStartDateRange, value);
            EditingQuery.SellStartDateRange = value.Value;
            EditingQuery.SellStartDateRangeLower = PreDefinedDateTimeRangesHelper.GetLowerBound(value.Value);
            EditingQuery.SellStartDateRangeLower = PreDefinedDateTimeRangesHelper.GetUpperBound(value.Value);
        }
    }

    // AdvancedQuery.DateTimeRange.4 SellEndDateRange
    private NameValuePair m_SelectedSellEndDateRange;
    public NameValuePair SelectedSellEndDateRange
    {
        get => m_SelectedSellEndDateRange;
        set
        {
            SetProperty(ref m_SelectedSellEndDateRange, value);
            EditingQuery.SellEndDateRange = value.Value;
            EditingQuery.SellEndDateRangeLower = PreDefinedDateTimeRangesHelper.GetLowerBound(value.Value);
            EditingQuery.SellEndDateRangeLower = PreDefinedDateTimeRangesHelper.GetUpperBound(value.Value);
        }
    }

    // AdvancedQuery.DateTimeRange.5 DiscontinuedDateRange
    private NameValuePair m_SelectedDiscontinuedDateRange;
    public NameValuePair SelectedDiscontinuedDateRange
    {
        get => m_SelectedDiscontinuedDateRange;
        set
        {
            SetProperty(ref m_SelectedDiscontinuedDateRange, value);
            EditingQuery.DiscontinuedDateRange = value.Value;
            EditingQuery.DiscontinuedDateRangeLower = PreDefinedDateTimeRangesHelper.GetLowerBound(value.Value);
            EditingQuery.DiscontinuedDateRangeLower = PreDefinedDateTimeRangesHelper.GetUpperBound(value.Value);
        }
    }

    // AdvancedQuery.DateTimeRange.6 ModifiedDateRange
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

    public ListVM(ProductService dataService)
        : base(dataService, true)
    {
        // AdvancedQuery.Start DateTimeRanges
        // AdvancedQuery.DateTimeRangeList: DateTimeRangeListPast/DateTimeRangeListFuture/DateTimeRangeListAll
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        // AdvancedQuery.DateTimeRange.3 SellStartDateRange
        SelectedSellStartDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.SellStartDateRange);
        /*
        SelectedSellStartDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.SellStartDateRange);
        SelectedSellStartDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.SellStartDateRange);
        */

        // AdvancedQuery.DateTimeRange.4 SellEndDateRange
        SelectedSellEndDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.SellEndDateRange);
        /*
        SelectedSellEndDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.SellEndDateRange);
        SelectedSellEndDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.SellEndDateRange);
        */

        // AdvancedQuery.DateTimeRange.5 DiscontinuedDateRange
        SelectedDiscontinuedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.DiscontinuedDateRange);
        /*
        SelectedDiscontinuedDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.DiscontinuedDateRange);
        SelectedDiscontinuedDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.DiscontinuedDateRange);
        */

        // AdvancedQuery.DateTimeRange.6 ModifiedDateRange
        SelectedModifiedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        /*
        SelectedModifiedDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        SelectedModifiedDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        */
        // AdvancedQuery.End DateTimeRanges

        // 1. Init LaunchProductCreatePageCommand
        LaunchCreatePageCommand = LaunchViewCommandsHelper.GetLaunchProductCreatePageCommand(AppShellRoutes.ProductListPage);
        // 2. Init LaunchProductDeletePageCommand
        LaunchDeletePageCommand = LaunchViewCommandsHelper.GetLaunchProductDeletePageCommand(AppShellRoutes.ProductListPage);
        // 3. Init LaunchProductDetailsPageCommand
        LaunchDetailsPageCommand = LaunchViewCommandsHelper.GetLaunchProductDetailsPageCommand(AppShellRoutes.ProductListPage);
        // 4. Init LaunchProductEditPageCommand
        LaunchEditPageCommand = LaunchViewCommandsHelper.GetLaunchProductEditPageCommand(AppShellRoutes.ProductListPage);
        // 5. Init LaunchProductDashboardPageCommand
        LaunchDashboardPageCommand = LaunchViewCommandsHelper.GetLaunchProductDashboardPageCommand(AppShellRoutes.ProductListPage);
        // 6. Init LaunchProductCreatePopupCommand
        LaunchCreatePopupCommand = LaunchViewCommandsHelper.GetLaunchProductCreatePopupCommand();
        // 7. Init LaunchProductDeletePopupCommand
        LaunchDeletePopupCommand = LaunchViewCommandsHelper.GetLaunchProductDeletePopupCommand();
        // 8. Init LaunchProductDetailsPopupCommand
        LaunchDetailsPopupCommand = LaunchViewCommandsHelper.GetLaunchProductDetailsPopupCommand();
        // 9. Init LaunchProductEditPopupCommand
        LaunchEditPopupCommand = LaunchViewCommandsHelper.GetLaunchProductEditPopupCommand();
        // 10. Init LaunchProductAdvancedSearchPopupCommand
        AdvancedSearchLaunchCommand = LaunchViewCommandsHelper.GetLaunchProductAdvancedSearchPopupCommand();
        // 11. Init LaunchProductListBulkActionsPopupCommand
        ListBulkActionsLaunchCommand = new Command<string>(
            (currentBulkActionName) =>
            {
                BulkUpdateItem = _dataService.GetDefault();
                CurrentBulkActionName = currentBulkActionName;
                var launchCommand = LaunchViewCommandsHelper.GetLaunchProductListBulkActionsPopupCommand();
                launchCommand.Execute(null);
            },
            (currentBulkActionName) => EnableMultiSelectCommands()
            );
        // 12. Init LaunchProductListOrderBysPopupCommand
        ListOrderBysLaunchCommand = LaunchViewCommandsHelper.GetLaunchProductListOrderBysPopupCommand();

    }

    protected override async Task LoadCodeListsIfAny()
    {

        // // ForeignKeys.2. ParentIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetProductCategoryCodeList(new ProductCategoryAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ParentIDList = new List<NameValuePair<int>>(response.ResponseBody);
                SelectedParentID = ParentIDList.FirstOrDefault(t=>t.Value == EditingQuery.ParentID);
            }
        }

        // // ForeignKeys.3. ProductModelIDList
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

