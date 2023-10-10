namespace DocConnect.Business.Models.Options
{
    public class TokenOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }

        public int TokenLifetimeSeconds { get; set; }

        public int RefreshTokenLifetimeHours { get; set; }
    }
}
