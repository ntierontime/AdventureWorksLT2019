using Framework.Models;
using Framework.Common;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{

    public class ProductModelProductDescriptionIdentifier
    {

        // PredicateType:Equals
        public int? ProductModelID { get; set; }

        // PredicateType:Equals
        public int? ProductDescriptionID { get; set; }

        // PredicateType:Equals
        public string? Culture { get; set; }
    }

    public class ProductModelProductDescriptionAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Equals
        public int? ProductDescriptionID { get; set; }

        // PredicateType:Equals
        public int? ProductModelID { get; set; }

        public string? ModifiedDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeUpper { get; set; }

        // PredicateType:Contains
        public string? Culture { get; set; }
        public TextSearchTypes CultureSearchType { get; set; } = TextSearchTypes.Contains;
    }
}

