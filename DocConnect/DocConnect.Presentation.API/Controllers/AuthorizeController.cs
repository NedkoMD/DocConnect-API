using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.DTOs.User;
using DocConnect.Business.Models.Enums;
using DocConnect.Presentation.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocConnect.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AuthorizeController(IAuthorizeService authorizeService, ITokenService tokenService, IEmailService emailService)
        {
            _authorizeService = authorizeService;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        // POST: api/Authorize/login
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO userRequestDTO)
        {
            var response = await _authorizeService.LoginAsync(userRequestDTO);

            if (response.StatusCode == DocConnectStatusCode.BadRequest)
            {
                return BadRequest(response.ErrorMessages);
            }

            var tokenResponse = await _tokenService.AddAsync(response.Data);

            return this.HandleResponse(tokenResponse);
        }

        // DELETE: api/Authorize/log-out
        [HttpDelete("log-out/{refreshToken}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LogOutAsync([FromRoute] string refreshToken)
        {
            string accessToken = HttpContext.Request.Headers.Authorization;

            var accessTokenResponse = await _tokenService.RemoveAsync(accessToken);

            if (accessTokenResponse.StatusCode == DocConnectStatusCode.Unauthorized)
            {
                return Unauthorized(accessTokenResponse.ErrorMessages);
            }

            var refreshTokenResponse = await _tokenService.RemoveAsync(refreshToken);

            if (refreshTokenResponse.StatusCode == DocConnectStatusCode.Unauthorized)
            {
                return Unauthorized(refreshTokenResponse.ErrorMessages);
            }

            await _authorizeService.LogOutAsync();

            return this.HandleResponse(refreshTokenResponse);
        }

        // POST: api/Authorize/sign-up
        [HttpPost("sign-up")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SignUpAsync([FromBody] UserRegistrationDTO request)
        {
            var response = await _authorizeService.SignUpAsync(request);

            if (response.StatusCode == DocConnectStatusCode.BadRequest)
            {
                return BadRequest(response.ErrorMessages);
            }

            var emailResponse = await _emailService.SendEmailVerification(response.Data);

            return this.HandleResponse(emailResponse);
        }

        // GET: api/Authorize/confirm-email/user@example.com/dtshstd434tfgre-dsf4w
        [HttpGet("confirm-email/{email}/{token}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmailAsync([FromRoute] string email, [FromRoute] string token)
        {
            var response = await _authorizeService.ConfirmUserEmailAsync(email, token);

            if (response.StatusCode == DocConnectStatusCode.NotFound)
            {
                return NotFound(response.ErrorMessages);
            }

            if (response.StatusCode == DocConnectStatusCode.BadRequest)
            {
                return BadRequest(response.ErrorMessages);
            }

            return this.HandleResponse(response);
        }

        // GET: api/Authorize/resend-verification-email/user@example.com
        [HttpGet("resend-verification-email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResendVerificationEmailAsync([FromRoute] string email)
        {
            var response = await _authorizeService.ResendVerificationEmailAsync(email);

            if (response.StatusCode == DocConnectStatusCode.NotFound)
            {
                return NotFound(response.ErrorMessages);
            }

            if (response.StatusCode == DocConnectStatusCode.BadRequest)
            {
                return BadRequest(response.ErrorMessages);
            }

            var emailResponse = await _emailService.SendEmailVerification(response.Data);

            return this.HandleResponse(emailResponse);
        }

        // GET: api/Authorize/request-access-token/asdasdasdasdasdsa
        [HttpGet("request-access-token/{refreshToken}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RequestAccessTokenAsync([FromRoute] string refreshToken)
        {
            string accessToken = HttpContext.Request.Headers.Authorization;
            string email = User.GetEmail();

            var accessTokenResponse = await _tokenService.RequestAccessTokenAsync(email, accessToken, refreshToken);

            return this.HandleResponse(accessTokenResponse);
        }

        [HttpGet("forgot-password/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ForgotPasswordAsync([FromRoute] string email)
        {
            var response = await _authorizeService.ForgotPasswordAsync(email);

            if (response.StatusCode == DocConnectStatusCode.NotFound)
            {
                return NotFound(response.ErrorMessages);
            }

            if (response.StatusCode == DocConnectStatusCode.BadRequest)
            {
                return BadRequest(response.ErrorMessages);
            }

            var emailResponse = await _emailService.SendPasswordResetAsync(response.Data);

            return this.HandleResponse(emailResponse);
        }

        [HttpGet("validate-reset-password-token/{email}/{token}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ValidateResetTokenAsync([FromRoute] string email, [FromRoute] string token)
        {
            var response = await _authorizeService.ValidateResetTokenAsync(email, token);

            if (response.StatusCode == DocConnectStatusCode.NotFound)
            {
                return NotFound(response.ErrorMessages);
            }

            if (response.StatusCode == DocConnectStatusCode.BadRequest)
            {
                return BadRequest(response.ErrorMessages);
            }

            return this.HandleResponse(response);
        }

        [HttpPost("reset-password/{email}/{token}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPasswordAsync([FromRoute] string email, [FromRoute] string token, [FromBody] UserResetPasswordDTO userResetPasswordDTO)
        {
            var response = await _authorizeService.ResetPasswordAsync(email, token, userResetPasswordDTO);

            if (response.StatusCode == DocConnectStatusCode.BadRequest)
            {
                return BadRequest(response.ErrorMessages);
            }

            return this.HandleResponse(response);
        }
    }
}
