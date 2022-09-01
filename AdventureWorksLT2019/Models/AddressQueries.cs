using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{

    public class AddressIdentifier
    {

        // PredicateType:Equals
        public int? AddressID { get; set; }
    }

    public class AddressAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

        public string? ModifiedDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeUpper { get; set; }

        // PredicateType:Contains
        public string? AddressLine1 { get; set; }
        public TextSearchTypes AddressLine1SearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? AddressLine2 { get; set; }
        public TextSearchTypes AddressLine2SearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? City { get; set; }
        public TextSearchTypes CitySearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? StateProvince { get; set; }
        public TextSearchTypes StateProvinceSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? CountryRegion { get; set; }
        public TextSearchTypes CountryRegionSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? PostalCode { get; set; }
        public TextSearchTypes PostalCodeSearchType { get; set; } = TextSearchTypes.Contains;
    }
}

