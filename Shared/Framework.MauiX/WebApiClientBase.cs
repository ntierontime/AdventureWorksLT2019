using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MauiX;

public abstract class WebApiClientBase
{
    protected readonly string _rootPath;
    protected readonly string _controllerName;

    protected readonly HttpClient _client = null!;

    public WebApiClientBase(string rootPath, string controllerName)
    {
        this._rootPath = rootPath;
        _controllerName = controllerName;
        _client = new HttpClient(new System.Net.Http.HttpClientHandler());
    }

    public async Task<TResponse> Post<TRequest, TResponse>(string url, TRequest request, bool userToken = true)
    {
        if (userToken)
        {
            if (_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GetToken());
        }

        var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        jsonSerializerSettings.Converters.Add(new StringEnumConverter());
        string requestJSON = JsonConvert.SerializeObject(request, Formatting.Indented, jsonSerializerSettings);
        var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(url, httpContent);

        if (response.IsSuccessStatusCode)
        {
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TResponse>(content);
                return result;
            }
            catch
            {
                return default(TResponse);
            }
        }
        else
        {
            return default(TResponse);
        }
    }


    public async Task<TResponse> Put<TRequest, TResponse>(string url, TRequest request, bool userToken = true)
        where TResponse: class, new()
    {
        if (userToken)
        {
            if (_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GetToken());
        }

        var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        jsonSerializerSettings.Converters.Add(new StringEnumConverter());
        string requestJSON = JsonConvert.SerializeObject(request, Formatting.Indented, jsonSerializerSettings);
        var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(url, httpContent);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResponse>(content);
            return result;
        }
        return default(TResponse);
    }

    public async Task<TResponse> Get<TResponse>(string url, bool userToken = true)
    {
        if (userToken)
        {
            if (_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GetToken());
        }

        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResponse>(content);
            return result;
        }
        else
        {
            return default(TResponse);
        }
    }

    public async Task<TResponse> Delete<TResponse>(string url, bool userToken = true)
    {
        if (userToken)
        {
            if (_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GetToken());
        }
        var response = await _client.DeleteAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResponse>(content);
            return result;
        }
        return default(TResponse);
    }

    public static string GetToken()
    {
        // TODO, review on how to keep TOKEN
        return Preferences.Default.Get<string>("Token", string.Empty);
    }

    public string GetHttpRequestUrl(string actionName, string route, Dictionary<string, string> parameters)
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

        return GetHttpRequestUrl(_rootPath, _controllerName, actionName, route, parametersInString);
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

        return GetHttpRequestUrl(_rootPath, _controllerName, actionName, null, parametersInString);
    }

    public const string ListsApiControllerName = "ListsApi";
    public string GetListsApiHttpRequestUrl(string actionName, Dictionary<string, string> parameters)
    {
        if (parameters == null)
            return GetHttpRequestUrl(_rootPath, ListsApiControllerName, actionName, null, null);

        List<string> parametersInList = new();
        foreach (var kvPair in parameters)
        {
            if (!string.IsNullOrEmpty(kvPair.Value))
                parametersInList.Add(string.Format("{0}={1}", kvPair.Key, kvPair.Value));
        }
        string parametersInString = String.Join("&", parametersInList);

        return GetHttpRequestUrl(_rootPath, ListsApiControllerName, actionName, null, parametersInString);
    }

    public static string GetArrayParamterString<T>(string name, bool addNameToParameters, List<T> array = null!)
    {
        if (array == null && array!.Count == 0)
            return string.Empty;

        if (addNameToParameters)
            return string.Join("&", array.Select(t => $"{name}[{array.IndexOf(t)}]={t}"));
        else
            return string.Join("&", array.Select(t => $"[{array.IndexOf(t)}]={t}"));
    }

    public string GetHttpRequestUrl(string actionName, string route)
    {
        return GetHttpRequestUrl(_rootPath, _controllerName, actionName, route, null);
    }
    public string GetHttpRequestUrl(string actionName)
    {
        return GetHttpRequestUrl(_rootPath, _controllerName, actionName, null, null);
    }

    public static string GetHttpRequestUrl(string rootPath, string controllerName, string actionName, string route, string parameters)
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
        if (!string.IsNullOrWhiteSpace(route))
        {
            retval.Append('/');
            retval.Append(route.TrimStart('/').TrimEnd('/'));
        }
        if (!string.IsNullOrWhiteSpace(parameters))
        {
            retval.Append('?');
            retval.Append(parameters.TrimStart('?').TrimEnd('/'));
        }
        return retval.ToString();
    }
}

