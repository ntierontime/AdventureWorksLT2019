using Framework.Models;
using AdventureWorksLT2019.Resx.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class AddressDataModel : ObservableValidator, IClone<AddressDataModel>, ICopyTo<AddressDataModel>, IGetIdentifier<AddressIdentifier>
{
    public string Avatar__
    {
        get => GetAvatar();
    }

    private string GetAvatar()
    {
        if (string.IsNullOrEmpty(AddressLine1) || AddressLine1.Length == 0)
            return "?";
        if (AddressLine1.Length == 1)
            return AddressLine1[..1];
        return AddressLine1[..2];
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

    private int m_AddressID;
    [Display(Name = "AddressID", ResourceType = typeof(UIStrings))]
    [PrimaryKey]
    public int AddressID
    {
        get => m_AddressID;
        set
        {
            SetProperty(ref m_AddressID, value);
        }
    }

    private string m_AddressLine1;
    [Display(Name = "AddressLine1", ResourceType = typeof(UIStrings))]
    [StringLength(60, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_AddressLine1_should_be_1_to_60", MinimumLength = 1)]
    public string AddressLine1
    {
        get => m_AddressLine1;
        set
        {
            SetProperty(ref m_AddressLine1, value);
            OnPropertyChanged(nameof(Avatar__));
        }
    }

    private string m_AddressLine2;
    [Display(Name = "AddressLine2", ResourceType = typeof(UIStrings))]
    [StringLength(60, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_AddressLine2_should_be_0_to_60")]
    public string AddressLine2
    {
        get => m_AddressLine2;
        set
        {
            SetProperty(ref m_AddressLine2, value);
        }
    }

    private string m_City;
    [Display(Name = "City", ResourceType = typeof(UIStrings))]
    [StringLength(30, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_City_should_be_1_to_30", MinimumLength = 1)]
    public string City
    {
        get => m_City;
        set
        {
            SetProperty(ref m_City, value);
        }
    }

    private string m_StateProvince;
    [Display(Name = "StateProvince", ResourceType = typeof(UIStrings))]
    [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_StateProvince_should_be_1_to_50", MinimumLength = 1)]
    public string StateProvince
    {
        get => m_StateProvince;
        set
        {
            SetProperty(ref m_StateProvince, value);
        }
    }

    private string m_CountryRegion;
    [Display(Name = "CountryRegion", ResourceType = typeof(UIStrings))]
    [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_CountryRegion_should_be_1_to_50", MinimumLength = 1)]
    public string CountryRegion
    {
        get => m_CountryRegion;
        set
        {
            SetProperty(ref m_CountryRegion, value);
        }
    }

    private string m_PostalCode;
    [Display(Name = "PostalCode", ResourceType = typeof(UIStrings))]
    [StringLength(15, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_PostalCode_should_be_1_to_15", MinimumLength = 1)]
    public string PostalCode
    {
        get => m_PostalCode;
        set
        {
            SetProperty(ref m_PostalCode, value);
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

    public AddressDataModel Clone()
    {
        return new AddressDataModel
        {
            m_ItemUIStatus______ = m_ItemUIStatus______,
            m_IsDeleted______ = m_IsDeleted______,
            m_AddressID = m_AddressID,
            m_AddressLine1 = m_AddressLine1,
            m_AddressLine2 = m_AddressLine2,
            m_City = m_City,
            m_StateProvince = m_StateProvince,
            m_CountryRegion = m_CountryRegion,
            m_PostalCode = m_PostalCode,
            m_rowguid = m_rowguid,
            m_ModifiedDate = m_ModifiedDate,
        };
    }

    public void CopyTo(AddressDataModel destination)
    {
        destination.ItemUIStatus______ = ItemUIStatus______;
        destination.IsDeleted______ = IsDeleted______;
        destination.AddressID = AddressID;
        destination.AddressLine1 = AddressLine1;
        destination.AddressLine2 = AddressLine2;
        destination.City = City;
        destination.StateProvince = StateProvince;
        destination.CountryRegion = CountryRegion;
        destination.PostalCode = PostalCode;
        destination.rowguid = rowguid;
        destination.ModifiedDate = ModifiedDate;
    }

    public AddressIdentifier GetIdentifier()
    {
        return new AddressIdentifier
        {
            AddressID = AddressID,
        };
    }
}

