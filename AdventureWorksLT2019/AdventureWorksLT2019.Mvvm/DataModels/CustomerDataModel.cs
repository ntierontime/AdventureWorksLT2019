using Framework.Mvvm.Models;

namespace AdventureWorksLT2019.Mvvm.DataModels
{
    public class CustomerDataModel: Framework.Mvvm.Models.PropertyChangedNotifier
	{
		protected Framework.Models.ItemUIStatus _ItemUIStatus______;
		public Framework.Models.ItemUIStatus ItemUIStatus______
        {
            get
            {
                return _ItemUIStatus______;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(ItemUIStatus______), ref _ItemUIStatus______, value);
            }
        }
		protected System.Boolean _IsDeleted______;
		public System.Boolean IsDeleted______
        {
            get
            {
                return _IsDeleted______;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(IsDeleted______), ref _IsDeleted______, value);
            }
        }

        protected System.Int32 _CustomerID;
		public System.Int32 CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(CustomerID), ref _CustomerID, value);
            }
        }
		protected System.Boolean _NameStyle;
		public System.Boolean NameStyle
        {
            get
            {
                return _NameStyle;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(NameStyle), ref _NameStyle, value);
            }
        }
		protected System.String? _Title;
		public System.String? Title
        {
            get
            {
                return _Title;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(Title), ref _Title, value);
            }
        }
		protected System.String? _FirstName;
		public System.String? FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(FirstName), ref _FirstName, value);
            }
        }
		protected System.String? _MiddleName;
		public System.String? MiddleName
        {
            get
            {
                return _MiddleName;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(MiddleName), ref _MiddleName, value);
            }
        }
		protected System.String? _LastName;
		public System.String? LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(LastName), ref _LastName, value);
            }
        }
		protected System.String? _Suffix;
		public System.String? Suffix
        {
            get
            {
                return _Suffix;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(Suffix), ref _Suffix, value);
            }
        }
		protected System.String? _CompanyName;
		public System.String? CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(CompanyName), ref _CompanyName, value);
            }
        }
		protected System.String? _SalesPerson;
		public System.String? SalesPerson
        {
            get
            {
                return _SalesPerson;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(SalesPerson), ref _SalesPerson, value);
            }
        }
		protected System.String? _EmailAddress;
		public System.String? EmailAddress
        {
            get
            {
                return _EmailAddress;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(EmailAddress), ref _EmailAddress, value);
            }
        }
		protected System.String? _Phone;
		public System.String? Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(Phone), ref _Phone, value);
            }
        }
		protected System.String? _PasswordHash;
		public System.String? PasswordHash
        {
            get
            {
                return _PasswordHash;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(PasswordHash), ref _PasswordHash, value);
            }
        }
		protected System.String? _PasswordSalt;
		public System.String? PasswordSalt
        {
            get
            {
                return _PasswordSalt;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(PasswordSalt), ref _PasswordSalt, value);
            }
        }
		protected System.Guid _rowguid;
		public System.Guid rowguid
        {
            get
            {
                return _rowguid;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(rowguid), ref _rowguid, value);
            }
        }
		protected System.DateTime _ModifiedDate;
		public System.DateTime ModifiedDate
        {
            get
            {
                return _ModifiedDate;
            }
            set
            {
				//ValidateProperty(value);
                Set(nameof(ModifiedDate), ref _ModifiedDate, value);
            }
        }
	}
}
