using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Spatial;
using Newtonsoft.Json;

namespace Framework.Helpers
{
    public class GeoHelper
    {
        internal string _Version = "3";

        public void Init(string version)
        {
            _Version = version;
        }

        public string GeographyToGml(Geography geography)
        {
            if (_Version == "2")
                return GeographyToGmlV2(geography);
            else
                return GeographyToGmlV3(geography);
        }

        public string GeographyToGmlV3(Geography geography)
        {
            try
            {
                GmlFormatter gmlFormatter = GmlFormatter.Create();
                var result = gmlFormatter.Write(geography);
                result = result.Replace(@"gml:srsName=""http://www.opengis.net/def/crs/EPSG/0/4326""", "");
                return result;
            }
            catch
            {
                return String.Empty;
            }
        }

        private const string GmlV2_Format_Point =
@"﻿<gml:Point xmlns:gml=""http://www.opengis.net/gml"">
  <gml:coord>
    <gml:X>{0}</gml:X>
    <gml:Y>{1}</gml:Y>
  </gml:coord>
</gml:Point>";

        /// <summary>
        /// in order to compatible with NetTopologySuite.IO.GML2
        /// Geographies to GML v2.
        /// </summary>
        /// <param name="geography">The geography.</param>
        /// <returns></returns>
        public string GeographyToGmlV2(Geography geography)
        {
            if (geography == null || !(geography is GeographyPoint))
                return string.Empty;
            try
            {
                var geographyPoint = (GeographyPoint)geography;
                var result = string.Format(GmlV2_Format_Point, geographyPoint.Latitude, geographyPoint.Longitude, geographyPoint.Z);
                return result;
            }
            catch
            {
                return String.Empty;
            }
        }

        public Geography? GeographyFromGml(string gml)
        {
            try
            {
                GmlFormatter gmlFormatter = GmlFormatter.Create();
                TextReader reader = new StringReader(gml);
                var r = XmlReader.Create(reader);
                return gmlFormatter.Read<Geography>(r);
            }
            catch
            {
                return null;
            }
        }

        public string GeographyToGeoJSON<T>(T input)
            where T : class, ISpatial
        {
            var geoJsonConverter = new GeoJsonConverter<T>();
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                JsonSerializer serializer = new JsonSerializer();
                writer.Formatting = Newtonsoft.Json.Formatting.Indented;
                geoJsonConverter.WriteJson(writer, input, serializer);
                return sb.ToString();
            }
        }

        public T GeographyFromGeoJSON<T>(string input)
            where T : class, ISpatial
        {
            var geoJsonConverter = new GeoJsonConverter<T>();
            JsonTextReader reader = new JsonTextReader(new StringReader(input));
            JsonSerializer serializer = new JsonSerializer();
            var result = geoJsonConverter.ReadJson(reader, typeof(T), input, serializer);
            return (T)result;
        }

        public string GeographyToWKT<T>(T input)
            where T : class, ISpatial
        {

            var wktFormatter = WellKnownTextSqlFormatter.Create();
            var result = wktFormatter.Write(input);
            return result;
        }

        public T GeographyFromWKT<T>(string input)
            where T : class, ISpatial
        {
            var wktFormatter = WellKnownTextSqlFormatter.Create();
            TextReader reader = new StringReader(input);
            var result = wktFormatter.Read<T>(reader);
            return (T)result;
        }

        public string GetAddress(string addressLine1, string addressLine2, string city, string stateProvince, string countryRegion, string postalCode)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(addressLine1))
            {
                sb.Append(addressLine1);
                sb.Append(" ");
            }
            sb.Append(addressLine2);

            if (!string.IsNullOrEmpty(city))
            {
                sb.Append(", ");
                sb.Append(city);
            }

            if (!string.IsNullOrEmpty(stateProvince))
            {
                sb.Append(", ");
                sb.Append(stateProvince);
            }

            if (!string.IsNullOrEmpty(countryRegion))
            {
                sb.Append(", ");
                sb.Append(countryRegion);
            }

            if (!string.IsNullOrEmpty(postalCode))
            {
                sb.Append(" ");
                sb.Append(postalCode);
            }

            return sb.ToString();
        }
    }
    public class GeoHelperSinglton : Singleton<GeoHelper>
    {
    }
}

