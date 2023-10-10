namespace DocConnect.Business.Models.DTOs.User
{
    public class UserSendEmailDTO
    {
        public string Email { get; set; }

        public string Subject { get; set; }

        public string PlainText { get; set; }

        public string Html { get; set; }
    }
}
