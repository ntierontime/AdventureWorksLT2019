namespace AdventureWorksLT2019.MauiXApp.WebApiClients
{
    public class AuthenticationWebApiConfig: Framework.MauiX.IWebApiConfig
    {
        public string WebApiRootUrl 
        {
            get
            {
#if DEBUG
#if WINDOWS
                return "https://localhost:16601/api/";
#elif ANDROID
                return "https://10.0.2.2:16601/api/";
#elif IOS || MACCATALYST
                return "https://localhost:16601/api/";
#else
                return "https://localhost:16601/api/";
#endif
#else
        // TODO: use production url.
#if WINDOWS
                return "https://localhost:16601/api/";
#elif ANDROID
                return "https://10.0.2.2:16601/api/";
#elif IOS || MACCATALYST
                return "https://localhost:16601/api/";
#else
                return "https://localhost:16601/api/";
#endif
#endif
            }
        }
        public bool UseToken { get; set; } = false;
        /// <summary>
        /// Should use TOKEN when on all web services calls, only exception is login.
        /// </summary>
        public string Token { get; set; } = String.Empty;
    }
}
