using Framework.Models;
using AdventureWorksLT2019.Resx.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class SalesOrderDetailDataModel : ObservableValidator, IClone<SalesOrderDetailDataModel>, ICopyTo<SalesOrderDetailDataModel>
{
    public string Avatar__
    {
        get => GetAvatar();
    }

    private string GetAvatar()
    {
        if (string.IsNullOrEmpty(SalesOrderID.ToString()) || SalesOrderID.ToString().Length == 0)
            return "?";
        if (SalesOrderID.ToString().Length == 1)
            return SalesOrderID.ToString().Substring(0, 1);
        return SalesOrderID.ToString().Substring(0, 2);
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

    private Int32 m___DBId__;
    [PrimaryKey, AutoIncrement]
    public Int32 __DBId__
    {
        get => m___DBId__;
        set => SetProperty(ref m___DBId__, value);
    }

    protected int m_SalesOrderID;
    [Display(Name = "SalesOrderHeader", ResourceType = typeof(UIStrings))]
    public int SalesOrderID
    {
        get => m_SalesOrderID;
        set
        {
            SetProperty(ref m_SalesOrderID, value);
            OnPropertyChanged(nameof(Avatar__));
        }
    }

    protected int m_SalesOrderDetailID;
    [Display(Name = "SalesOrderDetailID", ResourceType = typeof(UIStrings))]
    public int SalesOrderDetailID
    {
        get => m_SalesOrderDetailID;
        set
        {
            SetProperty(ref m_SalesOrderDetailID, value);
        }
    }

    protected short m_OrderQty;
    [Display(Name = "OrderQty", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="OrderQty_is_required")]
    public short OrderQty
    {
        get => m_OrderQty;
        set
        {
            SetProperty(ref m_OrderQty, value);
        }
    }

    protected int m_ProductID;
    [Display(Name = "Product", ResourceType = typeof(UIStrings))]
    public int ProductID
    {
        get => m_ProductID;
        set
        {
            SetProperty(ref m_ProductID, value);
        }
    }

    protected decimal m_UnitPrice;
    [Display(Name = "UnitPrice", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="UnitPrice_is_required")]
    public decimal UnitPrice
    {
        get => m_UnitPrice;
        set
        {
            SetProperty(ref m_UnitPrice, value);
        }
    }

    protected decimal m_UnitPriceDiscount;
    [Display(Name = "UnitPriceDiscount", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="UnitPriceDiscount_is_required")]
    public decimal UnitPriceDiscount
    {
        get => m_UnitPriceDiscount;
        set
        {
            SetProperty(ref m_UnitPriceDiscount, value);
        }
    }

    protected decimal m_LineTotal;
    [Display(Name = "LineTotal", ResourceType = typeof(UIStrings))]
    public decimal LineTotal
    {
        get => m_LineTotal;
        set
        {
            SetProperty(ref m_LineTotal, value);
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

    protected string m_Product_Name;
    [Display(Name = "Name", ResourceType = typeof(UIStrings))]
    public string Product_Name
    {
        get => m_Product_Name;
        set
        {
            SetProperty(ref m_Product_Name, value);
        }
    }

    protected int m_ProductCategoryID;
    [Display(Name = "ProductCategory", ResourceType = typeof(UIStrings))]
    public int ProductCategoryID
    {
        get => m_ProductCategoryID;
        set
        {
            SetProperty(ref m_ProductCategoryID, value);
        }
    }

    protected string m_ProductCategory_Name;
    [Display(Name = "Name", ResourceType = typeof(UIStrings))]
    public string ProductCategory_Name
    {
        get => m_ProductCategory_Name;
        set
        {
            SetProperty(ref m_ProductCategory_Name, value);
        }
    }

    protected int m_ProductCategory_ParentID;
    [Display(Name = "ProductCategory", ResourceType = typeof(UIStrings))]
    public int ProductCategory_ParentID
    {
        get => m_ProductCategory_ParentID;
        set
        {
            SetProperty(ref m_ProductCategory_ParentID, value);
        }
    }

    protected string m_ProductCategory_Parent_Name;
    [Display(Name = "Name", ResourceType = typeof(UIStrings))]
    public string ProductCategory_Parent_Name
    {
        get => m_ProductCategory_Parent_Name;
        set
        {
            SetProperty(ref m_ProductCategory_Parent_Name, value);
        }
    }

    protected int m_ProductModelID;
    [Display(Name = "ProductModel", ResourceType = typeof(UIStrings))]
    public int ProductModelID
    {
        get => m_ProductModelID;
        set
        {
            SetProperty(ref m_ProductModelID, value);
        }
    }

    protected string m_ProductModel_Name;
    [Display(Name = "Name", ResourceType = typeof(UIStrings))]
    public string ProductModel_Name
    {
        get => m_ProductModel_Name;
        set
        {
            SetProperty(ref m_ProductModel_Name, value);
        }
    }

    protected string m_SalesOrderHeader_Name;
    [Display(Name = "SalesOrderNumber", ResourceType = typeof(UIStrings))]
    public string SalesOrderHeader_Name
    {
        get => m_SalesOrderHeader_Name;
        set
        {
            SetProperty(ref m_SalesOrderHeader_Name, value);
        }
    }

    protected int m_BillToID;
    [Display(Name = "Address", ResourceType = typeof(UIStrings))]
    public int BillToID
    {
        get => m_BillToID;
        set
        {
            SetProperty(ref m_BillToID, value);
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

    protected int m_ShipToID;
    [Display(Name = "Address", ResourceType = typeof(UIStrings))]
    public int ShipToID
    {
        get => m_ShipToID;
        set
        {
            SetProperty(ref m_ShipToID, value);
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

    public SalesOrderDetailDataModel Clone()
    {
        return new SalesOrderDetailDataModel
        {
            m_ItemUIStatus______ = m_ItemUIStatus______,
            m_IsDeleted______ = m_IsDeleted______,
            m___DBId__ = m___DBId__,
            m_SalesOrderID = m_SalesOrderID,
            m_SalesOrderDetailID = m_SalesOrderDetailID,
            m_OrderQty = m_OrderQty,
            m_ProductID = m_ProductID,
            m_UnitPrice = m_UnitPrice,
            m_UnitPriceDiscount = m_UnitPriceDiscount,
            m_LineTotal = m_LineTotal,
            m_rowguid = m_rowguid,
            m_ModifiedDate = m_ModifiedDate,
            m_Product_Name = m_Product_Name,
            m_ProductCategoryID = m_ProductCategoryID,
            m_ProductCategory_Name = m_ProductCategory_Name,
            m_ProductCategory_ParentID = m_ProductCategory_ParentID,
            m_ProductCategory_Parent_Name = m_ProductCategory_Parent_Name,
            m_ProductModelID = m_ProductModelID,
            m_ProductModel_Name = m_ProductModel_Name,
            m_SalesOrderHeader_Name = m_SalesOrderHeader_Name,
            m_BillToID = m_BillToID,
            m_BillTo_Name = m_BillTo_Name,
            m_CustomerID = m_CustomerID,
            m_Customer_Name = m_Customer_Name,
            m_ShipToID = m_ShipToID,
            m_ShipTo_Name = m_ShipTo_Name,
        };
    }

    public void CopyTo(SalesOrderDetailDataModel destination)
    {
        destination.ItemUIStatus______ = ItemUIStatus______;
        destination.IsDeleted______ = IsDeleted______;
        destination.__DBId__ = __DBId__;
        destination.SalesOrderID = SalesOrderID;
        destination.SalesOrderDetailID = SalesOrderDetailID;
        destination.OrderQty = OrderQty;
        destination.ProductID = ProductID;
        destination.UnitPrice = UnitPrice;
        destination.UnitPriceDiscount = UnitPriceDiscount;
        destination.LineTotal = LineTotal;
        destination.rowguid = rowguid;
        destination.ModifiedDate = ModifiedDate;
        destination.Product_Name = Product_Name;
        destination.ProductCategoryID = ProductCategoryID;
        destination.ProductCategory_Name = ProductCategory_Name;
        destination.ProductCategory_ParentID = ProductCategory_ParentID;
        destination.ProductCategory_Parent_Name = ProductCategory_Parent_Name;
        destination.ProductModelID = ProductModelID;
        destination.ProductModel_Name = ProductModel_Name;
        destination.SalesOrderHeader_Name = SalesOrderHeader_Name;
        destination.BillToID = BillToID;
        destination.BillTo_Name = BillTo_Name;
        destination.CustomerID = CustomerID;
        destination.Customer_Name = Customer_Name;
        destination.ShipToID = ShipToID;
        destination.ShipTo_Name = ShipTo_Name;
    }
}

