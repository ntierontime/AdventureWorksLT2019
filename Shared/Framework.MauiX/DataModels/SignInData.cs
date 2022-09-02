namespace Framework.MauiX.DataModels
{
    public struct SignInData
    {
        public string UserName;
        public string Password;
        //public bool RememberMe;
        //public bool AutoSignIn;
        public string Token;
        public string ShortGuid;
        public bool GoToWelcomeWizard;
        public DateTime? LastLoggedInDateTime;
        public DateTime? TokenExpireDateTime;

        public bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(Token);
        }
    }
}
