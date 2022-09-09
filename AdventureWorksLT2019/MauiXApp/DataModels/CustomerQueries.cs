using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels
{
    public class CustomerIdentifier
    {

        // PredicateType:Equals
        public int? CustomerID { get; set; }

        public string GetWebApiRoute()
        {
            return $"{CustomerID}";
        }
    }

    public class CustomerAdvancedQuery: Framework.MauiX.DataModels.ObservableBaseQuery
    {
        // will query all text columns in this table, ||
        public string TextSearch { get; set; }
        public Framework.Models.TextSearchTypes TextSearchType { get; set; } = Framework.Models.TextSearchTypes.Contains;

        // PredicateType:Equals
        public bool? NameStyle { get; set; }

        public string ModifiedDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeUpper { get; set; }

        // PredicateType:Contains
        public string Title { get; set; }
        public Framework.Models.TextSearchTypes TitleSearchType { get; set; } = Framework.Models.TextSearchTypes.Contains;

        // PredicateType:Contains
        public string FirstName { get; set; }
        public Framework.Models.TextSearchTypes FirstNameSearchType { get; set; } = Framework.Models.TextSearchTypes.Contains;

        // PredicateType:Contains
        public string MiddleName { get; set; }
        public Framework.Models.TextSearchTypes MiddleNameSearchType { get; set; } = Framework.Models.TextSearchTypes.Contains;

        // PredicateType:Contains
        public string LastName { get; set; }
        public Framework.Models.TextSearchTypes LastNameSearchType { get; set; } = Framework.Models.TextSearchTypes.Contains;

        // PredicateType:Contains
        public string Suffix { get; set; }
        public Framework.Models.TextSearchTypes SuffixSearchType { get; set; } = Framework.Models.TextSearchTypes.Contains;

        // PredicateType:Contains
        public string CompanyName { get; set; }
        public Framework.Models.TextSearchTypes CompanyNameSearchType { get; set; } = Framework.Models.TextSearchTypes.Contains;

        // PredicateType:Contains
        public string SalesPerson { get; set; }
        public Framework.Models.TextSearchTypes SalesPersonSearchType { get; set; } = Framework.Models.TextSearchTypes.Contains;

        // PredicateType:Contains
        public string EmailAddress { get; set; }
        public Framework.Models.TextSearchTypes EmailAddressSearchType { get; set; } = Framework.Models.TextSearchTypes.Contains;

        // PredicateType:Contains
        public string Phone { get; set; }
        public Framework.Models.TextSearchTypes PhoneSearchType { get; set; } = Framework.Models.TextSearchTypes.Contains;

        // PredicateType:Contains
        public string PasswordHash { get; set; }
        public Framework.Models.TextSearchTypes PasswordHashSearchType { get; set; } = Framework.Models.TextSearchTypes.Contains;

        // PredicateType:Contains
        public string PasswordSalt { get; set; }
        public Framework.Models.TextSearchTypes PasswordSaltSearchType { get; set; } = Framework.Models.TextSearchTypes.Contains;
    }
}

