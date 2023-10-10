namespace DocConnect.Business.Models.DTOs.User
{
    public class UserForgotPasswordDTO
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
