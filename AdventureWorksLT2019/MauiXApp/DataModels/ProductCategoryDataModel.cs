using Framework.Models;
using AdventureWorksLT2019.Resx.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class ProductCategoryDataModel : ObservableValidator, IClone<ProductCategoryDataModel>, ICopyTo<ProductCategoryDataModel>, IGetIdentifier<ProductCategoryIdentifier>
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

    private int m_ProductCategoryID;
    [Display(Name = "ProductCategoryID", ResourceType = typeof(UIStrings))]
    [PrimaryKey]
    public int ProductCategoryID
    {
        get => m_ProductCategoryID;
        set
        {
            SetProperty(ref m_ProductCategoryID, value);
        }
    }

    private int? m_ParentProductCategoryID;
    [Display(Name = "ProductCategory", ResourceType = typeof(UIStrings))]
    public int? ParentProductCategoryID
    {
        get => m_ParentProductCategoryID;
        set
        {
            SetProperty(ref m_ParentProductCategoryID, value);
        }
    }

    private string m_Name;
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

    private string m_Parent_Name;
    [Display(Name = "ProductCategory", ResourceType = typeof(UIStrings))]
    public string Parent_Name
    {
        get => m_Parent_Name;
        set
        {
            SetProperty(ref m_Parent_Name, value);
        }
    }

    public ProductCategoryDataModel Clone()
    {
        return new ProductCategoryDataModel
        {
            m_ItemUIStatus______ = m_ItemUIStatus______,
            m_IsDeleted______ = m_IsDeleted______,
            m_ProductCategoryID = m_ProductCategoryID,
            m_ParentProductCategoryID = m_ParentProductCategoryID,
            m_Name = m_Name,
            m_rowguid = m_rowguid,
            m_ModifiedDate = m_ModifiedDate,
            m_Parent_Name = m_Parent_Name,
        };
    }

    public void CopyTo(ProductCategoryDataModel destination)
    {
        destination.ItemUIStatus______ = ItemUIStatus______;
        destination.IsDeleted______ = IsDeleted______;
        destination.ProductCategoryID = ProductCategoryID;
        destination.ParentProductCategoryID = ParentProductCategoryID;
        destination.Name = Name;
        destination.rowguid = rowguid;
        destination.ModifiedDate = ModifiedDate;
        destination.Parent_Name = Parent_Name;
    }

    public ProductCategoryIdentifier GetIdentifier()
    {
        return new ProductCategoryIdentifier
        {
            ProductCategoryID = ProductCategoryID,
        };
    }
}

