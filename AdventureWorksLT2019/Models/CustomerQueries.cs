using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{

    public class CustomerIdentifier
    {

        // PredicateType:Equals
        public int? CustomerID { get; set; }
    }

    public class CustomerAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Equals
        public bool? NameStyle { get; set; }

        public string? ModifiedDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeUpper { get; set; }

        // PredicateType:Contains
        public string? Title { get; set; }
        public TextSearchTypes TitleSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? FirstName { get; set; }
        public TextSearchTypes FirstNameSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? MiddleName { get; set; }
        public TextSearchTypes MiddleNameSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? LastName { get; set; }
        public TextSearchTypes LastNameSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? Suffix { get; set; }
        public TextSearchTypes SuffixSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? CompanyName { get; set; }
        public TextSearchTypes CompanyNameSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? SalesPerson { get; set; }
        public TextSearchTypes SalesPersonSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? EmailAddress { get; set; }
        public TextSearchTypes EmailAddressSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? Phone { get; set; }
        public TextSearchTypes PhoneSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? PasswordHash { get; set; }
        public TextSearchTypes PasswordHashSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? PasswordSalt { get; set; }
        public TextSearchTypes PasswordSaltSearchType { get; set; } = TextSearchTypes.Contains;
    }
}

