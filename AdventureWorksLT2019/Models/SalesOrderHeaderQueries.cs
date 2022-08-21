using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{

    public class SalesOrderHeaderIdentifier
    {

        // PredicateType:Equals
        public int? SalesOrderID { get; set; }
    }

    public class SalesOrderHeaderAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Equals
        public int? BillToAddressID { get; set; }

        // PredicateType:Equals
        public int? ShipToAddressID { get; set; }

        // PredicateType:Equals
        public int? CustomerID { get; set; }

        // PredicateType:Equals
        public bool? OnlineOrderFlag { get; set; }

        public string? OrderDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? OrderDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? OrderDateRangeUpper { get; set; }

        public string? DueDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? DueDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? DueDateRangeUpper { get; set; }

        public string? ShipDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ShipDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ShipDateRangeUpper { get; set; }

        public string? ModifiedDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeUpper { get; set; }

        // PredicateType:Contains
        public string? SalesOrderNumber { get; set; }
        public TextSearchTypes SalesOrderNumberSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? PurchaseOrderNumber { get; set; }
        public TextSearchTypes PurchaseOrderNumberSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? AccountNumber { get; set; }
        public TextSearchTypes AccountNumberSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? ShipMethod { get; set; }
        public TextSearchTypes ShipMethodSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? CreditCardApprovalCode { get; set; }
        public TextSearchTypes CreditCardApprovalCodeSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? Comment { get; set; }
        public TextSearchTypes CommentSearchType { get; set; } = TextSearchTypes.Contains;
    }
}

