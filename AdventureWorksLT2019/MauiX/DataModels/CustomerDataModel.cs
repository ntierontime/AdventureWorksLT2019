namespace AdventureWorksLT2019.MauiX.DataModels
{
    public class CustomerDataModel: Framework.MauiX.PropertyChangedNotifier
	{
		protected Framework.Common.ItemUIStatus m_ItemUIStatus______;
		public Framework.Common.ItemUIStatus ItemUIStatus______
        {
            get
            {
                return m_ItemUIStatus______;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(ItemUIStatus______), ref m_ItemUIStatus______, value);
            }
        }
		protected System.Boolean m_IsDeleted______;
		public System.Boolean IsDeleted______
        {
            get
            {
                return m_IsDeleted______;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(IsDeleted______), ref m_IsDeleted______, value);
            }
        }

        protected System.Int32 m_CustomerID;
		public System.Int32 CustomerID
        {
            get
            {
                return m_CustomerID;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(CustomerID), ref m_CustomerID, value);
            }
        }
		protected System.Boolean m_NameStyle;
		public System.Boolean NameStyle
        {
            get
            {
                return m_NameStyle;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(NameStyle), ref m_NameStyle, value);
            }
        }
		protected System.String m_Title;
		public System.String Title
        {
            get
            {
                return m_Title;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(Title), ref m_Title, value);
            }
        }
		protected System.String m_FirstName;
		public System.String FirstName
        {
            get
            {
                return m_FirstName;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(FirstName), ref m_FirstName, value);
            }
        }
		protected System.String m_MiddleName;
		public System.String MiddleName
        {
            get
            {
                return m_MiddleName;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(MiddleName), ref m_MiddleName, value);
            }
        }
		protected System.String m_LastName;
		public System.String LastName
        {
            get
            {
                return m_LastName;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(LastName), ref m_LastName, value);
            }
        }
		protected System.String m_Suffix;
		public System.String Suffix
        {
            get
            {
                return m_Suffix;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(Suffix), ref m_Suffix, value);
            }
        }
		protected System.String m_CompanyName;
		public System.String CompanyName
        {
            get
            {
                return m_CompanyName;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(CompanyName), ref m_CompanyName, value);
            }
        }
		protected System.String m_SalesPerson;
		public System.String SalesPerson
        {
            get
            {
                return m_SalesPerson;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(SalesPerson), ref m_SalesPerson, value);
            }
        }
		protected System.String m_EmailAddress;
		public System.String EmailAddress
        {
            get
            {
                return m_EmailAddress;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(EmailAddress), ref m_EmailAddress, value);
            }
        }
		protected System.String m_Phone;
		public System.String Phone
        {
            get
            {
                return m_Phone;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(Phone), ref m_Phone, value);
            }
        }
		protected System.String m_PasswordHash;
		public System.String PasswordHash
        {
            get
            {
                return m_PasswordHash;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(PasswordHash), ref m_PasswordHash, value);
            }
        }
		protected System.String m_PasswordSalt;
		public System.String PasswordSalt
        {
            get
            {
                return m_PasswordSalt;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(PasswordSalt), ref m_PasswordSalt, value);
            }
        }
		protected System.Guid m_rowguid;
		public System.Guid rowguid
        {
            get
            {
                return m_rowguid;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(rowguid), ref m_rowguid, value);
            }
        }
		protected System.DateTime m_ModifiedDate;
		public System.DateTime ModifiedDate
        {
            get
            {
                return m_ModifiedDate;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(ModifiedDate), ref m_ModifiedDate, value);
            }
        }
	}
}
