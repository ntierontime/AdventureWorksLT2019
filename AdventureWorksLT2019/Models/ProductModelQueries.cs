using Framework.Models;
using Framework.Common;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{

    public class ProductModelIdentifier
    {

        // PredicateType:Equals
        public int? ProductModelID { get; set; }
    }

    public class ProductModelAdvancedQuery: BaseQuery
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
        public string? Name { get; set; }
        public TextSearchTypes NameSearchType { get; set; } = TextSearchTypes.Contains;
    }
}

