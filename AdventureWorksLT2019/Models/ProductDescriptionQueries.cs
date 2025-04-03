using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{

    public class ProductDescriptionIdentifier
    {
        // PredicateType:Equals
        public int? ProductDescriptionID { get; set; }
    }

    public class ProductDescriptionAdvancedQuery: BaseQuery
    {
        public string? ModifiedDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeUpper { get; set; }
        // PredicateType:Contains
        public string? Description { get; set; }
        public TextSearchTypes DescriptionSearchType { get; set; } = TextSearchTypes.Contains;
    }
}

