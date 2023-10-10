namespace DocConnect.Business.Models.Utilities
{
    public static class UserDTOMessages
    {
        public const string EmptyEmail = "Please enter an email address.";
        public const string InvalidEmail = "Please enter a valid email address.";

        public const string EmptyPassword = "Please enter a password.";
        public const string InvalidPasswordLength = "Password must be between 8 and 100 characters.";
        public const string WeakPassword = "Your password must have at least 8 characters, with a mix of uppercase, lowercase, numbers, and symbols.";

        public const string EmptyConfirmPassword = "Please confirm your password.";
        public const string PasswordsNoMatch = "Those passwords didn’t match. Please try again.";

        public const string EmptyFirstName = "Please enter a first name.";
        public const string InvalidFirstNameLength = "First name must be less than 50 characters long.";

        public const string EmptyLastName = "Please enter a last name.";
        public const string InvalidLastNameLength = "Last name must be less than 50 characters long.";
    }
}
