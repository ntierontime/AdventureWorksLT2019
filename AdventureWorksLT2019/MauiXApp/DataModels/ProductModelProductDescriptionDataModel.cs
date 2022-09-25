using Framework.Models;
using AdventureWorksLT2019.Resx.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class ProductModelProductDescriptionDataModel : ObservableValidator, IClone<ProductModelProductDescriptionDataModel>, ICopyTo<ProductModelProductDescriptionDataModel>, IGetIdentifier<ProductModelProductDescriptionIdentifier>
{
    public string Avatar__
    {
        get => GetAvatar();
    }

    private string GetAvatar()
    {
        if (string.IsNullOrEmpty(Culture) || Culture.Length == 0)
            return "?";
        if (Culture.Length == 1)
            return Culture[..1];
        return Culture[..2];
    }

    private ItemUIStatus m_ItemUIStatus______;
    public ItemUIStatus ItemUIStatus______
    {
        get => m_ItemUIStatus______;
        set => SetProperty(ref m_ItemUIStatus______, value);
    }
    private System.Boolean m_IsDeleted______;
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

    private int m_ProductModelID;
    [Display(Name = "ProductModel", ResourceType = typeof(UIStrings))]
    public int ProductModelID
    {
        get => m_ProductModelID;
        set
        {
            SetProperty(ref m_ProductModelID, value);
        }
    }

    private int m_ProductDescriptionID;
    [Display(Name = "ProductDescription", ResourceType = typeof(UIStrings))]
    public int ProductDescriptionID
    {
        get => m_ProductDescriptionID;
        set
        {
            SetProperty(ref m_ProductDescriptionID, value);
        }
    }

    private string m_Culture;
    [Display(Name = "Culture_", ResourceType = typeof(UIStrings))]
    [StringLength(6, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Culture_should_be_1_to_6", MinimumLength = 1)]
    public string Culture
    {
        get => m_Culture;
        set
        {
            SetProperty(ref m_Culture, value);
            OnPropertyChanged(nameof(Avatar__));
        }
    }

    private System.Guid m_rowguid;
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

    private System.DateTime m_ModifiedDate;
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

    private string m_ProductDescription_Name;
    [Display(Name = "Description", ResourceType = typeof(UIStrings))]
    public string ProductDescription_Name
    {
        get => m_ProductDescription_Name;
        set
        {
            SetProperty(ref m_ProductDescription_Name, value);
        }
    }

    private string m_ProductModel_Name;
    [Display(Name = "Name", ResourceType = typeof(UIStrings))]
    public string ProductModel_Name
    {
        get => m_ProductModel_Name;
        set
        {
            SetProperty(ref m_ProductModel_Name, value);
        }
    }

    public ProductModelProductDescriptionDataModel Clone()
    {
        return new ProductModelProductDescriptionDataModel
        {
            m_ItemUIStatus______ = m_ItemUIStatus______,
            m_IsDeleted______ = m_IsDeleted______,
            m___DBId__ = m___DBId__,
            m_ProductModelID = m_ProductModelID,
            m_ProductDescriptionID = m_ProductDescriptionID,
            m_Culture = m_Culture,
            m_rowguid = m_rowguid,
            m_ModifiedDate = m_ModifiedDate,
            m_ProductDescription_Name = m_ProductDescription_Name,
            m_ProductModel_Name = m_ProductModel_Name,
        };
    }

    public void CopyTo(ProductModelProductDescriptionDataModel destination)
    {
        destination.ItemUIStatus______ = ItemUIStatus______;
        destination.IsDeleted______ = IsDeleted______;
        destination.__DBId__ = __DBId__;
        destination.ProductModelID = ProductModelID;
        destination.ProductDescriptionID = ProductDescriptionID;
        destination.Culture = Culture;
        destination.rowguid = rowguid;
        destination.ModifiedDate = ModifiedDate;
        destination.ProductDescription_Name = ProductDescription_Name;
        destination.ProductModel_Name = ProductModel_Name;
    }

    public ProductModelProductDescriptionIdentifier GetIdentifier()
    {
        return new ProductModelProductDescriptionIdentifier
        {
            ProductModelID = ProductModelID,
            ProductDescriptionID = ProductDescriptionID,
            Culture = Culture,
        };
    }
}

