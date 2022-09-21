using Framework.Models;
using AdventureWorksLT2019.Resx.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class CustomerAddressDataModel : ObservableValidator, IClone<CustomerAddressDataModel>, ICopyTo<CustomerAddressDataModel>
{
    public string Avatar__
    {
        get => GetAvatar();
    }

    private string GetAvatar()
    {
        if (string.IsNullOrEmpty(AddressType) || AddressType.Length == 0)
            return "?";
        if (AddressType.Length == 1)
            return AddressType[..1];
        return AddressType[..2];
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

    protected int m_AddressID;
    [Display(Name = "Address", ResourceType = typeof(UIStrings))]
    public int AddressID
    {
        get => m_AddressID;
        set
        {
            SetProperty(ref m_AddressID, value);
        }
    }

    protected string m_AddressType;
    [Display(Name = "AddressType", ResourceType = typeof(UIStrings))]
    [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_AddressType_should_be_1_to_50", MinimumLength = 1)]
    public string AddressType
    {
        get => m_AddressType;
        set
        {
            SetProperty(ref m_AddressType, value);
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

    protected string m_Address_Name;
    [Display(Name = "AddressLine1", ResourceType = typeof(UIStrings))]
    public string Address_Name
    {
        get => m_Address_Name;
        set
        {
            SetProperty(ref m_Address_Name, value);
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

    public CustomerAddressDataModel Clone()
    {
        return new CustomerAddressDataModel
        {
            m_ItemUIStatus______ = m_ItemUIStatus______,
            m_IsDeleted______ = m_IsDeleted______,
            m___DBId__ = m___DBId__,
            m_CustomerID = m_CustomerID,
            m_AddressID = m_AddressID,
            m_AddressType = m_AddressType,
            m_rowguid = m_rowguid,
            m_ModifiedDate = m_ModifiedDate,
            m_Address_Name = m_Address_Name,
            m_Customer_Name = m_Customer_Name,
        };
    }

    public void CopyTo(CustomerAddressDataModel destination)
    {
        destination.ItemUIStatus______ = ItemUIStatus______;
        destination.IsDeleted______ = IsDeleted______;
        destination.__DBId__ = __DBId__;
        destination.CustomerID = CustomerID;
        destination.AddressID = AddressID;
        destination.AddressType = AddressType;
        destination.rowguid = rowguid;
        destination.ModifiedDate = ModifiedDate;
        destination.Address_Name = Address_Name;
        destination.Customer_Name = Customer_Name;
    }
}

