using DocConnect.Business.Models.DTOs.User;
using DocConnect.Business.Models.Results;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Represents a service responsible for user authentication and authorization operations.
    /// </summary>
    public interface IAuthorizeService
    {
        /// <summary>
        /// Attempts to log in a user asynchronously using the provided login credentials.
        /// </summary>
        /// <param name="userLoginDTO">The data transfer object containing user login information.</param>
        /// <returns>A task that represents the asynchronous login operation. The task result contains the operation result along with user data.</returns>
        Task<IResult<UserResultDTO>> LoginAsync(UserLoginDTO userLoginDTO);

        /// <summary>
        /// Logs out the currently authenticated user asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous logout operation. The task result contains the operation result along with user data.</returns>
        Task<IResult<UserResultDTO>> LogOutAsync();

        /// <summary>
        /// Registers a new user asynchronously using the provided registration details.
        /// </summary>
        /// <param name="userRegistrationDTO">The data transfer object containing user registration information.</param>
        /// <returns>A task that represents the asynchronous signup operation. The task result contains the operation result along with user data.</returns>
        Task<IResult<UserEmailCredentialsDTO>> SignUpAsync(UserRegistrationDTO userRegistrationDTO);

        /// <summary>
        /// Confirms a user's email address using a verification token.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="token">The verification token associated with the email.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The result is an instance of IResult
        /// containing information about the status of the email confirmation process, along
        /// with additional user-related data (UserResultDTO).
        /// </returns>
        Task<IResult<UserResultDTO>> ConfirmUserEmailAsync(string email, string token);

        /// <summary>
        /// Resends a verification email to the specified email address.
        /// </summary>
        /// <param name="email">The email address to which the verification email should be resent.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The result is an instance of IResult
        /// containing information about the status of the email sending process, along with
        /// additional data (UserSendEmailDTO) related to the resent verification email.
        /// </returns>
        Task<IResult<UserEmailCredentialsDTO>> ResendVerificationEmailAsync(string email);

        /// <summary>
        /// Initiates the process of resetting a user's forgotten password asynchronously.
        /// </summary>
        /// <param name="email">The email address of the user who forgot their password.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing information about the password reset process if successful.</returns>
        Task<IResult<UserForgotPasswordDTO>> ForgotPasswordAsync(string email);

        /// <summary>
        /// Resets a user's password asynchronously using a reset token.
        /// </summary>
        /// <param name="email">The email address of the user whose password is being reset.</param>
        /// <param name="token">The reset token associated with the password reset request.</param>
        /// <param name="userResetPasswordDTO">The data transfer object containing the new password and confirmation.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing validation information if the reset is successful.</returns>
        Task<IResult<ValidateResetTokenDTO>> ResetPasswordAsync(string email, string token, UserResetPasswordDTO userResetPasswordDTO);

        /// <summary>
        /// Validates a reset token for a user asynchronously.
        /// </summary>
        /// <param name="email">The email address of the user whose reset token is being validated.</param>
        /// <param name="token">The reset token to be validated.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing validation information if the token is valid.</returns>
        Task<IResult<ValidateResetTokenDTO>> ValidateResetTokenAsync(string email, string token);
    }
}