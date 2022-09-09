using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MauiX
{
    public interface IWebApiConfig
    {
        string WebApiRootUrl { get; }

        ///// <summary>
        ///// UseToken will be a a parameter of each GET/POST/PUT... method in HttpClient
        ///// </summary>
        //bool UseToken { get; }
        /// <summary>
        /// Token is saved in Preferences: will read each time call web api Should use TOKEN when on all web services calls, only exception is login.
        /// </summary>
        //string Token { get; }
    }
}
