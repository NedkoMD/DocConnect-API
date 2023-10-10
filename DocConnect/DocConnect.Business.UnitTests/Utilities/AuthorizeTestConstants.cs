using DocConnect.Business.Models.DTOs.User;
using DocConnect.Data.Models.Entities;

namespace DocConnect.Business.UnitTests.Utilities
{
    public static class AuthorizeTestConstants
    {
        public const string TestNonExistingUserEmail = "validemail@mentormate.com";

        public const string TestExistingUserEmail = "invalidemail@mentormate.com";

        public const string TestValidPassword = "ValidPassword";

        public const string TestInvalidPassword = "InvalidPassword";

        public const string TestEmailConfirmationToken = "ValidToken";

        public const string TestEmailInvalidToken = "InvalidToken";

        public const string TestInvalidBase64Email = "InvalidBase64Email==";

        public const string TestInvalidBase64Token = "InvalidBase64Token==";

        public const string TestNewPassword = "new_password";

        public static readonly UserRegistrationDTO TestValidUserRegistrationDTO = new UserRegistrationDTO()
        {
            Email = TestNonExistingUserEmail
        };

        public static readonly UserRegistrationDTO TestInvalidUserRegistrationDTO = new UserRegistrationDTO()
        {
            Email = TestExistingUserEmail
        };

        public static readonly UserLoginDTO TestUserLoginDTOWithInvalidEmail = new UserLoginDTO()
        {
            Email = TestNonExistingUserEmail,
            Password = TestValidPassword
        };

        public static readonly UserLoginDTO TestUserLoginDTOWithInvalidPassword = new UserLoginDTO()
        {
            Email = TestExistingUserEmail,
            Password = TestInvalidPassword
        };

        public static readonly UserLoginDTO TestValidUserLoginDTO = new UserLoginDTO()
        {
            Email = TestExistingUserEmail,
            Password = TestValidPassword
        };

        public static readonly User TestExistingUser = new User()
        {
            Email = TestNonExistingUserEmail
        };

        public static readonly User TestNonExistingUser = null;
    }
}
