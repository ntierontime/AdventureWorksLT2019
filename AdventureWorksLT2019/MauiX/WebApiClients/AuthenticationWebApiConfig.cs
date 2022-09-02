namespace AdventureWorksLT2019.MauiX.WebApiClients
{
    public class AuthenticationWebApiConfig: Framework.MauiX.IWebApiConfig
    {
        public string WebApiRootUrl 
        {
            get
            {
#if DEBUG
#if WINDOWS
                return "http://localhost:15601/api/";
#elif ANDROID
                return "http://10.0.2.2:15601/api/";
#elif IOS || MACCATALYST
                return "http://localhost:15601/api/";
#else
                return "http://localhost:15601/api/";
#endif
#else
        // TODO: use production url.
#if WINDOWS
                return "http://localhost:15601/api/";
#elif ANDROID
                return "http://10.0.2.2:15601/api/";
#elif IOS || MACCATALYST
                return "http://localhost:15601/api/";
#else
                return "http://localhost:15601/api/";
#endif
#endif
            }
        }
        public bool UseToken { get; set; }
        /// <summary>
        /// Should use TOKEN when on all web services calls, only exception is login.
        /// </summary>
        public string Token { get; set; }
    }
}
