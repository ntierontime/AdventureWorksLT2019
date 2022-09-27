using Framework.MauiX.DataModels;
using Framework.Models;

using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class ProductIdentifier: ObservableBaseQuery
{

    // PredicateType:Equals
    private int? m_ProductID;
    public int? ProductID
    {
        get => m_ProductID;
        set => SetProperty(ref m_ProductID, value);
    }

    public string GetWebApiRoute()
    {
        return $"{ProductID}";
    }
}

public class ProductAdvancedQuery: ObservableBaseQuery, IClone<ProductAdvancedQuery>
{

    // PredicateType:Equals
    private int? m_ProductCategoryID;
    public int? ProductCategoryID
    {
        get => m_ProductCategoryID;
        set => SetProperty(ref m_ProductCategoryID, value);
    }

    // PredicateType:Equals
    private int? m_ParentID;
    public int? ParentID
    {
        get => m_ParentID;
        set => SetProperty(ref m_ParentID, value);
    }

    // PredicateType:Equals
    private int? m_ProductModelID;
    public int? ProductModelID
    {
        get => m_ProductModelID;
        set => SetProperty(ref m_ProductModelID, value);
    }

    // PredicateType:Range
    private string m_SellStartDateRange = PreDefinedDateTimeRanges.AllTime.ToString();
    public string SellStartDateRange
    {
        get => m_SellStartDateRange;
        set => SetProperty(ref m_SellStartDateRange, value);
    }
    private DateTime? m_SellStartDateRangeLower;
    public DateTime? SellStartDateRangeLower
    {
        get => m_SellStartDateRangeLower;
        set => SetProperty(ref m_SellStartDateRangeLower, value);
    }
    private DateTime? m_SellStartDateRangeUpper;
    public DateTime? SellStartDateRangeUpper
    {
        get => m_SellStartDateRangeUpper;
        set => SetProperty(ref m_SellStartDateRangeUpper, value);
    }

    // PredicateType:Range
    private string m_SellEndDateRange = PreDefinedDateTimeRanges.AllTime.ToString();
    public string SellEndDateRange
    {
        get => m_SellEndDateRange;
        set => SetProperty(ref m_SellEndDateRange, value);
    }
    private DateTime? m_SellEndDateRangeLower;
    public DateTime? SellEndDateRangeLower
    {
        get => m_SellEndDateRangeLower;
        set => SetProperty(ref m_SellEndDateRangeLower, value);
    }
    private DateTime? m_SellEndDateRangeUpper;
    public DateTime? SellEndDateRangeUpper
    {
        get => m_SellEndDateRangeUpper;
        set => SetProperty(ref m_SellEndDateRangeUpper, value);
    }

    // PredicateType:Range
    private string m_DiscontinuedDateRange = PreDefinedDateTimeRanges.AllTime.ToString();
    public string DiscontinuedDateRange
    {
        get => m_DiscontinuedDateRange;
        set => SetProperty(ref m_DiscontinuedDateRange, value);
    }
    private DateTime? m_DiscontinuedDateRangeLower;
    public DateTime? DiscontinuedDateRangeLower
    {
        get => m_DiscontinuedDateRangeLower;
        set => SetProperty(ref m_DiscontinuedDateRangeLower, value);
    }
    private DateTime? m_DiscontinuedDateRangeUpper;
    public DateTime? DiscontinuedDateRangeUpper
    {
        get => m_DiscontinuedDateRangeUpper;
        set => SetProperty(ref m_DiscontinuedDateRangeUpper, value);
    }

    // PredicateType:Range
    private string m_ModifiedDateRange = PreDefinedDateTimeRanges.AllTime.ToString();
    public string ModifiedDateRange
    {
        get => m_ModifiedDateRange;
        set => SetProperty(ref m_ModifiedDateRange, value);
    }
    private DateTime? m_ModifiedDateRangeLower;
    public DateTime? ModifiedDateRangeLower
    {
        get => m_ModifiedDateRangeLower;
        set => SetProperty(ref m_ModifiedDateRangeLower, value);
    }
    private DateTime? m_ModifiedDateRangeUpper;
    public DateTime? ModifiedDateRangeUpper
    {
        get => m_ModifiedDateRangeUpper;
        set => SetProperty(ref m_ModifiedDateRangeUpper, value);
    }

    public ProductAdvancedQuery Clone()
    {
        return new ProductAdvancedQuery
        {

        // PredicateType:Equals
        m_ProductCategoryID = m_ProductCategoryID,

        // PredicateType:Equals
        m_ParentID = m_ParentID,

        // PredicateType:Equals
        m_ProductModelID = m_ProductModelID,

        // PredicateType:Range
        m_SellStartDateRange = m_SellStartDateRange,
        m_SellStartDateRangeLower = m_SellStartDateRangeLower,
        m_SellStartDateRangeUpper = m_SellStartDateRangeUpper,

        // PredicateType:Range
        m_SellEndDateRange = m_SellEndDateRange,
        m_SellEndDateRangeLower = m_SellEndDateRangeLower,
        m_SellEndDateRangeUpper = m_SellEndDateRangeUpper,

        // PredicateType:Range
        m_DiscontinuedDateRange = m_DiscontinuedDateRange,
        m_DiscontinuedDateRangeLower = m_DiscontinuedDateRangeLower,
        m_DiscontinuedDateRangeUpper = m_DiscontinuedDateRangeUpper,

        // PredicateType:Range
        m_ModifiedDateRange = m_ModifiedDateRange,
        m_ModifiedDateRangeLower = m_ModifiedDateRangeLower,
        m_ModifiedDateRangeUpper = m_ModifiedDateRangeUpper,
        };
    }
}

