using Framework.Models;
using AdventureWorksLT2019.Resx.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class CustomerDataModel : ObservableValidator, IClone<CustomerDataModel>, ICopyTo<CustomerDataModel>
{
    public string Avatar__
    {
        get => GetAvatar();
    }

    private string GetAvatar()
    {
        if (string.IsNullOrEmpty(Title) || Title.Length == 0)
            return "?";
        if (Title.Length == 1)
            return Title.Substring(0, 1);
        return Title.Substring(0, 2);
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

    protected int m_CustomerID;
    [Display(Name = "CustomerID", ResourceType = typeof(UIStrings))]
    [PrimaryKey]
    public int CustomerID
    {
        get => m_CustomerID;
        set
        {
            SetProperty(ref m_CustomerID, value);
        }
    }

    protected bool m_NameStyle;
    [Display(Name = "NameStyle", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="NameStyle_is_required")]
    public bool NameStyle
    {
        get => m_NameStyle;
        set
        {
            SetProperty(ref m_NameStyle, value);
        }
    }

    protected string? m_Title;
    [Display(Name = "Title", ResourceType = typeof(UIStrings))]
    [StringLength(8, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Title_should_be_0_to_8")]
    public string? Title
    {
        get => m_Title;
        set
        {
            SetProperty(ref m_Title, value);
            OnPropertyChanged(nameof(Avatar__));
        }
    }

    protected string m_FirstName;
    [Display(Name = "FirstName", ResourceType = typeof(UIStrings))]
    [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_FirstName_should_be_1_to_50", MinimumLength = 1)]
    public string FirstName
    {
        get => m_FirstName;
        set
        {
            SetProperty(ref m_FirstName, value);
        }
    }

    protected string? m_MiddleName;
    [Display(Name = "MiddleName", ResourceType = typeof(UIStrings))]
    [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_MiddleName_should_be_0_to_50")]
    public string? MiddleName
    {
        get => m_MiddleName;
        set
        {
            SetProperty(ref m_MiddleName, value);
        }
    }

    protected string m_LastName;
    [Display(Name = "LastName", ResourceType = typeof(UIStrings))]
    [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_LastName_should_be_1_to_50", MinimumLength = 1)]
    public string LastName
    {
        get => m_LastName;
        set
        {
            SetProperty(ref m_LastName, value);
        }
    }

    protected string? m_Suffix;
    [Display(Name = "Suffix", ResourceType = typeof(UIStrings))]
    [StringLength(10, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Suffix_should_be_0_to_10")]
    public string? Suffix
    {
        get => m_Suffix;
        set
        {
            SetProperty(ref m_Suffix, value);
        }
    }

    protected string? m_CompanyName;
    [Display(Name = "CompanyName", ResourceType = typeof(UIStrings))]
    [StringLength(128, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_CompanyName_should_be_0_to_128")]
    public string? CompanyName
    {
        get => m_CompanyName;
        set
        {
            SetProperty(ref m_CompanyName, value);
        }
    }

    protected string? m_SalesPerson;
    [Display(Name = "SalesPerson", ResourceType = typeof(UIStrings))]
    [StringLength(256, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_SalesPerson_should_be_0_to_256")]
    public string? SalesPerson
    {
        get => m_SalesPerson;
        set
        {
            SetProperty(ref m_SalesPerson, value);
        }
    }

    protected string? m_EmailAddress;
    [Display(Name = "EmailAddress", ResourceType = typeof(UIStrings))]
    [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_EmailAddress_should_be_0_to_50")]
    public string? EmailAddress
    {
        get => m_EmailAddress;
        set
        {
            SetProperty(ref m_EmailAddress, value);
        }
    }

    protected string? m_Phone;
    [Display(Name = "Phone", ResourceType = typeof(UIStrings))]
    [StringLength(25, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Phone_should_be_0_to_25")]
    public string? Phone
    {
        get => m_Phone;
        set
        {
            SetProperty(ref m_Phone, value);
        }
    }

    protected string m_PasswordHash;
    [Display(Name = "PasswordHash", ResourceType = typeof(UIStrings))]
    [StringLength(128, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_PasswordHash_should_be_1_to_128", MinimumLength = 1)]
    public string PasswordHash
    {
        get => m_PasswordHash;
        set
        {
            SetProperty(ref m_PasswordHash, value);
        }
    }

    protected string m_PasswordSalt;
    [Display(Name = "PasswordSalt", ResourceType = typeof(UIStrings))]
    [StringLength(10, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_PasswordSalt_should_be_1_to_10", MinimumLength = 1)]
    public string PasswordSalt
    {
        get => m_PasswordSalt;
        set
        {
            SetProperty(ref m_PasswordSalt, value);
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

    public CustomerDataModel Clone()
    {
        return new CustomerDataModel
        {
            m_ItemUIStatus______ = m_ItemUIStatus______,
            m_IsDeleted______ = m_IsDeleted______,
            m_CustomerID = m_CustomerID,
            m_NameStyle = m_NameStyle,
            m_Title = m_Title,
            m_FirstName = m_FirstName,
            m_MiddleName = m_MiddleName,
            m_LastName = m_LastName,
            m_Suffix = m_Suffix,
            m_CompanyName = m_CompanyName,
            m_SalesPerson = m_SalesPerson,
            m_EmailAddress = m_EmailAddress,
            m_Phone = m_Phone,
            m_PasswordHash = m_PasswordHash,
            m_PasswordSalt = m_PasswordSalt,
            m_rowguid = m_rowguid,
            m_ModifiedDate = m_ModifiedDate,
        };
    }

    public void CopyTo(CustomerDataModel destination)
    {
        destination.ItemUIStatus______ = ItemUIStatus______;
        destination.IsDeleted______ = IsDeleted______;
        destination.CustomerID = CustomerID;
        destination.NameStyle = NameStyle;
        destination.Title = Title;
        destination.FirstName = FirstName;
        destination.MiddleName = MiddleName;
        destination.LastName = LastName;
        destination.Suffix = Suffix;
        destination.CompanyName = CompanyName;
        destination.SalesPerson = SalesPerson;
        destination.EmailAddress = EmailAddress;
        destination.Phone = Phone;
        destination.PasswordHash = PasswordHash;
        destination.PasswordSalt = PasswordSalt;
        destination.rowguid = rowguid;
        destination.ModifiedDate = ModifiedDate;
    }
}

