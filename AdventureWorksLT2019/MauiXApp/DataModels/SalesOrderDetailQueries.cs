using Framework.MauiX.DataModels;
using Framework.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class SalesOrderDetailIdentifier: ObservableObject
{

    // PredicateType:Equals
    private int? m_SalesOrderID;
    public int? SalesOrderID
    {
        get => m_SalesOrderID;
        set => SetProperty(ref m_SalesOrderID, value);
    }

    // PredicateType:Equals
    private int? m_SalesOrderDetailID;
    public int? SalesOrderDetailID
    {
        get => m_SalesOrderDetailID;
        set => SetProperty(ref m_SalesOrderDetailID, value);
    }

    public string GetWebApiRoute()
    {
        return $"{SalesOrderID}/{SalesOrderDetailID}";
    }

    public override int GetHashCode()
    {
        return ($"{SalesOrderID}/{SalesOrderDetailID}").GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is SalesOrderDetailIdentifier))
            return false;
        var typedObj = (SalesOrderDetailIdentifier)obj;
        return SalesOrderID == typedObj.SalesOrderID && SalesOrderDetailID == typedObj.SalesOrderDetailID;
    }
}

public class SalesOrderDetailAdvancedQuery: ObservableBaseQuery, IClone<SalesOrderDetailAdvancedQuery>
{

    // PredicateType:Equals
    private int? m_ProductID;
    public int? ProductID
    {
        get => m_ProductID;
        set => SetProperty(ref m_ProductID, value);
    }

    // PredicateType:Equals
    private int? m_ProductCategoryID;
    public int? ProductCategoryID
    {
        get => m_ProductCategoryID;
        set => SetProperty(ref m_ProductCategoryID, value);
    }

    // PredicateType:Equals
    private int? m_ProductCategory_ParentID;
    public int? ProductCategory_ParentID
    {
        get => m_ProductCategory_ParentID;
        set => SetProperty(ref m_ProductCategory_ParentID, value);
    }

    // PredicateType:Equals
    private int? m_ProductModelID;
    public int? ProductModelID
    {
        get => m_ProductModelID;
        set => SetProperty(ref m_ProductModelID, value);
    }

    // PredicateType:Equals
    private int? m_SalesOrderID;
    public int? SalesOrderID
    {
        get => m_SalesOrderID;
        set => SetProperty(ref m_SalesOrderID, value);
    }

    // PredicateType:Equals
    private int? m_BillToID;
    public int? BillToID
    {
        get => m_BillToID;
        set => SetProperty(ref m_BillToID, value);
    }

    // PredicateType:Equals
    private int? m_ShipToID;
    public int? ShipToID
    {
        get => m_ShipToID;
        set => SetProperty(ref m_ShipToID, value);
    }

    // PredicateType:Equals
    private int? m_CustomerID;
    public int? CustomerID
    {
        get => m_CustomerID;
        set => SetProperty(ref m_CustomerID, value);
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

    public SalesOrderDetailAdvancedQuery Clone()
    {
        return new SalesOrderDetailAdvancedQuery
        {

            // PredicateType:Equals
            m_ProductID = m_ProductID,

            // PredicateType:Equals
            m_ProductCategoryID = m_ProductCategoryID,

            // PredicateType:Equals
            m_ProductCategory_ParentID = m_ProductCategory_ParentID,

            // PredicateType:Equals
            m_ProductModelID = m_ProductModelID,

            // PredicateType:Equals
            m_SalesOrderID = m_SalesOrderID,

            // PredicateType:Equals
            m_BillToID = m_BillToID,

            // PredicateType:Equals
            m_ShipToID = m_ShipToID,

            // PredicateType:Equals
            m_CustomerID = m_CustomerID,

            // PredicateType:Range
            m_ModifiedDateRange = m_ModifiedDateRange,
            m_ModifiedDateRangeLower = m_ModifiedDateRangeLower,
            m_ModifiedDateRangeUpper = m_ModifiedDateRangeUpper,
        };
    }
}

