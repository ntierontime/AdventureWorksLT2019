namespace Framework.MauiX.DataModels
{
    public class SignInData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        //public bool RememberMe;
        //public bool AutoSignIn;
        public string Token { get; set; }
        public string ShortGuid { get; set; }
        public DateTime? LastLoggedInDateTime;
        public DateTime? TokenExpireDateTime;

        public bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(Token);
        }
    }
}
