using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{

    public class CustomerAddressIdentifier
    {
        // PredicateType:Equals
        public int? CustomerID { get; set; }
        // PredicateType:Equals
        public int? AddressID { get; set; }
    }

    public class CustomerAddressAdvancedQuery: BaseQuery
    {
        // PredicateType:Equals
        public int? AddressID { get; set; }
        // PredicateType:Equals
        public int? CustomerID { get; set; }
        public string? ModifiedDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeUpper { get; set; }
        // PredicateType:Contains
        public string? AddressType { get; set; }
        public TextSearchTypes AddressTypeSearchType { get; set; } = TextSearchTypes.Contains;
    }
}

