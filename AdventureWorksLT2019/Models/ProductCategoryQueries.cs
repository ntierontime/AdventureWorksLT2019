using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{

    public class ProductCategoryIdentifier
    {
        // PredicateType:Equals
        public int? ProductCategoryID { get; set; }
    }

    public class ProductCategoryAdvancedQuery: BaseQuery
    {
        // PredicateType:Equals
        public int? ParentProductCategoryID { get; set; }
        public string? ModifiedDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeUpper { get; set; }
        // PredicateType:Contains
        public string? Name { get; set; }
        public TextSearchTypes NameSearchType { get; set; } = TextSearchTypes.Contains;
    }
}

