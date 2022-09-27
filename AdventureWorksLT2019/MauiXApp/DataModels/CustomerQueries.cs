using Framework.MauiX.DataModels;
using Framework.Models;

using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class CustomerIdentifier: ObservableBaseQuery
{

    // PredicateType:Equals
    private int? m_CustomerID;
    public int? CustomerID
    {
        get => m_CustomerID;
        set => SetProperty(ref m_CustomerID, value);
    }

    public string GetWebApiRoute()
    {
        return $"{CustomerID}";
    }
}

public class CustomerAdvancedQuery: ObservableBaseQuery, IClone<CustomerAdvancedQuery>
{

    // PredicateType:Equals
    private string m_NameStyle = BooleanSearchOptions.All.ToString();
    public string NameStyle
    {
        get => m_NameStyle;
        set => SetProperty(ref m_NameStyle, value);
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

    public CustomerAdvancedQuery Clone()
    {
        return new CustomerAdvancedQuery
        {

        // PredicateType:Equals
        m_NameStyle = m_NameStyle,

        // PredicateType:Range
        m_ModifiedDateRange = m_ModifiedDateRange,
        m_ModifiedDateRangeLower = m_ModifiedDateRangeLower,
        m_ModifiedDateRangeUpper = m_ModifiedDateRangeUpper,
        };
    }
}

