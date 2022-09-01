using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{

    public class ErrorLogIdentifier
    {

        // PredicateType:Equals
        public int? ErrorLogID { get; set; }
    }

    public class ErrorLogAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

        public string? ErrorTimeRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ErrorTimeRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ErrorTimeRangeUpper { get; set; }

        // PredicateType:Contains
        public string? UserName { get; set; }
        public TextSearchTypes UserNameSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? ErrorProcedure { get; set; }
        public TextSearchTypes ErrorProcedureSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? ErrorMessage { get; set; }
        public TextSearchTypes ErrorMessageSearchType { get; set; } = TextSearchTypes.Contains;
    }
}

