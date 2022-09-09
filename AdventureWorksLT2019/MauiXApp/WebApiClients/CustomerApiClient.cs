using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients
{
    public partial class CustomerApiClient : Framework.MauiX.WebApiClientBase
    {
        public CustomerApiClient(AdventureWorksLT2019.MauiXApp.WebApiClients.Common.WebApiConfig webApiConfig)
            : base(webApiConfig.WebApiRootUrl, "CustomerApi")
        {
        }
    }
}
