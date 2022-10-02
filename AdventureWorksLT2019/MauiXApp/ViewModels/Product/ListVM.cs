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

public class ListVM : ListVMBase<ProductAdvancedQuery, ProductIdentifier, ProductDataModel, ProductService, ProductItemChangedMessage, ProductItemRequestMessage>
{
    // AdvancedQuery.Start

    // ForeignKeys.1. ProductCategoryIDList
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
                EditingQuery.ProductCategoryID = value.Value;
            }
        }
    }

    // ForeignKeys.2. ParentIDList
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
                EditingQuery.ParentID = value.Value;
            }
        }
    }

    // ForeignKeys.3. ProductModelIDList
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
                EditingQuery.ProductModelID = value.Value;
            }
        }
    }

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

    public ListVM(ProductService dataService)
        : base(dataService)
    {
        // AdvancedQuery.Start
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        SelectedSellStartDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.SellStartDateRange);
        /*
        SelectedSellStartDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.SellStartDateRange);
        SelectedSellStartDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.SellStartDateRange);
        */

        SelectedSellEndDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.SellEndDateRange);
        /*
        SelectedSellEndDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.SellEndDateRange);
        SelectedSellEndDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.SellEndDateRange);
        */

        SelectedDiscontinuedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.DiscontinuedDateRange);
        /*
        SelectedDiscontinuedDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.DiscontinuedDateRange);
        SelectedDiscontinuedDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.DiscontinuedDateRange);
        */

        SelectedModifiedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        /*
        SelectedModifiedDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        SelectedModifiedDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        */
        // AdvancedQuery.End

        LaunchDetailsPageCommand = AdventureWorksLT2019.MauiXApp.Common.Helpers.LaunchViewCommandsHelper.GetLaunchProductCategoryDetailsPageCommand(AdventureWorksLT2019.MauiXApp.Common.Services.AppShellRoutes.ProductCategoryListPage);
        LaunchDetailsPopupCommand = AdventureWorksLT2019.MauiXApp.Common.Helpers.LaunchViewCommandsHelper.GetLaunchProductCategoryDetailsPopupCommand();

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

