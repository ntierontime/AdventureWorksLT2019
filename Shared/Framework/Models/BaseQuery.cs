using System.ComponentModel.DataAnnotations;

namespace Framework.Models
{
    public class ExtraBaseQuery
    {
        /// <summary>
        /// From <see cref="ClaimsModel.PersonID"/>
        /// Role = "Visitor" when null
        /// Used to load CurrentLogInPerson related data,
        /// e.g. ConsumerOnEntity, ConsumerOnItem, ConsumerOnEntityAlbumItem
        /// </summary>
        public long? CurrentLogInPersonID { get; set; } = null;

        // PredicateType:GeographyRange - ReferencePoint
        public NetTopologySuite.Geometries.Geometry? SpatialLocation { get; set; }
        // PredicateType:GeographyRange - Radius
        public long? SpatialLocationRadius { get; set; }
        // PredicateType:GeographyIntersects
        public NetTopologySuite.Geometries.Geometry? SpatialLocationGeographyIntersects { get; set; }
    }

    public class BaseQuery: ExtraBaseQuery
    {
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

        public string? DateTimeRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? DateTimeRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? DateTimeRangeUpper { get; set; }

        public int PageSize { get; set; } = 10; // default 10 items per pages
        public int PageIndex { get; set; } = 1; // start from 1
        public string? OrderBys { get; set; }

        public PaginationOptions PaginationOption { get; set; } = PaginationOptions.Paged;
    }
}

