using Framework.Models;
using Framework.Mvc.Identity;
using Framework.Mvc.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Framework.Mvc.Identity
{
    public static class SwaggerExtension
    {

        public static string GetSwaggerCustomizedSchemaId(Type x)
        {
            if (x == null || string.IsNullOrEmpty(x.FullName))
                return String.Empty;

            if (!x.IsGenericType)
            {
                return x.FullName.Replace("+", "");
            }

            if (x.Namespace == typeof(Response<int>).Namespace) // same namespace
            {
                if (x.Name == typeof(Response<int>).Name) // Response'1
                {
                    if (x.GenericTypeArguments != null && x.GenericTypeArguments.Length == 1 && !string.IsNullOrEmpty(x.GenericTypeArguments[0].FullName))
                    {
                        if (x.GenericTypeArguments[0].Name == typeof(MultiItemsCUDRequest<int, string>).Name) // MultiItemsCUDRequest'2
                        {
                            if (x.GenericTypeArguments[0].GenericTypeArguments != null && x.GenericTypeArguments[0].GenericTypeArguments.Length == 2 && !string.IsNullOrEmpty(x.GenericTypeArguments[0].GenericTypeArguments[0].FullName))
                            {
                                // the first GenericTypeArguments is Identifier
                                return x.GenericTypeArguments[0].GenericTypeArguments[0].FullName!.Replace("Identifier", "MultiItemsCUDResponse");
                            }
                        }

                        return x.GenericTypeArguments[0].FullName!.Replace("+", "") + "Response";
                    }
                }
                if (x.Name == typeof(Response<int>).Name) // Response'1
                {
                    if (x.GenericTypeArguments != null && x.GenericTypeArguments.Length == 1 && !string.IsNullOrEmpty(x.GenericTypeArguments[0].FullName))
                    {
                        return x.GenericTypeArguments[0].FullName!.Replace("+", "") + "Response";
                    }
                }
                if (x.Name == typeof(ListResponse<int>).Name) // ListResponse'1
                {
                    if (x.GenericTypeArguments != null && x.GenericTypeArguments.Length == 1 && !string.IsNullOrEmpty(x.GenericTypeArguments[0].FullName))
                    {
                        return x.GenericTypeArguments[0].FullName!.Replace("+", "").Replace("[]", "") + "ListResponse";
                    }
                }
                if (x.Name == typeof(MultiItemsCUDRequest<int, string>).Name) // MultiItemsCUDRequest'2
                {
                    if (x.GenericTypeArguments != null && x.GenericTypeArguments.Length == 2 && !string.IsNullOrEmpty(x.GenericTypeArguments[0].FullName))
                    {
                        // the first GenericTypeArguments is Identifier
                        return x.GenericTypeArguments[0].FullName!.Replace("Identifier", "MultiItemsCUDRequest");
                    }
                }
                //if (x.Name == typeof(BatchActionRequest<int>).Name) // BatchActionRequest'1
                //{
                //    if (x.GenericTypeArguments != null && x.GenericTypeArguments.Length == 1 && !string.IsNullOrEmpty(x.GenericTypeArguments[0].FullName))
                //    {
                //        return x.GenericTypeArguments[0].FullName!.Replace("+", "").Replace("Identifier", "") + "BatchActionRequest";
                //    }
                //}
                if (x.Name == typeof(BatchActionRequest<int, int>).Name) // BatchActionRequest'2
                {
                    if (x.GenericTypeArguments != null && x.GenericTypeArguments.Length == 2 && !string.IsNullOrEmpty(x.GenericTypeArguments[1].FullName))
                    {
                        return x.GenericTypeArguments[1].FullName!.Replace("+", "") + "BatchUpdateRequest";
                    }
                }
            }

            return x.FullName!;
        }
    }
}