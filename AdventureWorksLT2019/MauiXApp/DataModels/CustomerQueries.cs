using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class CustomerIdentifier
{

    // PredicateType:Equals
    public int? CustomerID { get; set; }

    public string GetWebApiRoute()
    {
        return $"{CustomerID}";
    }
}

public class CustomerAdvancedQuery : Framework.MauiX.DataModels.ObservableBaseQuery, Framework.Models.IClone<CustomerAdvancedQuery>
{
    private string m_NameStyle = "All";
    public string NameStyle
    {
        get => m_NameStyle;
        set => SetProperty(ref m_NameStyle, value);
    }

    private string m_ModifiedDateRange = Framework.Models.PreDefinedDateTimeRanges.AllTime.ToString();
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

    public AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery Clone()
    {
        return new AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery
        {
            m_NameStyle = m_NameStyle,
            m_ModifiedDateRange = m_ModifiedDateRange,
            m_ModifiedDateRangeLower = m_ModifiedDateRangeLower,
            m_ModifiedDateRangeUpper = m_ModifiedDateRangeUpper
        };
    }
}

