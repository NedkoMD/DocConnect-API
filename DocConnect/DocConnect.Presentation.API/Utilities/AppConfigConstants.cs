namespace DocConnect.Presentation.API.Utilities
{
    public static class AppConfigConstants
    {
        public const string DatabaseUrl = "DATABASE_URL";
        public const string DatabaseUser = "DATABASE_USER";
        public const string DatabaseName = "DATABASE_NAME";
        public const string DatabasePassword = "DATABASE_PASSWORD";

        public const string AzureImageDomainName = "AZURE_CDN_HOSTNAME";
        public const string AzureSecretKeyName = "JWT_SECRET_KEY";

        public const string DefaultConnection = "DefaultConnection";

        public const string AppAllowedOriginsName = "AppAllowedOrigins";
        public const string CorsKey = "CORS:Hosts";

        public const string JWTTokenKey = "Authorization";

        public const string EmailConnectionStringName = "AZURE_SMTP_CONNECTION_STRING";

        public const string EmailSenderName = "AZURE_SMTP_SENDER";
    }
}
