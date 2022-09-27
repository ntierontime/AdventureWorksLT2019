using Framework.MauiX.DataModels;
using Framework.Models;

using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class ErrorLogIdentifier: ObservableBaseQuery
{

    // PredicateType:Equals
    private int? m_ErrorLogID;
    public int? ErrorLogID
    {
        get => m_ErrorLogID;
        set => SetProperty(ref m_ErrorLogID, value);
    }

    public string GetWebApiRoute()
    {
        return $"{ErrorLogID}";
    }
}

public class ErrorLogAdvancedQuery: ObservableBaseQuery, IClone<ErrorLogAdvancedQuery>
{

    // PredicateType:Range
    private string m_ErrorTimeRange = PreDefinedDateTimeRanges.AllTime.ToString();
    public string ErrorTimeRange
    {
        get => m_ErrorTimeRange;
        set => SetProperty(ref m_ErrorTimeRange, value);
    }
    private DateTime? m_ErrorTimeRangeLower;
    public DateTime? ErrorTimeRangeLower
    {
        get => m_ErrorTimeRangeLower;
        set => SetProperty(ref m_ErrorTimeRangeLower, value);
    }
    private DateTime? m_ErrorTimeRangeUpper;
    public DateTime? ErrorTimeRangeUpper
    {
        get => m_ErrorTimeRangeUpper;
        set => SetProperty(ref m_ErrorTimeRangeUpper, value);
    }

    public ErrorLogAdvancedQuery Clone()
    {
        return new ErrorLogAdvancedQuery
        {

        // PredicateType:Range
        m_ErrorTimeRange = m_ErrorTimeRange,
        m_ErrorTimeRangeLower = m_ErrorTimeRangeLower,
        m_ErrorTimeRangeUpper = m_ErrorTimeRangeUpper,
        };
    }
}

