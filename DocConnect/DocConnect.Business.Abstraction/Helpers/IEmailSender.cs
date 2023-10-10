using DocConnect.Business.Models.DTOs.User;

namespace DocConnect.Business.Abstraction.Helpers
{
    /// <summary>
    /// Represents an interface for sending emails.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Asynchronously sends an email to a user using the provided UserSendEmailDTO.
        /// </summary>
        /// <param name="userSendEmailDTO">The data required for sending the email, including recipient,
        /// subject, message body, and any other relevant information.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The method does not return a result
        /// directly, but it indicates the completion or progress of the email sending process.
        /// </returns>
        Task SendEmailAsync(UserSendEmailDTO userSendEmailDTO);
    }
}