using Framework.MauiX.DataModels;
using Framework.Models;

using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class AddressIdentifier: ObservableBaseQuery
{

    // PredicateType:Equals
    private int m_AddressID;
    public int AddressID
    {
        get => m_AddressID;
        set => SetProperty(ref m_AddressID, value);
    }

    public string GetWebApiRoute()
    {
        return $"{AddressID}";
    }
}

public class AddressAdvancedQuery: ObservableBaseQuery, IClone<AddressAdvancedQuery>
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

    public AddressAdvancedQuery Clone()
    {
        return new AddressAdvancedQuery
        {

        // PredicateType:Range
        m_ModifiedDateRange = m_ModifiedDateRange,
        m_ModifiedDateRangeLower = m_ModifiedDateRangeLower,
        m_ModifiedDateRangeUpper = m_ModifiedDateRangeUpper,
        };
    }
}

