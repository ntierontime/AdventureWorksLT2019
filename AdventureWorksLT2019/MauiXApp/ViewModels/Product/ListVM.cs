using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.Helpers;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.Models;
using Framework.MauiX.ViewModels;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Product;

public class ListVM : ListVMBase<ProductAdvancedQuery, ProductIdentifier, ProductDataModel, ProductService, ProductItemChangedMessage, ProductItemRequestMessage>
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

