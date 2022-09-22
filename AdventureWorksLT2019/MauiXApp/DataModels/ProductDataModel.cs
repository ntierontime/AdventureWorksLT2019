using Framework.Models;
using AdventureWorksLT2019.Resx.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class ProductDataModel : ObservableValidator, IClone<ProductDataModel>, ICopyTo<ProductDataModel>
{
    public string Avatar__
    {
        get => GetAvatar();
    }

    private string GetAvatar()
    {
        if (string.IsNullOrEmpty(Name) || Name.Length == 0)
            return "?";
        if (Name.Length == 1)
            return Name[..1];
        return Name[..2];
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

    protected int m_ProductID;
    [Display(Name = "ProductID", ResourceType = typeof(UIStrings))]
    [PrimaryKey]
    public int ProductID
    {
        get => m_ProductID;
        set
        {
            SetProperty(ref m_ProductID, value);
        }
    }

    protected string m_Name;
    [Display(Name = "Name", ResourceType = typeof(UIStrings))]
    [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Name_should_be_1_to_50", MinimumLength = 1)]
    public string Name
    {
        get => m_Name;
        set
        {
            SetProperty(ref m_Name, value);
            OnPropertyChanged(nameof(Avatar__));
        }
    }

    protected string m_ProductNumber;
    [Display(Name = "ProductNumber", ResourceType = typeof(UIStrings))]
    [StringLength(25, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_ProductNumber_should_be_1_to_25", MinimumLength = 1)]
    public string ProductNumber
    {
        get => m_ProductNumber;
        set
        {
            SetProperty(ref m_ProductNumber, value);
        }
    }

    protected string m_Color;
    [Display(Name = "Color", ResourceType = typeof(UIStrings))]
    [StringLength(15, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Color_should_be_0_to_15")]
    public string Color
    {
        get => m_Color;
        set
        {
            SetProperty(ref m_Color, value);
        }
    }

    protected decimal m_StandardCost;
    [Display(Name = "StandardCost", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="StandardCost_is_required")]
    public decimal StandardCost
    {
        get => m_StandardCost;
        set
        {
            SetProperty(ref m_StandardCost, value);
        }
    }

    protected decimal m_ListPrice;
    [Display(Name = "ListPrice", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ListPrice_is_required")]
    public decimal ListPrice
    {
        get => m_ListPrice;
        set
        {
            SetProperty(ref m_ListPrice, value);
        }
    }

    protected string m_Size;
    [Display(Name = "Size", ResourceType = typeof(UIStrings))]
    [StringLength(5, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Size_should_be_0_to_5")]
    public string Size
    {
        get => m_Size;
        set
        {
            SetProperty(ref m_Size, value);
        }
    }

    protected decimal? m_Weight;
    [Display(Name = "Weight", ResourceType = typeof(UIStrings))]
    public decimal? Weight
    {
        get => m_Weight;
        set
        {
            SetProperty(ref m_Weight, value);
        }
    }

    protected int? m_ProductCategoryID;
    [Display(Name = "ProductCategory", ResourceType = typeof(UIStrings))]
    public int? ProductCategoryID
    {
        get => m_ProductCategoryID;
        set
        {
            SetProperty(ref m_ProductCategoryID, value);
        }
    }

    protected int? m_ProductModelID;
    [Display(Name = "ProductModel", ResourceType = typeof(UIStrings))]
    public int? ProductModelID
    {
        get => m_ProductModelID;
        set
        {
            SetProperty(ref m_ProductModelID, value);
        }
    }

    protected System.DateTime m_SellStartDate;
    [Display(Name = "SellStartDate", ResourceType = typeof(UIStrings))]
    [DataType(DataType.DateTime)]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="SellStartDate_is_required")]
    public System.DateTime SellStartDate
    {
        get => m_SellStartDate;
        set
        {
            SetProperty(ref m_SellStartDate, value);
        }
    }

    protected System.DateTime? m_SellEndDate;
    [Display(Name = "SellEndDate", ResourceType = typeof(UIStrings))]
    [DataType(DataType.DateTime)]
    public System.DateTime? SellEndDate
    {
        get => m_SellEndDate;
        set
        {
            SetProperty(ref m_SellEndDate, value);
        }
    }

    protected System.DateTime? m_DiscontinuedDate;
    [Display(Name = "DiscontinuedDate", ResourceType = typeof(UIStrings))]
    [DataType(DataType.DateTime)]
    public System.DateTime? DiscontinuedDate
    {
        get => m_DiscontinuedDate;
        set
        {
            SetProperty(ref m_DiscontinuedDate, value);
        }
    }

    protected System.Byte[] m_ThumbNailPhoto;
    [Display(Name = "ThumbNailPhoto", ResourceType = typeof(UIStrings))]
    public System.Byte[] ThumbNailPhoto
    {
        get => m_ThumbNailPhoto;
        set
        {
            SetProperty(ref m_ThumbNailPhoto, value);
        }
    }

    protected string m_ThumbnailPhotoFileName;
    [Display(Name = "ThumbnailPhotoFileName", ResourceType = typeof(UIStrings))]
    [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_ThumbnailPhotoFileName_should_be_0_to_50")]
    public string ThumbnailPhotoFileName
    {
        get => m_ThumbnailPhotoFileName;
        set
        {
            SetProperty(ref m_ThumbnailPhotoFileName, value);
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

    protected int m_ParentID;
    [Display(Name = "ProductCategory", ResourceType = typeof(UIStrings))]
    //[PrimaryKey]
    public int ParentID
    {
        get => m_ParentID;
        set
        {
            SetProperty(ref m_ParentID, value);
        }
    }

    protected string m_Parent_Name;
    [Display(Name = "Name", ResourceType = typeof(UIStrings))]
    public string Parent_Name
    {
        get => m_Parent_Name;
        set
        {
            SetProperty(ref m_Parent_Name, value);
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

    public ProductDataModel Clone()
    {
        return new ProductDataModel
        {
            m_ItemUIStatus______ = m_ItemUIStatus______,
            m_IsDeleted______ = m_IsDeleted______,
            m_ProductID = m_ProductID,
            m_Name = m_Name,
            m_ProductNumber = m_ProductNumber,
            m_Color = m_Color,
            m_StandardCost = m_StandardCost,
            m_ListPrice = m_ListPrice,
            m_Size = m_Size,
            m_Weight = m_Weight,
            m_ProductCategoryID = m_ProductCategoryID,
            m_ProductModelID = m_ProductModelID,
            m_SellStartDate = m_SellStartDate,
            m_SellEndDate = m_SellEndDate,
            m_DiscontinuedDate = m_DiscontinuedDate,
            m_ThumbNailPhoto = m_ThumbNailPhoto,
            m_ThumbnailPhotoFileName = m_ThumbnailPhotoFileName,
            m_rowguid = m_rowguid,
            m_ModifiedDate = m_ModifiedDate,
            m_ProductCategory_Name = m_ProductCategory_Name,
            m_ParentID = m_ParentID,
            m_Parent_Name = m_Parent_Name,
            m_ProductModel_Name = m_ProductModel_Name,
        };
    }

    public void CopyTo(ProductDataModel destination)
    {
        destination.ItemUIStatus______ = ItemUIStatus______;
        destination.IsDeleted______ = IsDeleted______;
        destination.ProductID = ProductID;
        destination.Name = Name;
        destination.ProductNumber = ProductNumber;
        destination.Color = Color;
        destination.StandardCost = StandardCost;
        destination.ListPrice = ListPrice;
        destination.Size = Size;
        destination.Weight = Weight;
        destination.ProductCategoryID = ProductCategoryID;
        destination.ProductModelID = ProductModelID;
        destination.SellStartDate = SellStartDate;
        destination.SellEndDate = SellEndDate;
        destination.DiscontinuedDate = DiscontinuedDate;
        destination.ThumbNailPhoto = ThumbNailPhoto;
        destination.ThumbnailPhotoFileName = ThumbnailPhotoFileName;
        destination.rowguid = rowguid;
        destination.ModifiedDate = ModifiedDate;
        destination.ProductCategory_Name = ProductCategory_Name;
        destination.ParentID = ParentID;
        destination.Parent_Name = Parent_Name;
        destination.ProductModel_Name = ProductModel_Name;
    }
}

