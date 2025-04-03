using Framework.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Framework.Mvc
{
    public class ExtraApiBaseQuery
    {
        // PredicateType:GeographyRange - ReferencePoint
        public string? SpatialLocation { get; set; }
        // PredicateType:GeographyRange - Radius
        public long? SpatialLocationRadius { get; set; }
        // PredicateType:GeographyIntersects
        public string? SpatialLocationGeographyIntersects { get; set; }
    }

    /// <summary>
    /// all values comes from QueryString
    /// web api route parameters, [FromRoute], will be valuetype parameters of the search method.
    /// </summary>
    public class ApiBaseQuery
    {
        public string? TextSearch { get; set; }

        public int PageSize { get; set; } = 10; // default 10 items per pages
        public int PageIndex { get; set; } = 1; // start from 1
        public string? OrderBys { get; set; }

        public PaginationOptions PaginationOption { get; set; } = PaginationOptions.Paged;

        public static NetTopologySuite.Geometries.Point? ParsePoint(string? latLon)
        {
            if (string.IsNullOrEmpty(latLon))
            {
                return null;
            }
            var split = latLon.Split(',').Where(t=>!string.IsNullOrEmpty(t) && !string.IsNullOrEmpty(t.Trim())).ToArray();
            if(split == null || split.Count() != 2)
            {
                return null;
            }
            double lat = 0, lon = 0;
            if (double.TryParse(split![0], out lat) && double.TryParse(split![1], out lon))
            {
                if (lat < -90 || lat > 90)
                    return null;
                if (lon < -180 || lon > 180)
                    return null;
            }
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            return geometryFactory.CreatePoint(new Coordinate(lon, lat));
        }

        public static NetTopologySuite.Geometries.Polygon? ParsePolygon(string? polygon)
        {
            if (string.IsNullOrEmpty(polygon))
                return null;
            var split = polygon.Split(';');
            if (split.Length < 3)
                return null;

            var splitAgain =
                from t in split
                let t_split = t.Split(",").Where(t => !string.IsNullOrEmpty(t)).ToArray()
                select t_split;
            if (splitAgain.Any(t => t.Count() != 2))
            {
                return null;
            }
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var points = new List<NetTopologySuite.Geometries.Coordinate>();
            foreach (var t_split in splitAgain) // t_split.length == 2
            {
                double latitue = 0.0, longitude = 0.0;
                if (double.TryParse(t_split[0], out latitue) && double.TryParse(t_split[1], out longitude))
                {
                    var point = new NetTopologySuite.Geometries.Coordinate(latitue, longitude);
                    points.Add(point);
                }
                else
                    return null;
            }

            var result = new NetTopologySuite.Geometries.Polygon(geometryFactory.CreateLinearRing(points.ToArray()));
            return result;

        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T">integer, decimal, or datetime</typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T? Parse<T>(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return default(T?);

            try
            {
                var result = (T)System.Convert.ChangeType(input, typeof(T));
                return result;
            }
            catch // (Exception ex)
            { return default(T?); }
        }

        /// <summary>
        /// to parse integer, decimal, or datetime array in query string
        /// delimiter is ","
        /// </summary>
        /// <typeparam name="T">integer, decimal, or datetime</typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T[]? ParseArray<T>(string? input)
            where T : struct
        {
            if (string.IsNullOrEmpty(input))
                return default(T[]?);

            var resultList = input.Split(',').Select(t => new { t, tParsed = Parse<T>(t) }).Where(t => t.t == t.tParsed.ToString() || !t.tParsed.Equals(default(T))).Select(t => t.tParsed).ToArray();
            return resultList;
        }

        /// <summary>
        /// to parse Guid array in query string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Guid[]? ParseGuidArray(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return default(Guid[]?);

            var resultList = input.Split(',').Select(t => ParseGuid(t)).Where(t => t.HasValue).Select(t => t!.Value).ToArray();
            return resultList;
        }

        public static Guid? ParseGuid(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            try
            {
                if(Guid.TryParse(input, out Guid result))
                {
                    return result;
                }
                return null;
            }
            catch // (Exception ex)
            { return null; }
        }
    }
}

