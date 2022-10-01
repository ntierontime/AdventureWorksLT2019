using Framework.MauiX.DataModels;
using Framework.Models;

using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class ProductModelProductDescriptionIdentifier: ObservableBaseQuery
{

    // PredicateType:Equals
    private int? m_ProductModelID;
    public int? ProductModelID
    {
        get => m_ProductModelID;
        set => SetProperty(ref m_ProductModelID, value);
    }

    // PredicateType:Equals
    private int? m_ProductDescriptionID;
    public int? ProductDescriptionID
    {
        get => m_ProductDescriptionID;
        set => SetProperty(ref m_ProductDescriptionID, value);
    }

    // PredicateType:Equals
    private string m_Culture;
    public string Culture
    {
        get => m_Culture;
        set => SetProperty(ref m_Culture, value);
    }

    public string GetWebApiRoute()
    {
        return $"{ProductModelID}/{ProductDescriptionID}/{Culture}";
    }

    public override int GetHashCode()
    {
        return ($"{ProductModelID}/{ProductDescriptionID}/{Culture}").GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is ProductModelProductDescriptionIdentifier))
            return false;
        var typedObj = (ProductModelProductDescriptionIdentifier)obj;
        return ProductModelID == typedObj.ProductModelID && ProductDescriptionID == typedObj.ProductDescriptionID && Culture == typedObj.Culture;
    }
}

public class ProductModelProductDescriptionAdvancedQuery: ObservableBaseQuery, IClone<ProductModelProductDescriptionAdvancedQuery>
{

    // PredicateType:Equals
    private int? m_ProductDescriptionID;
    public int? ProductDescriptionID
    {
        get => m_ProductDescriptionID;
        set => SetProperty(ref m_ProductDescriptionID, value);
    }

    // PredicateType:Equals
    private int? m_ProductModelID;
    public int? ProductModelID
    {
        get => m_ProductModelID;
        set => SetProperty(ref m_ProductModelID, value);
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

    public ProductModelProductDescriptionAdvancedQuery Clone()
    {
        return new ProductModelProductDescriptionAdvancedQuery
        {

            // PredicateType:Equals
            m_ProductDescriptionID = m_ProductDescriptionID,

            // PredicateType:Equals
            m_ProductModelID = m_ProductModelID,

            // PredicateType:Range
            m_ModifiedDateRange = m_ModifiedDateRange,
            m_ModifiedDateRangeLower = m_ModifiedDateRangeLower,
            m_ModifiedDateRangeUpper = m_ModifiedDateRangeUpper,
        };
    }
}

