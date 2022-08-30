using Framework.Models;
using Framework.Common;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{

    public class SalesOrderDetailIdentifier
    {

        // PredicateType:Equals
        public int? SalesOrderID { get; set; }

        // PredicateType:Equals
        public int? SalesOrderDetailID { get; set; }
    }

    public class SalesOrderDetailAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Equals
        public int? ProductID { get; set; }

        // PredicateType:Equals
        public int? ProductCategoryID { get; set; }

        // PredicateType:Equals
        public int? ProductCategory_ParentID { get; set; }

        // PredicateType:Equals
        public int? ProductModelID { get; set; }

        // PredicateType:Equals
        public int? SalesOrderID { get; set; }

        // PredicateType:Equals
        public int? BillToID { get; set; }

        // PredicateType:Equals
        public int? ShipToID { get; set; }

        // PredicateType:Equals
        public int? CustomerID { get; set; }

        public string? ModifiedDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeUpper { get; set; }
    }
}

