namespace DocConnect.Business.Models.Utilities
{
    public class UserDTOConstrains
    {
        public const int MaxLengthEmail = 255;

        public const int MinLengthPassword = 8;

        public const int MaxLengthPassword = 100;

        public const int MaxLengthFirstName = 50;

        public const int MaxLengthLastName = 50;

        public const string EmailRegex = @"^[a-zA-Z0-9_+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$";

        public const string PasswordRegex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$";
    }
}
