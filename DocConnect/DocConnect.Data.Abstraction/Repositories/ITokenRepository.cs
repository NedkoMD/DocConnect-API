using DocConnect.Data.Models.Entities;

namespace DocConnect.Data.Abstraction.Repositories
{
    /// <summary>
    /// Represents a repository responsible for storing and retrieving authentication tokens.
    /// </summary>
    public interface ITokenRepository
    {
        /// <summary>
        /// Retrieves an authentication token asynchronously based on its value.
        /// </summary>
        /// <param name="value">The value of the authentication token.</param>
        /// <returns>A task that represents the asynchronous operation of retrieving a token by its value.</returns>
        Task<Token> GetByValueAsync(string value);

        /// <summary>
        /// Adds a new authentication token asynchronously to the repository.
        /// </summary>
        /// <param name="token">The authentication token to be added.</param>
        /// <returns>A task that represents the asynchronous operation of adding a token to the repository.</returns>
        Task AddAsync(Token token);

        /// <summary>
        /// Deletes an authentication token asynchronously from the repository.
        /// </summary>
        /// <param name="token">The authentication token to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation of deleting a token from the repository.</returns>
        Task DeleteAsync(Token token);
    }
}
