using Framework.MauiX.DataModels;
using Framework.Models;

using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class ProductCategoryIdentifier: ObservableBaseQuery
{

    // PredicateType:Equals
    private int? m_ProductCategoryID;
    public int? ProductCategoryID
    {
        get => m_ProductCategoryID;
        set => SetProperty(ref m_ProductCategoryID, value);
    }

    public string GetWebApiRoute()
    {
        return $"{ProductCategoryID}";
    }
}

public class ProductCategoryAdvancedQuery: ObservableBaseQuery, IClone<ProductCategoryAdvancedQuery>
{

    // PredicateType:Equals
    private int? m_ParentProductCategoryID;
    public int? ParentProductCategoryID
    {
        get => m_ParentProductCategoryID;
        set => SetProperty(ref m_ParentProductCategoryID, value);
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

    public ProductCategoryAdvancedQuery Clone()
    {
        return new ProductCategoryAdvancedQuery
        {

            // PredicateType:Equals
            m_ParentProductCategoryID = m_ParentProductCategoryID,

            // PredicateType:Range
            m_ModifiedDateRange = m_ModifiedDateRange,
            m_ModifiedDateRangeLower = m_ModifiedDateRangeLower,
            m_ModifiedDateRangeUpper = m_ModifiedDateRangeUpper,
        };
    }
}

