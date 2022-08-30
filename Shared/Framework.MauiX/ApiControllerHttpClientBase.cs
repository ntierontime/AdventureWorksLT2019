using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MauiX
{
    public abstract class ApiControllerHttpClientBase
    {
        protected readonly string _rootPath;

        public abstract string ControllerName { get; }

        protected readonly HttpClient _client = null!;

        public ApiControllerHttpClientBase(string rootPath, bool useToken = false, string token = null!)
        {
            this._rootPath = rootPath;
            _client = new HttpClient(new System.Net.Http.HttpClientHandler());
            if(useToken)
            {
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
        }

        public string GetHttpRequestUrl(string actionName, Dictionary<string, string> parameters)
        {
            List<string> parametersInList = new();
            foreach (var kvPair in parameters)
            {
                if (!string.IsNullOrEmpty(kvPair.Key) && !string.IsNullOrEmpty(kvPair.Value))
                {
                    // with [query string name] is an array, otherwise is a single value
                    if (kvPair.Key.StartsWith("[") && kvPair.Key.EndsWith("]"))
                        parametersInList.Add(kvPair.Value); // only value is here. format parametername[index=0,1,...], parameter name is optional if only one array in this web api method, see https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1#collections
                    else if (!kvPair.Key.StartsWith("[") && !kvPair.Key.EndsWith("]")) // single value, should check it is a valid url query string parameter name
                        parametersInList.Add(string.Format("{0}={1}", kvPair.Key, kvPair.Value));
                }
            }
            string parametersInString = String.Join("&", parametersInList);

            return GetHttpRequestUrl(_rootPath, ControllerName, actionName, parametersInString);
        }

        public const string ListsApiControllerName = "ListsApi";
        public string GetListsApiHttpRequestUrl(string actionName, Dictionary<string, string> parameters)
        {
            if (parameters == null)
                return GetHttpRequestUrl(_rootPath, ListsApiControllerName, actionName, null);

            List<string> parametersInList = new();
            foreach (var kvPair in parameters)
            {
                if (!string.IsNullOrEmpty(kvPair.Value))
                    parametersInList.Add(string.Format("{0}={1}", kvPair.Key, kvPair.Value));
            }
            string parametersInString = String.Join("&", parametersInList);

            return GetHttpRequestUrl(_rootPath, ListsApiControllerName, actionName, parametersInString);
        }

        //public string GetHttpRequestUrl(string actionName, string parameters)
        //{
        //    return GetHttpRequestUrl(RootPath, ControllerName, actionName, parameters);
        //}

        public static string GetArrayParamterString<T>(string name, bool addNameToParameters, List<T> array = null!)
        {
            if (array == null && array!.Count == 0)
                return string.Empty;

            if (addNameToParameters)
                return string.Join("&", array.Select(t => $"{name}[{array.IndexOf(t)}]={t}"));
            else
                return string.Join("&", array.Select(t => $"[{array.IndexOf(t)}]={t}"));
        }

        public string GetHttpRequestUrl(string actionName)
        {
            return GetHttpRequestUrl(_rootPath, ControllerName, actionName, null);
        }

        public static string GetHttpRequestUrl(string rootPath, string controllerName, string actionName, string parameters)
        {
            StringBuilder retval = new(rootPath.TrimEnd('/'));
            if (!string.IsNullOrWhiteSpace(controllerName))
            {
                retval.Append('/');
                retval.Append(controllerName.TrimStart('/').TrimEnd('/'));
            }
            if (!string.IsNullOrWhiteSpace(actionName))
            {
                retval.Append('/');
                retval.Append(actionName.TrimStart('/').TrimEnd('/'));
            }
            if (!string.IsNullOrWhiteSpace(parameters))
            {
                retval.Append('?');
                retval.Append(parameters.TrimStart('?').TrimEnd('/'));
            }
            return retval.ToString();
        }
    }
}

