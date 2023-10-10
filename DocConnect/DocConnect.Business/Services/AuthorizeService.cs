using AutoMapper;
using DocConnect.Business.Abstraction.Factories;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.DTOs.User;
using DocConnect.Business.Models.Results;
using DocConnect.Business.Models.Utilities;
using DocConnect.Data.Abstraction.Helpers;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;
using System.Text;

namespace DocConnect.Business.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IDocConnectSignInManager _docConnectSignInManager;
        private readonly IDocConnectUserManager _docConnectUserManager;
        private readonly IResultFactory _resultFactory;
        private readonly IMapper _mapper;
        private static readonly IDictionary<string, int> _retryCounts = new Dictionary<string, int>();
        private static readonly IDictionary<string, DateTime> _verificationToken = new Dictionary<string, DateTime>();
        private static readonly IDictionary<string, DateTime> _passwordResetToken = new Dictionary<string, DateTime>();

        public AuthorizeService(
            IDocConnectUserManager docConnectUserManager,
            IDocConnectSignInManager docConnectSignInManager,
            IResultFactory resultFactory,
            IMapper mapper)
        {
            _docConnectUserManager = docConnectUserManager;
            _resultFactory = resultFactory;
            _mapper = mapper;
            _docConnectSignInManager = docConnectSignInManager;
        }

        public async Task<IResult<UserResultDTO>> LoginAsync(UserLoginDTO userLoginDTO)
        {
            var existingUser = await _docConnectUserManager.FindByEmailAsync(userLoginDTO.Email);

            if (existingUser == null)
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<UserResultDTO>(AuthorizeMessages.InvalidLogin);

                return badRequestResult;
            }

            var result = await _docConnectSignInManager.PasswordSignInAsync(existingUser, userLoginDTO.Password, false, false);

            if (!result.Succeeded)
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<UserResultDTO>(AuthorizeMessages.InvalidLogin);

                return badRequestResult;
            }

            if (!existingUser.EmailConfirmed)
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<UserResultDTO>(AuthorizeMessages.EmailIsNotConfirmed);

                return badRequestResult;
            }

            var userResultDTO = _mapper.Map<UserResultDTO>(existingUser);
            var okResult = _resultFactory.GetOkResult(userResultDTO);

            return okResult;
        }

        public async Task<IResult<UserResultDTO>> LogOutAsync()
        {
            await _docConnectSignInManager.SignOutAsync();

            var noContentResult = _resultFactory.GetNoContentResult<UserResultDTO>();

            return noContentResult;
        }

        public async Task<IResult<UserEmailCredentialsDTO>> SignUpAsync(UserRegistrationDTO userRegistrationDTO)
        {
            var existingUser = await _docConnectUserManager.FindByEmailAsync(userRegistrationDTO.Email);

            if (existingUser != null)
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<UserEmailCredentialsDTO>(AuthorizeMessages.UserAlreadyExists);

                return badRequestResult;
            }

            var user = _mapper.Map<User>(userRegistrationDTO);
            await _docConnectUserManager.CreateUserAsync(user, userRegistrationDTO.Password);

            var token = await _docConnectUserManager.GenerateConfirmEmailTokenAsync(user);

            var userEmailCredentialsDTO = _mapper.Map<UserEmailCredentialsDTO>(user);
            userEmailCredentialsDTO.Token = token;

            var okResult = _resultFactory.GetOkResult(userEmailCredentialsDTO);

            return okResult;
        }

        public async Task<IResult<UserResultDTO>> ConfirmUserEmailAsync(string email, string token)
        {
            var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var user = await _docConnectUserManager.FindByEmailAsync(email);

            if (user == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<UserResultDTO>();

                return notFoundResult;
            }

            if (user.EmailConfirmed)
            {
                var alreadyConfirmedResult = _resultFactory.GetBadRequestResult<UserResultDTO>(AuthorizeMessages.EmailAlreadyConfirmed);

                return alreadyConfirmedResult;
            }

            var result = await _docConnectUserManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(x => x.Description)
                    .ToArray();

                var badRequestResult = _resultFactory.GetBadRequestResult<UserResultDTO>(errors);

                return badRequestResult;
            }

            var noContentResult = _resultFactory.GetNoContentResult<UserResultDTO>();

            return noContentResult;
        }

        public async Task<IResult<UserEmailCredentialsDTO>> ResendVerificationEmailAsync(string email)
        {
            var user = await _docConnectUserManager.FindByEmailAsync(email);

            if (user == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<UserEmailCredentialsDTO>(AuthorizeMessages.UserNotFound);

                return notFoundResult;
            }

            if (user.EmailConfirmed)
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<UserEmailCredentialsDTO>(AuthorizeMessages.EmailAlreadyConfirmed);

                return badRequestResult;
            }

            if (_verificationToken.ContainsKey(email) && _verificationToken[email] > DateTime.Now)
            {
                if (_retryCounts.ContainsKey(email) && _retryCounts[email] >= 3)
                {
                    var supportMessageResult = _resultFactory.GetBadRequestResult<UserEmailCredentialsDTO>(AuthorizeMessages.ContactSupportForResend);

                    return supportMessageResult;
                }

                _retryCounts[email] = _retryCounts.ContainsKey(email) ? _retryCounts[email] + 1 : 1;

                var alreadySentResult = _resultFactory.GetBadRequestResult<UserEmailCredentialsDTO>(AuthorizeMessages.VerificationEmailAlreadySent);

                return alreadySentResult;
            }

            var token = await _docConnectUserManager.GenerateConfirmEmailTokenAsync(user);

            // Store the token and its expiration date
            _verificationToken[email] = DateTime.Now.AddHours(1); // Token is valid for 1 hour

            // Reset the retry count when a new verification email is sent
            if (_retryCounts.ContainsKey(email))
            {
                _retryCounts[email] = default;
            }

            var userEmailCredentialsDTO = _mapper.Map<UserEmailCredentialsDTO>(user);
            userEmailCredentialsDTO.Token = token;

            var okResult = _resultFactory.GetOkResult(userEmailCredentialsDTO);

            return okResult;
        }

        public async Task<IResult<UserForgotPasswordDTO>> ForgotPasswordAsync(string email)
        {
            var user = await _docConnectUserManager.FindByEmailAsync(email);

            if (user == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<UserForgotPasswordDTO>(AuthorizeMessages.UserNotFound);

                return notFoundResult;
            }

            if (_passwordResetToken.ContainsKey(email) && _passwordResetToken[email] > DateTime.Now)
            {
                var alreadySentResult = _resultFactory.GetBadRequestResult<UserForgotPasswordDTO>(AuthorizeMessages.ResetPasswordEmailAlreadySent);

                return alreadySentResult;
            }

            var token = await _docConnectUserManager.GenerateResetPasswordTokenAsync(user);

            // Store the token and its expiration date
            _passwordResetToken[email] = DateTime.Now.AddHours(1); // Token is valid for 1 hour

            var userForgotPasswordDTO = _mapper.Map<UserForgotPasswordDTO>(user);
            userForgotPasswordDTO.Token = token;

            var okResult = _resultFactory.GetOkResult(userForgotPasswordDTO);

            return okResult;
        }

        public async Task<IResult<ValidateResetTokenDTO>> ValidateResetTokenAsync(string email, string token)
        {
            var decodedEmail = Encoding.UTF8.GetString(Convert.FromBase64String(email));
            var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var user = await _docConnectUserManager.FindByEmailAsync(decodedEmail);

            var result = await _docConnectUserManager.IsValidTokenAsync(user, "Default", "ResetPassword", decodedToken);

            // Check if the token exists and is not expired
            if (!result)
            {
                // Token is either not found or expired
                var notFoundResult = _resultFactory.GetNotFoundResult<ValidateResetTokenDTO>(AuthorizeMessages.TokenExpiredOrInvalid);
                return notFoundResult;
            }

            var validateResetTokenDTO = new ValidateResetTokenDTO()
            {
                Email = email,
                Token = token
            };

            var okResult = _resultFactory.GetOkResult(validateResetTokenDTO);
            return okResult;
        }

        public async Task<IResult<ValidateResetTokenDTO>> ResetPasswordAsync(string email, string token, UserResetPasswordDTO userResetPasswordDTO)
        {
            var decodedEmail = Encoding.UTF8.GetString(Convert.FromBase64String(email));
            var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var user = await _docConnectUserManager.FindByEmailAsync(decodedEmail);

            if (user == null)
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<ValidateResetTokenDTO>(AuthorizeMessages.UserNotFound);

                return badRequestResult;
            }

            var result = await _docConnectUserManager.ResetPasswordAsync(user, decodedToken, userResetPasswordDTO.NewPassword, userResetPasswordDTO.ConfirmPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(x => x.Description)
                    .ToArray();

                var badRequestResult = _resultFactory.GetBadRequestResult<ValidateResetTokenDTO>(errors);

                return badRequestResult;
            }

            var noContentResult = _resultFactory.GetNoContentResult<ValidateResetTokenDTO>();

            return noContentResult;
        }
    }
}
