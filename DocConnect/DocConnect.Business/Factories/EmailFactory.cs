using DocConnect.Business.Abstraction.Factories;
using DocConnect.Business.Extensions;
using DocConnect.Business.Models.DTOs.User;
using DocConnect.Business.Models.Options;
using System.Globalization;

namespace DocConnect.Business.Factories
{
    public class EmailFactory : IEmailFactory
    {
        private readonly EmailOptions _emailOptions;

        public EmailFactory(EmailOptions emailOptions)
        {
            _emailOptions = emailOptions;
        }

        public UserSendEmailDTO GetEmailVerificationToken(string email, string routeValues)
        {
            var emailContent = string.Format(EmailSenderExtensions.EmailVerificationTemplate, _emailOptions.ConfirmEmailRoute + routeValues);

            return new UserSendEmailDTO()
            {
                Email = email,
                Subject = "DocConnect Email Verification",
                PlainText = string.Empty,
                Html = emailContent
            };
        }

        public UserSendEmailDTO GetPasswordResetToken(string email, string routeValues, string firstName, string lastName)
        {
            var emailContent = string.Format(EmailSenderExtensions.PasswordResetTemplate, firstName, lastName, _emailOptions.ResetPasswordRoute + routeValues);

            return new UserSendEmailDTO()
            {
                Email = email,
                Subject = "DocConnect Reset Password",
                PlainText = string.Empty,
                Html = emailContent
            };
        }

        public UserSendEmailDTO GetAppointmentCancellationNotification(string email, string patientFullName, string doctorFullName, string speciality, DateTime timeSlot)
        {
            var formattedTimeSlot = timeSlot.ToString("dddd, dd MMMM yyyy - hh:mm tt", CultureInfo.InvariantCulture);
            var emailContent = string.Format(EmailSenderExtensions.AppointmentCancellationTemplate, patientFullName, doctorFullName, speciality, formattedTimeSlot);

            return new UserSendEmailDTO()
            {
                Email = email,
                Subject = "Appointment Cancellation Notification",
                PlainText = string.Empty,
                Html = emailContent
            };
        }
    }
}
