using Framework.Models;
using AdventureWorksLT2019.Resx.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class ProductDescriptionDataModel : ObservableValidator, IClone<ProductDescriptionDataModel>, ICopyTo<ProductDescriptionDataModel>, IGetIdentifier<ProductDescriptionIdentifier>
{
    public string Avatar__
    {
        get => GetAvatar();
    }

    private string GetAvatar()
    {
        if (string.IsNullOrEmpty(Description) || Description.Length == 0)
            return "?";
        if (Description.Length == 1)
            return Description[..1];
        return Description[..2];
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

    private int m_ProductDescriptionID;
    [Display(Name = "ProductDescriptionID", ResourceType = typeof(UIStrings))]
    [PrimaryKey]
    public int ProductDescriptionID
    {
        get => m_ProductDescriptionID;
        set
        {
            SetProperty(ref m_ProductDescriptionID, value);
        }
    }

    private string m_Description;
    [Display(Name = "Description", ResourceType = typeof(UIStrings))]
    [StringLength(400, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Description_should_be_1_to_400", MinimumLength = 1)]
    public string Description
    {
        get => m_Description;
        set
        {
            SetProperty(ref m_Description, value);
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

    public ProductDescriptionDataModel Clone()
    {
        return new ProductDescriptionDataModel
        {
            m_ItemUIStatus______ = m_ItemUIStatus______,
            m_IsDeleted______ = m_IsDeleted______,
            m_ProductDescriptionID = m_ProductDescriptionID,
            m_Description = m_Description,
            m_rowguid = m_rowguid,
            m_ModifiedDate = m_ModifiedDate,
        };
    }

    public void CopyTo(ProductDescriptionDataModel destination)
    {
        destination.ItemUIStatus______ = ItemUIStatus______;
        destination.IsDeleted______ = IsDeleted______;
        destination.ProductDescriptionID = ProductDescriptionID;
        destination.Description = Description;
        destination.rowguid = rowguid;
        destination.ModifiedDate = ModifiedDate;
    }

    public ProductDescriptionIdentifier GetIdentifier()
    {
        return new ProductDescriptionIdentifier
        {
            ProductDescriptionID = ProductDescriptionID,
        };
    }
}

