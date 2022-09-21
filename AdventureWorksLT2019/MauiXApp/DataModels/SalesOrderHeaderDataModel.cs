using Framework.Models;
using AdventureWorksLT2019.Resx.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class SalesOrderHeaderDataModel : ObservableValidator, IClone<SalesOrderHeaderDataModel>, ICopyTo<SalesOrderHeaderDataModel>
{
    public string Avatar__
    {
        get => GetAvatar();
    }

    private string GetAvatar()
    {
        if (string.IsNullOrEmpty(SalesOrderNumber) || SalesOrderNumber.Length == 0)
            return "?";
        if (SalesOrderNumber.Length == 1)
            return SalesOrderNumber.Substring(0, 1);
        return SalesOrderNumber.Substring(0, 2);
    }

    protected ItemUIStatus m_ItemUIStatus______;
    public ItemUIStatus ItemUIStatus______
    {
        get => m_ItemUIStatus______;
        set => SetProperty(ref m_ItemUIStatus______, value);
    }
    protected System.Boolean m_IsDeleted______;
    public System.Boolean IsDeleted______
    {
        get => m_IsDeleted______;
        set => SetProperty(ref m_IsDeleted______, value);
    }

    protected int m_SalesOrderID;
    [Display(Name = "SalesOrderID", ResourceType = typeof(UIStrings))]
    [PrimaryKey]
    public int SalesOrderID
    {
        get => m_SalesOrderID;
        set
        {
            SetProperty(ref m_SalesOrderID, value);
        }
    }

    protected byte m_RevisionNumber;
    [Display(Name = "RevisionNumber", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="RevisionNumber_is_required")]
    public byte RevisionNumber
    {
        get => m_RevisionNumber;
        set
        {
            SetProperty(ref m_RevisionNumber, value);
        }
    }

    protected System.DateTime m_OrderDate;
    [Display(Name = "OrderDate", ResourceType = typeof(UIStrings))]
    [DataType(DataType.DateTime)]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="OrderDate_is_required")]
    public System.DateTime OrderDate
    {
        get => m_OrderDate;
        set
        {
            SetProperty(ref m_OrderDate, value);
        }
    }

    protected System.DateTime m_DueDate;
    [Display(Name = "DueDate", ResourceType = typeof(UIStrings))]
    [DataType(DataType.DateTime)]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="DueDate_is_required")]
    public System.DateTime DueDate
    {
        get => m_DueDate;
        set
        {
            SetProperty(ref m_DueDate, value);
        }
    }

    protected System.DateTime? m_ShipDate;
    [Display(Name = "ShipDate", ResourceType = typeof(UIStrings))]
    [DataType(DataType.DateTime)]
    public System.DateTime? ShipDate
    {
        get => m_ShipDate;
        set
        {
            SetProperty(ref m_ShipDate, value);
        }
    }

    protected byte m_Status;
    [Display(Name = "Status", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="Status_is_required")]
    public byte Status
    {
        get => m_Status;
        set
        {
            SetProperty(ref m_Status, value);
        }
    }

    protected bool m_OnlineOrderFlag;
    [Display(Name = "OnlineOrderFlag", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="OnlineOrderFlag_is_required")]
    public bool OnlineOrderFlag
    {
        get => m_OnlineOrderFlag;
        set
        {
            SetProperty(ref m_OnlineOrderFlag, value);
        }
    }

    protected string m_SalesOrderNumber;
    [Display(Name = "SalesOrderNumber", ResourceType = typeof(UIStrings))]
    [StringLength(25, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_SalesOrderNumber_should_be_0_to_25")]
    public string SalesOrderNumber
    {
        get => m_SalesOrderNumber;
        set
        {
            SetProperty(ref m_SalesOrderNumber, value);
            OnPropertyChanged(nameof(Avatar__));
        }
    }

    protected string m_PurchaseOrderNumber;
    [Display(Name = "PurchaseOrderNumber", ResourceType = typeof(UIStrings))]
    [StringLength(25, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_PurchaseOrderNumber_should_be_0_to_25")]
    public string PurchaseOrderNumber
    {
        get => m_PurchaseOrderNumber;
        set
        {
            SetProperty(ref m_PurchaseOrderNumber, value);
        }
    }

    protected string m_AccountNumber;
    [Display(Name = "AccountNumber", ResourceType = typeof(UIStrings))]
    [StringLength(15, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_AccountNumber_should_be_0_to_15")]
    public string AccountNumber
    {
        get => m_AccountNumber;
        set
        {
            SetProperty(ref m_AccountNumber, value);
        }
    }

    protected int m_CustomerID;
    [Display(Name = "Customer", ResourceType = typeof(UIStrings))]
    public int CustomerID
    {
        get => m_CustomerID;
        set
        {
            SetProperty(ref m_CustomerID, value);
        }
    }

    protected int? m_ShipToAddressID;
    [Display(Name = "Address", ResourceType = typeof(UIStrings))]
    public int? ShipToAddressID
    {
        get => m_ShipToAddressID;
        set
        {
            SetProperty(ref m_ShipToAddressID, value);
        }
    }

    protected int? m_BillToAddressID;
    [Display(Name = "Address", ResourceType = typeof(UIStrings))]
    public int? BillToAddressID
    {
        get => m_BillToAddressID;
        set
        {
            SetProperty(ref m_BillToAddressID, value);
        }
    }

    protected string m_ShipMethod;
    [Display(Name = "ShipMethod", ResourceType = typeof(UIStrings))]
    [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_ShipMethod_should_be_1_to_50", MinimumLength = 1)]
    public string ShipMethod
    {
        get => m_ShipMethod;
        set
        {
            SetProperty(ref m_ShipMethod, value);
        }
    }

    protected string m_CreditCardApprovalCode;
    [Display(Name = "CreditCardApprovalCode", ResourceType = typeof(UIStrings))]
    [StringLength(15, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_CreditCardApprovalCode_should_be_0_to_15")]
    public string CreditCardApprovalCode
    {
        get => m_CreditCardApprovalCode;
        set
        {
            SetProperty(ref m_CreditCardApprovalCode, value);
        }
    }

    protected decimal m_SubTotal;
    [Display(Name = "SubTotal", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="SubTotal_is_required")]
    public decimal SubTotal
    {
        get => m_SubTotal;
        set
        {
            SetProperty(ref m_SubTotal, value);
        }
    }

    protected decimal m_TaxAmt;
    [Display(Name = "TaxAmt", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="TaxAmt_is_required")]
    public decimal TaxAmt
    {
        get => m_TaxAmt;
        set
        {
            SetProperty(ref m_TaxAmt, value);
        }
    }

    protected decimal m_Freight;
    [Display(Name = "Freight", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="Freight_is_required")]
    public decimal Freight
    {
        get => m_Freight;
        set
        {
            SetProperty(ref m_Freight, value);
        }
    }

    protected decimal m_TotalDue;
    [Display(Name = "TotalDue", ResourceType = typeof(UIStrings))]
    public decimal TotalDue
    {
        get => m_TotalDue;
        set
        {
            SetProperty(ref m_TotalDue, value);
        }
    }

    protected string m_Comment;
    [Display(Name = "Comment", ResourceType = typeof(UIStrings))]
    [StringLength(4096, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Comment_should_be_0_to_MAX")]
    public string Comment
    {
        get => m_Comment;
        set
        {
            SetProperty(ref m_Comment, value);
        }
    }

    protected System.Guid m_rowguid;
    [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="rowguid_is_required")]
    public System.Guid rowguid
    {
        get => m_rowguid;
        set
        {
            SetProperty(ref m_rowguid, value);
        }
    }

    protected System.DateTime m_ModifiedDate;
    [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
    [DataType(DataType.DateTime)]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ModifiedDate_is_required")]
    public System.DateTime ModifiedDate
    {
        get => m_ModifiedDate;
        set
        {
            SetProperty(ref m_ModifiedDate, value);
        }
    }

    protected string m_BillTo_Name;
    [Display(Name = "AddressLine1", ResourceType = typeof(UIStrings))]
    public string BillTo_Name
    {
        get => m_BillTo_Name;
        set
        {
            SetProperty(ref m_BillTo_Name, value);
        }
    }

    protected string m_Customer_Name;
    [Display(Name = "Title", ResourceType = typeof(UIStrings))]
    public string Customer_Name
    {
        get => m_Customer_Name;
        set
        {
            SetProperty(ref m_Customer_Name, value);
        }
    }

    protected string m_ShipTo_Name;
    [Display(Name = "AddressLine1", ResourceType = typeof(UIStrings))]
    public string ShipTo_Name
    {
        get => m_ShipTo_Name;
        set
        {
            SetProperty(ref m_ShipTo_Name, value);
        }
    }

    public SalesOrderHeaderDataModel Clone()
    {
        return new SalesOrderHeaderDataModel
        {
            m_ItemUIStatus______ = m_ItemUIStatus______,
            m_IsDeleted______ = m_IsDeleted______,
            m_SalesOrderID = m_SalesOrderID,
            m_RevisionNumber = m_RevisionNumber,
            m_OrderDate = m_OrderDate,
            m_DueDate = m_DueDate,
            m_ShipDate = m_ShipDate,
            m_Status = m_Status,
            m_OnlineOrderFlag = m_OnlineOrderFlag,
            m_SalesOrderNumber = m_SalesOrderNumber,
            m_PurchaseOrderNumber = m_PurchaseOrderNumber,
            m_AccountNumber = m_AccountNumber,
            m_CustomerID = m_CustomerID,
            m_ShipToAddressID = m_ShipToAddressID,
            m_BillToAddressID = m_BillToAddressID,
            m_ShipMethod = m_ShipMethod,
            m_CreditCardApprovalCode = m_CreditCardApprovalCode,
            m_SubTotal = m_SubTotal,
            m_TaxAmt = m_TaxAmt,
            m_Freight = m_Freight,
            m_TotalDue = m_TotalDue,
            m_Comment = m_Comment,
            m_rowguid = m_rowguid,
            m_ModifiedDate = m_ModifiedDate,
            m_BillTo_Name = m_BillTo_Name,
            m_Customer_Name = m_Customer_Name,
            m_ShipTo_Name = m_ShipTo_Name,
        };
    }

    public void CopyTo(SalesOrderHeaderDataModel destination)
    {
        destination.ItemUIStatus______ = ItemUIStatus______;
        destination.IsDeleted______ = IsDeleted______;
        destination.SalesOrderID = SalesOrderID;
        destination.RevisionNumber = RevisionNumber;
        destination.OrderDate = OrderDate;
        destination.DueDate = DueDate;
        destination.ShipDate = ShipDate;
        destination.Status = Status;
        destination.OnlineOrderFlag = OnlineOrderFlag;
        destination.SalesOrderNumber = SalesOrderNumber;
        destination.PurchaseOrderNumber = PurchaseOrderNumber;
        destination.AccountNumber = AccountNumber;
        destination.CustomerID = CustomerID;
        destination.ShipToAddressID = ShipToAddressID;
        destination.BillToAddressID = BillToAddressID;
        destination.ShipMethod = ShipMethod;
        destination.CreditCardApprovalCode = CreditCardApprovalCode;
        destination.SubTotal = SubTotal;
        destination.TaxAmt = TaxAmt;
        destination.Freight = Freight;
        destination.TotalDue = TotalDue;
        destination.Comment = Comment;
        destination.rowguid = rowguid;
        destination.ModifiedDate = ModifiedDate;
        destination.BillTo_Name = BillTo_Name;
        destination.Customer_Name = Customer_Name;
        destination.ShipTo_Name = ShipTo_Name;
    }
}

