using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Spatial;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Framework.Helpers
{
    /// <summary>
    /// http://blogs.microsoft.co.il/applisec/2014/06/03/spatial-support-in-web-api-and-odata/
    /// disabled some warnings in this file, because this is legacy code.
    /// </summary>
    /// <typeparam name="T"></typeparam>
#pragma warning disable CS8625, CS8600, CS8602, CS8765, CS8604, CS8601, CS8603
    public class GeoJsonConverter<T> : JsonConverter where T : class, ISpatial
    {
        public override bool CanConvert(Type objectType)
        {
#if WINDOWS
            return typeof(T).IsAssignableFrom(objectType);
#else
            return true;
#endif
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var formatter = SpatialImplementation.CurrentImplementation.CreateGeoJsonObjectFormatter();
            var data = serializer.Deserialize(reader);
            if (data is JArray)
            {
                var jArray = data as JArray;
                List<Object> res = new List<object>();

                foreach (var token in jArray.Children())
                {
                    var jObject = token as JObject;
                    var dic = CreateGeoJsonDictionary(jObject);
                    res.Add(formatter.Read<T>(dic));
                }

                var elementType = objectType.GetElementType();

                // call res.Cast<elementType>().ToArray<elementType>() using reflection
                var cast_mi = GenericMethodOf<IEnumerable<int>>((_) => ((IEnumerable)null).Cast<int>());
                var toArray_mi = GenericMethodOf<IEnumerable<int>>((_) => ((IEnumerable<int>)null).ToArray());
                var cast_mi_of_t = cast_mi.MakeGenericMethod(elementType);
                var toArray_mi_of_t = toArray_mi.MakeGenericMethod(elementType);
                var tmp_result = cast_mi_of_t.Invoke(null, new object[] { res });
                var result = toArray_mi_of_t.Invoke(null, new object[] { tmp_result });
                return result;

            }
            if (data is JObject)
            {
                var jObject = data as JObject;
                var dic = CreateGeoJsonDictionary(jObject);
                return formatter.Read<T>(dic); ;
            }
            return null;
        }

        private static MethodInfo GenericMethodOf<TReturn>(Expression<Func<object, TReturn>> expression)
        {
            return ((expression).Body as MethodCallExpression).Method.GetGenericMethodDefinition();
        }

        private static Dictionary<string, object> CreateGeoJsonDictionary(JObject jObject)
        {
            var dic = new Dictionary<string, object>();
            foreach (var token in jObject.Properties())
            {
                if (token.Value.Type == JTokenType.String)
                    dic.Add(token.Name, token.Value.ToObject(typeof(string)));

                if (token.Value.Type == JTokenType.Integer)
                    dic.Add(token.Name, token.Value.ToObject(typeof(int)));

                if (token.Value.Type == JTokenType.Date)
                    dic.Add(token.Name, token.Value.ToObject(typeof(DateTime)));

                if (token.Value.Type == JTokenType.Float)
                    dic.Add(token.Name, token.Value.ToObject(typeof(float)));

                if (token.Value.Type == JTokenType.Guid)
                    dic.Add(token.Name, token.Value.ToObject(typeof(Guid)));

                else if (token.Value.Type == JTokenType.Array)
                    dic.Add(token.Name, token.Value.ToObject(typeof(List<object>)));

                else if (token.Value.Type == JTokenType.Object)
                    dic.Add(token.Name, CreateGeoJsonDictionary(token.Value as JObject));
            }

            return dic;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var formatter = SpatialImplementation.CurrentImplementation.CreateGeoJsonObjectFormatter();
            var points = value as ICollection<T>;
            object collection;
            if (points != null)
            {
                collection = points.Select(formatter.Write);
            }
            else
            {
                var geo = value as T;
                collection = formatter.Write(geo);

            }
            string json = JsonConvert.SerializeObject(collection, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None
            });

            // Serialize the object data as GeoJson
            serializer.Serialize(writer, collection);

        }
    }
#pragma warning restore CS8625, CS8600, CS8602, CS8765, CS8604, CS8601, CS8603
}

