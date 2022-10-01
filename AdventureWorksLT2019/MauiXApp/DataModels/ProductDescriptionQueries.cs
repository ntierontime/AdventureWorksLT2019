using Framework.MauiX.DataModels;
using Framework.Models;

using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class ProductDescriptionIdentifier: ObservableBaseQuery
{

    // PredicateType:Equals
    private int? m_ProductDescriptionID;
    public int? ProductDescriptionID
    {
        get => m_ProductDescriptionID;
        set => SetProperty(ref m_ProductDescriptionID, value);
    }

    public string GetWebApiRoute()
    {
        return $"{ProductDescriptionID}";
    }

    public override int GetHashCode()
    {
        return ($"{ProductDescriptionID}").GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is ProductDescriptionIdentifier))
            return false;
        var typedObj = (ProductDescriptionIdentifier)obj;
        return ProductDescriptionID == typedObj.ProductDescriptionID;
    }
}

public class ProductDescriptionAdvancedQuery: ObservableBaseQuery, IClone<ProductDescriptionAdvancedQuery>
{

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

    public ProductDescriptionAdvancedQuery Clone()
    {
        return new ProductDescriptionAdvancedQuery
        {

            // PredicateType:Range
            m_ModifiedDateRange = m_ModifiedDateRange,
            m_ModifiedDateRangeLower = m_ModifiedDateRangeLower,
            m_ModifiedDateRangeUpper = m_ModifiedDateRangeUpper,
        };
    }
}

