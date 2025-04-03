namespace Framework.Models.Account
{
    public class AuthenticationResponse<TPerson, TNotification>
    {
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public bool UserProfileCompleted { get; set; } = false;
        public string? Token { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; } = null!;
        public string? Message { get; set; }
        public IList<string>? Roles { get; set; }
        public TPerson? Person { get; set; }
        public TNotification[]? Notifications { get; set; }
    }
}

