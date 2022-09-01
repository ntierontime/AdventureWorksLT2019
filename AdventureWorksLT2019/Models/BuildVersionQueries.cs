using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{

    public class BuildVersionIdentifier
    {

        // PredicateType:Equals
        public byte? SystemInformationID { get; set; }

        // PredicateType:Equals
        public System.DateTime? VersionDate { get; set; }

        // PredicateType:Equals
        public System.DateTime? ModifiedDate { get; set; }
    }

    public class BuildVersionAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

        public string? VersionDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? VersionDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? VersionDateRangeUpper { get; set; }

        public string? ModifiedDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? ModifiedDateRangeUpper { get; set; }

        // PredicateType:Contains
        public string? Database_Version { get; set; }
        public TextSearchTypes Database_VersionSearchType { get; set; } = TextSearchTypes.Contains;
    }
}

