using Azure;
using Azure.Communication.Email;
using DocConnect.Business.Abstraction.Helpers;
using DocConnect.Business.Models.DTOs.User;
using DocConnect.Business.Models.Options;

namespace DocConnect.Business.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions _emailOptions;

        public EmailSender(EmailOptions emailOptions)
        {
            _emailOptions = emailOptions;
        }

        public async Task SendEmailAsync(UserSendEmailDTO userSendEmailDTO)
        {
            var emailClient = new EmailClient(_emailOptions.ConnectionString);

            var subject = userSendEmailDTO.Subject;
            var emailContent = new EmailContent(subject)
            {
                PlainText = userSendEmailDTO.PlainText,
                Html = userSendEmailDTO.Html
            };

            var emailMessage = new EmailMessage(_emailOptions.Sender, userSendEmailDTO.Email, emailContent);

            await emailClient.SendAsync(WaitUntil.Started, emailMessage);
        }
    }
}
