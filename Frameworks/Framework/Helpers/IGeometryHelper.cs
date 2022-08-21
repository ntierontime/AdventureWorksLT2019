using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite.IO.GML2;
using Newtonsoft.Json;
using System.Text;

namespace Framework.Helpers
{
    public static class IGeometryHelper
    {
        public static Geometry FromGml(string gml)
        {
            var reader = new GMLReader();
            return reader.Read(gml);
        }

        private const string GmlV3_Format_Point = @"<Point xmlns=""http://www.opengis.net/gml""><pos>{0} {1}</pos></Point>";
        public static string ToGmlString(Geometry input)
        {
            if (input.GeometryType.ToLower() == "point")
            {
                var point = (Point)input;
                return string.Format(GmlV3_Format_Point, point.X, point.Y, point.Z == double.NaN ? null : " " + point.Z);
            }
            return String.Empty;

            // The current Gml is v3. The following toGml is using Gml v2.1.1 schema.
/*
            var gmlWriter = new GMLWriter();
            var ms = new MemoryStream();

            gmlWriter.Write(input, ms);
            return System.Text.Encoding.Default.GetString(ms.ToArray());
*/
        }

        static NetTopologySuite.Geometries.GeometryFactory _Factory = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        public static Geometry FromGeoJson(string geoJson)
        {
            Geometry result = new GeoJsonReader(_Factory, new JsonSerializerSettings()).Read<Geometry>(geoJson);
            //var featureCollection = new GeoJsonReader(_Factory, new JsonSerializerSettings()).Read<FeatureCollection>(geoJson);
            //result.SRID = featureCollection[0].Attributes[0]
            return result;
        }

        public static string ToGeoJson(Geometry input)
        {
            //AttributesTable attributes = new AttributesTable();
            //attributes.Add("test1", "value1");
            //IFeature feature = new Feature(input, attributes);
            //FeatureCollection featureCollection = new FeatureCollection(new Collection<IFeature> { feature })
            //{ CRS = new NamedCRS("name1") };
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            JsonSerializer serializer = GeoJsonSerializer.CreateDefault();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Serialize(writer, input);
            writer.Flush();

            return sb.ToString();
        }
    }
}

