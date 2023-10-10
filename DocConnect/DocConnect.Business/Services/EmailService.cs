using DocConnect.Business.Abstraction.Factories;
using DocConnect.Business.Abstraction.Helpers;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.DTOs.Appointments;
using DocConnect.Business.Models.DTOs.User;
using DocConnect.Business.Models.Results;
using System.Text;

namespace DocConnect.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly IEmailFactory _emailFactory;
        private readonly IResultFactory _resultFactory;

        public EmailService(
            IEmailSender emailSender,
            IEmailFactory emailFactory,
            IResultFactory resultFactory)
        {
            _emailSender = emailSender;
            _emailFactory = emailFactory;
            _resultFactory = resultFactory;
        }

        public async Task<IResult<UserResultDTO>> SendEmailVerification(UserEmailCredentialsDTO userEmailCredentialsDTO)
        {
            var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(userEmailCredentialsDTO.Token));
            var routeValues = $"/{userEmailCredentialsDTO.Email}/{encodedToken}";

            var userSendEmailDTO = _emailFactory.GetEmailVerificationToken(userEmailCredentialsDTO.Email, routeValues);

            try
            {
                await _emailSender.SendEmailAsync(userSendEmailDTO);
            }
            catch (Exception ex)
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<UserResultDTO>(ex.Message);

                return badRequestResult;
            }

            var noContentResult = _resultFactory.GetNoContentResult<UserResultDTO>();

            return noContentResult;
        }

        public async Task<IResult<UserResultDTO>> SendPasswordResetAsync(UserForgotPasswordDTO userForgotPasswordDTO)
        {
            var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(userForgotPasswordDTO.Token));
            var encodedEmail = Convert.ToBase64String(Encoding.UTF8.GetBytes(userForgotPasswordDTO.Email));
            var routeValues = $"/{encodedEmail}/{encodedToken}";

            var userSendEmailDTO = _emailFactory.GetPasswordResetToken(userForgotPasswordDTO.Email, routeValues, userForgotPasswordDTO.FirstName, userForgotPasswordDTO.LastName);

            try
            {
                await _emailSender.SendEmailAsync(userSendEmailDTO);
            }
            catch (Exception ex)
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<UserResultDTO>(ex.Message);

                return badRequestResult;
            }

            var noContentResult = _resultFactory.GetNoContentResult<UserResultDTO>();

            return noContentResult;
        }

        public async Task<IResult<AppointmentCancellationDTO>> SendAppointmentCancellationAsync(AppointmentDetailedEmailDTO appointmentDetailedEmailDTO)
        {
            var userSendEmailDTO = _emailFactory.GetAppointmentCancellationNotification(appointmentDetailedEmailDTO.Email, appointmentDetailedEmailDTO.PatientFullName, appointmentDetailedEmailDTO.DoctorFullName, appointmentDetailedEmailDTO.SpecialityName, appointmentDetailedEmailDTO.TimeSlot);

            try
            {
                await _emailSender.SendEmailAsync(userSendEmailDTO);
            }
            catch (Exception ex)
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<AppointmentCancellationDTO>(ex.Message);

                return badRequestResult;
            }

            var noContentResult = _resultFactory.GetNoContentResult<AppointmentCancellationDTO>();

            return noContentResult;
        }
    }
}
