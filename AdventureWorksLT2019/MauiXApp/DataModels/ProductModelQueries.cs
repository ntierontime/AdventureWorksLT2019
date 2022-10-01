using Framework.MauiX.DataModels;
using Framework.Models;

using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class ProductModelIdentifier: ObservableBaseQuery
{

    // PredicateType:Equals
    private int? m_ProductModelID;
    public int? ProductModelID
    {
        get => m_ProductModelID;
        set => SetProperty(ref m_ProductModelID, value);
    }

    public string GetWebApiRoute()
    {
        return $"{ProductModelID}";
    }

    public override int GetHashCode()
    {
        return ($"{ProductModelID}").GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is ProductModelIdentifier))
            return false;
        var typedObj = (ProductModelIdentifier)obj;
        return ProductModelID == typedObj.ProductModelID;
    }
}

public class ProductModelAdvancedQuery: ObservableBaseQuery, IClone<ProductModelAdvancedQuery>
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

    public ProductModelAdvancedQuery Clone()
    {
        return new ProductModelAdvancedQuery
        {

            // PredicateType:Range
            m_ModifiedDateRange = m_ModifiedDateRange,
            m_ModifiedDateRangeLower = m_ModifiedDateRangeLower,
            m_ModifiedDateRangeUpper = m_ModifiedDateRangeUpper,
        };
    }
}

