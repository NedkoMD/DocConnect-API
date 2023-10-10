using DocConnect.Data.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace DocConnect.Data.Abstraction.Helpers
{
    /// <summary>
    /// Represents a service responsible for managing application users.
    /// </summary>
    public interface IDocConnectUserManager
    {
        /// <summary>
        /// Creates an administrator user asynchronously with the provided user object and password.
        /// </summary>
        /// <param name="user">The user object representing the administrator to be created.</param>
        /// <param name="password">The password associated with the administrator user.</param>
        /// <returns>A task that represents the asynchronous operation of creating an administrator user.</returns>
        Task CreateAdminAsync(User user, string password);

        /// <summary>
        /// Creates a standard user asynchronously with the provided user object and password.
        /// </summary>
        /// <param name="user">The user object representing the standard user to be created.</param>
        /// <param name="password">The password associated with the standard user.</param>
        /// <returns>A task that represents the asynchronous operation of creating a standard user.</returns>
        Task CreateUserAsync(User user, string password);

        /// <summary>
        /// Retrieves an application user asynchronously based on their email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation of finding an application user by email.</returns>
        Task<User> FindByEmailAsync(string email);

        /// <summary>
        /// Find a user by their unique identifier asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to find.</param>
        /// <returns>A task representing the asynchronous operation, returning the found user.</returns>
        Task<User> FindUserByIdAsync(uint userId);

        /// <summary>
        /// Retrieves a user's roles asynchronously based on the provied user.
        /// </summary>
        /// <param name="user">The user to be used for the retrived roles.</param>
        /// <returns>A task that represents the asynchronous operation of finding a roles by a user.</returns>
        Task<IEnumerable<string>> GetRolesAsync(User user);

        /// <summary>
        /// Generates a confirmation token asynchronously for the specified user's email verification.
        /// </summary>
        /// <param name="user">The user for whom the email confirmation token is to be generated.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The result of the task is a string
        /// containing the generated confirmation token that can be sent to the user for verification.
        /// </returns>
        Task<string> GenerateConfirmEmailTokenAsync(User user);

        /// <summary>
        /// Confirms a user's email address using a verification token.
        /// </summary>
        /// <param name="user">The user whose email address is being confirmed.</param>
        /// <param name="token">The verification token associated with the email confirmation.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The result of the task is an instance of
        /// IdentityResult indicating the outcome of the email confirmation process.
        /// </returns>
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        /// <summary>
        /// Generate a reset password token asynchronously for a user.
        /// </summary>
        /// <param name="user">The user for whom to generate the reset token.</param>
        /// <returns>A task representing the asynchronous operation, returning the generated reset token as a string.</returns>
        Task<string> GenerateResetPasswordTokenAsync(User user);

        /// <summary>
        /// Reset the user's password asynchronously using a token.
        /// </summary>
        /// <param name="user">The user whose password will be reset.</param>
        /// <param name="token">The reset token.</param>
        /// <param name="newPassword">The new password for the user.</param>
        /// <param name="confirmPassword">The confirmation of the new password.</param>
        /// <returns>A task representing the asynchronous operation, returning an IdentityResult indicating the result of the password reset.</returns>
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword, string confirmPassword);

        /// <summary>
        /// Check if a token is valid asynchronously for a user and specific token details.
        /// </summary>
        /// <param name="user">The user associated with the token.</param>
        /// <param name="tokenProvider">The token provider for validation.</param>
        /// <param name="purpose">The purpose of the token.</param>
        /// <param name="token">The token to be validated.</param>
        /// <returns>A task representing the asynchronous operation, returning a boolean indicating token validity.</returns>
        Task<bool> IsValidTokenAsync(User user, string tokenProvider, string purpose, string token);
    }
}