using Framework.Models;
using AdventureWorksLT2019.Resx.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class ProductDescriptionDataModel : ObservableValidator, IClone<ProductDescriptionDataModel>, ICopyTo<ProductDescriptionDataModel>
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
            return Description.Substring(0, 1);
        return Description.Substring(0, 2);
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

    protected int m_ProductDescriptionID;
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

    protected string m_Description;
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
}

