namespace Framework.Models.Account
{
    public class AuthenticationResponse
    {
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public string? Token { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; } = null!;
        public IList<string> Roles { get; set; } = null!;
    }
}

