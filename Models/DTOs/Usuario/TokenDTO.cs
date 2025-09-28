namespace HelpFinance.Models.DTOs.Usuario
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string RefreshToken { get; set; }

        public DateTime RefreshExpiresAt { get; set; }
    }

    public class JwtOptions
    {
        public string Key { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int ExpiresMinutes { get; set; } = 15;
    }
}
