using Framework.MauiX.DataModels;
using Framework.Models;

using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class BuildVersionIdentifier: ObservableBaseQuery
{

    // PredicateType:Equals
    private byte m_SystemInformationID;
    public byte SystemInformationID
    {
        get => m_SystemInformationID;
        set => SetProperty(ref m_SystemInformationID, value);
    }

    // PredicateType:Equals
    private System.DateTime m_VersionDate;
    public System.DateTime VersionDate
    {
        get => m_VersionDate;
        set => SetProperty(ref m_VersionDate, value);
    }

    // PredicateType:Equals
    private System.DateTime m_ModifiedDate;
    public System.DateTime ModifiedDate
    {
        get => m_ModifiedDate;
        set => SetProperty(ref m_ModifiedDate, value);
    }

    public string GetWebApiRoute()
    {
        return $"{SystemInformationID}/{VersionDate}/{ModifiedDate}";
    }
}

public class BuildVersionAdvancedQuery: ObservableBaseQuery, IClone<BuildVersionAdvancedQuery>
{

    // PredicateType:Range
    private string m_VersionDateRange = PreDefinedDateTimeRanges.AllTime.ToString();
    public string VersionDateRange
    {
        get => m_VersionDateRange;
        set => SetProperty(ref m_VersionDateRange, value);
    }
    private DateTime? m_VersionDateRangeLower;
    public DateTime? VersionDateRangeLower
    {
        get => m_VersionDateRangeLower;
        set => SetProperty(ref m_VersionDateRangeLower, value);
    }
    private DateTime? m_VersionDateRangeUpper;
    public DateTime? VersionDateRangeUpper
    {
        get => m_VersionDateRangeUpper;
        set => SetProperty(ref m_VersionDateRangeUpper, value);
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

    public BuildVersionAdvancedQuery Clone()
    {
        return new BuildVersionAdvancedQuery
        {

        // PredicateType:Range
        m_VersionDateRange = m_VersionDateRange,
        m_VersionDateRangeLower = m_VersionDateRangeLower,
        m_VersionDateRangeUpper = m_VersionDateRangeUpper,

        // PredicateType:Range
        m_ModifiedDateRange = m_ModifiedDateRange,
        m_ModifiedDateRangeLower = m_ModifiedDateRangeLower,
        m_ModifiedDateRangeUpper = m_ModifiedDateRangeUpper,
        };
    }
}

