using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class CustomerDataModel : ObservableValidator, Framework.Models.IClone<CustomerDataModel>, Framework.Models.ICopyTo<CustomerDataModel>
{
    private string GetAvatar()
    {
        return (string.IsNullOrEmpty(FirstName) ? "" : FirstName[0].ToString()) + (string.IsNullOrEmpty(LastName) ? "" : LastName[0].ToString());
    }



    public string Avatar__
    {
        get
        {
            return GetAvatar();
        }
    }

    protected Framework.Models.ItemUIStatus m_ItemUIStatus______;
    public Framework.Models.ItemUIStatus ItemUIStatus______
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

    protected System.Int32 m_CustomerID;
    [PrimaryKey]
    public System.Int32 CustomerID
    {
        get => m_CustomerID;
        set => SetProperty(ref m_CustomerID, value);
    }
    protected System.Boolean? m_NameStyle;
    public System.Boolean? NameStyle
    {
        get => m_NameStyle;
        set => SetProperty(ref m_NameStyle, value);
    }

    protected System.String m_Title;
    public System.String Title
    {
        get => m_Title;
        set => SetProperty(ref m_Title, value);
    }

    protected System.String m_FirstName;
    public System.String FirstName
    {
        get => m_FirstName;
        set
        {
            SetProperty(ref m_FirstName, value);
            OnPropertyChanged(nameof(Avatar__));
        }
    }
    protected System.String m_MiddleName;
    public System.String MiddleName
    {
        get => m_MiddleName;
        set => SetProperty(ref m_MiddleName, value);
    }
    protected System.String m_LastName;
    public System.String LastName
    {
        get => m_LastName;
        set
        {
            SetProperty(ref m_LastName, value);
            OnPropertyChanged(nameof(Avatar__));
        }
    }

    protected System.String m_Suffix;
    public System.String Suffix
    {
        get => m_Suffix;
        set => SetProperty(ref m_Suffix, value);
    }
    protected System.String m_CompanyName;
    public System.String CompanyName
    {
        get => m_CompanyName;
        set => SetProperty(ref m_CompanyName, value);
    }
    protected System.String m_SalesPerson;
    public System.String SalesPerson
    {
        get => m_SalesPerson;
        set => SetProperty(ref m_SalesPerson, value);
    }
    protected System.String m_EmailAddress;
    public System.String EmailAddress
    {
        get => m_EmailAddress;
        set => SetProperty(ref m_EmailAddress, value);
    }
    protected System.String m_Phone;
    public System.String Phone
    {
        get => m_Phone;
        set => SetProperty(ref m_Phone, value);
    }
    protected System.String m_PasswordHash;
    public System.String PasswordHash
    {
        get => m_PasswordHash;
        set => SetProperty(ref m_PasswordHash, value);
    }
    protected System.String m_PasswordSalt;
    public System.String PasswordSalt
    {
        get => m_PasswordSalt;
        set => SetProperty(ref m_PasswordSalt, value);
    }
    protected System.Guid m_rowguid;
    public System.Guid rowguid
    {
        get => m_rowguid;
        set => SetProperty(ref m_rowguid, value);
    }
    protected System.DateTime m_ModifiedDate;
    public System.DateTime ModifiedDate
    {
        get => m_ModifiedDate;
        set => SetProperty(ref m_ModifiedDate, value);
    }

    public CustomerDataModel Clone()
    {
        return new CustomerDataModel
        {
            m_ItemUIStatus______ = m_ItemUIStatus______,
            m_IsDeleted______ = m_IsDeleted______,
            m_CustomerID = m_CustomerID,
            m_FirstName = m_FirstName,
            m_LastName = m_LastName,
            m_CompanyName = m_CompanyName,
            m_EmailAddress = m_EmailAddress,
            m_MiddleName = m_MiddleName,
            m_ModifiedDate = m_ModifiedDate,
            m_NameStyle = m_NameStyle,
            m_PasswordHash = m_PasswordHash,
            m_PasswordSalt = m_PasswordSalt,
            m_Phone = m_Phone,
            m_rowguid = m_rowguid,
            m_SalesPerson = m_SalesPerson,
            m_Suffix = m_Suffix,
            m_Title = m_Title,
        };
    }

    public void CopyTo(CustomerDataModel destination)
    {
        destination.ItemUIStatus______ = ItemUIStatus______;
        destination.IsDeleted______ = IsDeleted______;
        destination.CustomerID = CustomerID;
        destination.FirstName = FirstName;
        destination.LastName = LastName;
        destination.CompanyName = CompanyName;
        destination.EmailAddress = EmailAddress;
        destination.MiddleName = MiddleName;
        destination.ModifiedDate = ModifiedDate;
        destination.NameStyle = NameStyle;
        destination.PasswordHash = PasswordHash;
        destination.PasswordSalt = PasswordSalt;
        destination.Phone = Phone;
        destination.rowguid = rowguid;
        destination.SalesPerson = SalesPerson;
        destination.Suffix = Suffix;
        destination.Title = Title;
    }
}
