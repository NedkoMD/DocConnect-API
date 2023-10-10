namespace DocConnect.Business.Models.Utilities
{
    public static class AuthorizeMessages
    {
        public const string InvalidLogin = "Email or password are invalid";

        public const string UserAlreadyExists = "User with this Email already exists";

        public const string UserCreationFailed = "User registration failed";

        public const string EmailIsNotConfirmed = "Please confirm your email before trying to login";

        public const string UserNotFound = "User not found";

        public const string EmailNotSent = "Email could not be sent";

        public const string EmailAlreadyConfirmed = "Email is already confirmed";

        public const string VerificationEmailAlreadySent = "An email has already been sent. Check your inbox (including spam folder) for the verification email";

        public const string ContactSupportForResend = "You have tried too many times! If you still experience issues with verifying your account, please contact our support: support@docconnect-green.devsmm.com";

        public const string ResetPasswordEmailAlreadySent = "An email has already been sent. Check your inbox (including spam folder) for the password reset email";

        public const string TokenExpiredOrInvalid = "Token is expired or doesn't exist";
    }
}
