using DocConnect.Data.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace DocConnect.Data.Abstraction.Helpers
{
    /// <summary>
    /// Represents a contract for managing user sign-in operations in a documentation-connected system.
    /// </summary>
    public interface IDocConnectSignInManager
    {
        /// <summary>
        /// Attempts to sign in a user with the provided password.
        /// </summary>
        /// <param name="user">The user to sign in.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="isPersistent">Indicates whether the sign-in session should persist across browser sessions.</param>
        /// <param name="lockoutOnFailure">Indicates whether to lock out the user account on sign-in failure.</param>
        /// <returns>A task that represents the asynchronous sign-in operation. The task result is true if the sign-in was successful; otherwise, false.</returns>
        Task<SignInResult> PasswordSignInAsync(User user, string password, bool isPersistent, bool lockoutOnFailure);

        /// <summary>
        /// Signs out the currently signed-in user.
        /// </summary>
        /// <returns>A task that represents the asynchronous sign-out operation.</returns>
        Task SignOutAsync();
    }
}