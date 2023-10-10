using DocConnect.Business.Models.DTOs.Token;
using DocConnect.Business.Models.DTOs.User;
using DocConnect.Business.Models.Results;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Represents a service responsible for managing authentication tokens for users.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Retrieves an authentication token asynchronously based on its value.
        /// </summary>
        /// <param name="value">The value of the authentication token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result along with the token data.</returns>
        Task<IResult<TokenResultDTO>> GetByValueAsync(string value);

        /// <summary>
        /// Generates and associates a new authentication token asynchronously with the provided user data.
        /// </summary>
        /// <param name="userResultDTO">The data transfer object containing user information for which the token is generated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result along with the token data.</returns>
        Task<IResult<ClientTokenResultDTO>> AddAsync(UserResultDTO userResultDTO);

        /// <summary>
        /// Removes an authentication token asynchronously based on its value.
        /// </summary>
        /// <param name="tokenValue">The value of the authentication token to be removed.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result along with the removed token data.</returns>
        Task<IResult<TokenResultDTO>> RemoveAsync(string tokenValue);

        /// <summary>
        /// The method receives a refresh token value, if the refresh token value is valid, revokes the old refresh token by the valid value, generates a new refresh token, and returns it as an ok result, if the token value is invalid returns unauthorized result.
        /// </summary>
        Task<IResult<TokenResultDTO>> RequestAccessTokenAsync(string email, string accessToken, string refreshToken);
    }
}