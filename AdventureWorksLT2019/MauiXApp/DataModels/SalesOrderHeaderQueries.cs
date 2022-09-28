using Framework.MauiX.DataModels;
using Framework.Models;

using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class SalesOrderHeaderIdentifier: ObservableBaseQuery
{

    // PredicateType:Equals
    private int? m_SalesOrderID;
    public int? SalesOrderID
    {
        get => m_SalesOrderID;
        set => SetProperty(ref m_SalesOrderID, value);
    }

    public string GetWebApiRoute()
    {
        return $"{SalesOrderID}";
    }
}

public class SalesOrderHeaderAdvancedQuery: ObservableBaseQuery, IClone<SalesOrderHeaderAdvancedQuery>
{

    // PredicateType:Equals
    private int? m_BillToAddressID;
    public int? BillToAddressID
    {
        get => m_BillToAddressID;
        set => SetProperty(ref m_BillToAddressID, value);
    }

    // PredicateType:Equals
    private int? m_ShipToAddressID;
    public int? ShipToAddressID
    {
        get => m_ShipToAddressID;
        set => SetProperty(ref m_ShipToAddressID, value);
    }

    // PredicateType:Equals
    private int? m_CustomerID;
    public int? CustomerID
    {
        get => m_CustomerID;
        set => SetProperty(ref m_CustomerID, value);
    }

    // PredicateType:Equals
    private string m_OnlineOrderFlag = BooleanSearchOptions.All.ToString();
    public string OnlineOrderFlag
    {
        get => m_OnlineOrderFlag;
        set => SetProperty(ref m_OnlineOrderFlag, value);
    }

    // PredicateType:Range
    private string m_OrderDateRange = PreDefinedDateTimeRanges.AllTime.ToString();
    public string OrderDateRange
    {
        get => m_OrderDateRange;
        set => SetProperty(ref m_OrderDateRange, value);
    }
    private DateTime? m_OrderDateRangeLower;
    public DateTime? OrderDateRangeLower
    {
        get => m_OrderDateRangeLower;
        set => SetProperty(ref m_OrderDateRangeLower, value);
    }
    private DateTime? m_OrderDateRangeUpper;
    public DateTime? OrderDateRangeUpper
    {
        get => m_OrderDateRangeUpper;
        set => SetProperty(ref m_OrderDateRangeUpper, value);
    }

    // PredicateType:Range
    private string m_DueDateRange = PreDefinedDateTimeRanges.AllTime.ToString();
    public string DueDateRange
    {
        get => m_DueDateRange;
        set => SetProperty(ref m_DueDateRange, value);
    }
    private DateTime? m_DueDateRangeLower;
    public DateTime? DueDateRangeLower
    {
        get => m_DueDateRangeLower;
        set => SetProperty(ref m_DueDateRangeLower, value);
    }
    private DateTime? m_DueDateRangeUpper;
    public DateTime? DueDateRangeUpper
    {
        get => m_DueDateRangeUpper;
        set => SetProperty(ref m_DueDateRangeUpper, value);
    }

    // PredicateType:Range
    private string m_ShipDateRange = PreDefinedDateTimeRanges.AllTime.ToString();
    public string ShipDateRange
    {
        get => m_ShipDateRange;
        set => SetProperty(ref m_ShipDateRange, value);
    }
    private DateTime? m_ShipDateRangeLower;
    public DateTime? ShipDateRangeLower
    {
        get => m_ShipDateRangeLower;
        set => SetProperty(ref m_ShipDateRangeLower, value);
    }
    private DateTime? m_ShipDateRangeUpper;
    public DateTime? ShipDateRangeUpper
    {
        get => m_ShipDateRangeUpper;
        set => SetProperty(ref m_ShipDateRangeUpper, value);
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

    public SalesOrderHeaderAdvancedQuery Clone()
    {
        return new SalesOrderHeaderAdvancedQuery
        {

            // PredicateType:Equals
            m_BillToAddressID = m_BillToAddressID,

            // PredicateType:Equals
            m_ShipToAddressID = m_ShipToAddressID,

            // PredicateType:Equals
            m_CustomerID = m_CustomerID,

            // PredicateType:Equals
            m_OnlineOrderFlag = m_OnlineOrderFlag,

            // PredicateType:Range
            m_OrderDateRange = m_OrderDateRange,
            m_OrderDateRangeLower = m_OrderDateRangeLower,
            m_OrderDateRangeUpper = m_OrderDateRangeUpper,

            // PredicateType:Range
            m_DueDateRange = m_DueDateRange,
            m_DueDateRangeLower = m_DueDateRangeLower,
            m_DueDateRangeUpper = m_DueDateRangeUpper,

            // PredicateType:Range
            m_ShipDateRange = m_ShipDateRange,
            m_ShipDateRangeLower = m_ShipDateRangeLower,
            m_ShipDateRangeUpper = m_ShipDateRangeUpper,

            // PredicateType:Range
            m_ModifiedDateRange = m_ModifiedDateRange,
            m_ModifiedDateRangeLower = m_ModifiedDateRangeLower,
            m_ModifiedDateRangeUpper = m_ModifiedDateRangeUpper,
        };
    }
}

