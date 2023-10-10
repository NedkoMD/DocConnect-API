using DocConnect.Business.Models.DTOs.User;

namespace DocConnect.Business.Abstraction.Factories
{
    /// <summary>
    /// Interface for generating email-related data transfer objects (DTOs).
    /// </summary>
    public interface IEmailFactory
    {
        /// <summary>
        /// Generates an email verification token for a user.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="routeValues">Additional route values or parameters for the email link.</param>
        /// <returns>A UserSendEmailDTO containing email verification token data.</returns>
        UserSendEmailDTO GetEmailVerificationToken(string email, string routeValues);

        /// <summary>
        /// Generates a password reset token for a user.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="routeValues">Additional route values or parameters for the email link.</param>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <returns>A UserSendEmailDTO containing password reset token data.</returns>
        UserSendEmailDTO GetPasswordResetToken(string email, string routeValues, string firstName, string lastName);

        /// <summary>
        /// Generates an email notification for appointment cancellation.
        /// </summary>
        /// <param name="email">The patient's email address.</param>
        /// <param name="patientFullName">The full name of the patient.</param>
        /// <param name="doctorFullName">The full name of the doctor.</param>
        /// <param name="timeSlot">The date and time of the canceled appointment.</param>
        /// <returns>A UserSendEmailDTO containing appointment cancellation notification data.</returns>
        UserSendEmailDTO GetAppointmentCancellationNotification(string email, string patientFullName, string doctorFullName, string speciality, DateTime timeSlot);
    }
}