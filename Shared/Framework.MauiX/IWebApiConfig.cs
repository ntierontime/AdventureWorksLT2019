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
        bool UseToken { get; set; }
        /// <summary>
        /// Should use TOKEN when on all web services calls, only exception is login.
        /// </summary>
        string Token { get; set; }
    }
}
