namespace DocConnect.Business.Models.Utilities
{
    public static class TokenMessages
    {
        public const string TokenNotFound = "Token with that value does not exist";

        public const string TokenNotInHeader = "You need to be logged in, before you logout";

        public const string RefreshTokenValueInHeadersIsNull = "The value of the refresh token in the headers is null";
    }
}
