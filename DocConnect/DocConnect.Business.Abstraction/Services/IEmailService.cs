using DocConnect.Business.Models.DTOs.Appointments;
using DocConnect.Business.Models.DTOs.User;
using DocConnect.Business.Models.Results;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Interface for sending various email notifications asynchronously.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Send an email for user email verification asynchronously.
        /// </summary>
        Task<IResult<UserResultDTO>> SendEmailVerification(UserEmailCredentialsDTO userEmailCredentialsDTO);

        /// <summary>
        /// Send an email for password reset asynchronously.
        /// </summary>
        Task<IResult<UserResultDTO>> SendPasswordResetAsync(UserForgotPasswordDTO userForgotPasswordDTO);

        /// <summary>
        /// Send an email for appointment cancellation asynchronously.
        /// </summary>
        Task<IResult<AppointmentCancellationDTO>> SendAppointmentCancellationAsync(AppointmentDetailedEmailDTO appointmentDetailedEmailDTO);
    }
}