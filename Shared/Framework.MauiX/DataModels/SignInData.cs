namespace Framework.MauiX.DataModels
{
    public class SignInData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool UserProfileCompleted { get; set; }

        //public bool RememberMe;
        //public bool AutoSignIn;
        public string Token { get; set; }
        public string ShortGuid { get; set; }

        public DateTime? LastLoggedInDateTime { get; set; }
        public DateTime? TokenExpireDateTime { get; set; }

        public bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(Token);
        }
        public bool GotoFirstTimeUserPage()
        {
            return !UserProfileCompleted;
        }
    }
}
