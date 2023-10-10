using DocConnect.Data.Models.Entities;

namespace DocConnect.Business.UnitTests.Utilities
{
    public static class TokenTestConstants
    {
        public const string TestExistingTokenValue = "ValidToken";

        public const string TestNonExistingTokenValue = "InvalidToken";

        public const string TestInvalidTokenValue = null;

        public static readonly Token TestExistingToken = new Token();

        public static readonly Token TestNonExistingToken = null;

        public static readonly Token TestNullValueToken = null;

        public const uint ValidUintUserId = 1;
    }
}
