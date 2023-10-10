using DocConnect.Business.Models.DTOs.Token;

namespace DocConnect.Business.Abstraction.Helpers
{
    /// <summary>
    /// Represents a class responsible for generating authentication tokens.
    /// </summary>
    public interface ITokenHandler
    {
        /// <summary>
        /// Generates an authentication token based on the provided user information.
        /// </summary>
        /// <param name="userResultDTO">The data transfer object containing user information for which the token is generated.</param>
        /// <returns>The authentication token data transfer object containing the generated token.</returns>
        TokenAddDTO GenerateAccessToken(TokenGenerateDTO tokenGenerateDTO);

        /// <summary>
        /// Generates a new refresh token based on the provided user information.
        /// </summary>
        /// <param name="userResultDTO">The user information to be included in the refresh token.</param>
        /// <returns>A <see cref="TokenAddDTO"/> containing the newly generated refresh token.</returns>
        TokenAddDTO GenerateRefreshToken(TokenGenerateDTO tokenGenerateDTO);
    }
}
