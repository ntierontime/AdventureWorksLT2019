using Framework.Models;
using Framework.Common;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{

    public class ProductIdentifier
    {

        // PredicateType:Equals
        public int? ProductID { get; set; }
    }

    public class ProductAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Equals
        public int? ProductCategoryID { get; set; }

        // PredicateType:Equals
        public int? ParentID { get; set; }

        // PredicateType:Equals
        public int? ProductModelID { get; set; }

        public string? SellStartDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? SellStartDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? SellStartDateRangeUpper { get; set; }

        public string? SellEndDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? SellEndDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? SellEndDateRangeUpper { get; set; }

        public string? DiscontinuedDateRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? DiscontinuedDateRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? DiscontinuedDateRangeUpper { get; set; }

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

        // PredicateType:Contains
        public string? ProductNumber { get; set; }
        public TextSearchTypes ProductNumberSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? Color { get; set; }
        public TextSearchTypes ColorSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? Size { get; set; }
        public TextSearchTypes SizeSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? ThumbnailPhotoFileName { get; set; }
        public TextSearchTypes ThumbnailPhotoFileNameSearchType { get; set; } = TextSearchTypes.Contains;
    }
}

