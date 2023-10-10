using AutoMapper;
using DocConnect.Business.Abstraction.Factories;
using DocConnect.Business.Abstraction.Helpers;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.DTOs.Token;
using DocConnect.Business.Models.DTOs.User;
using DocConnect.Business.Models.Results;
using DocConnect.Business.Models.Utilities;
using DocConnect.Data.Abstraction.Helpers;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;

namespace DocConnect.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenHandler _tokenHandler;
        private readonly IResultFactory _resultFactory;
        private readonly IDocConnectUserManager _docConnectUserManager;

        public TokenService(
            IMapper mapper,
            ITokenRepository tokenRepository,
            ITokenHandler tokenHandler,
            IResultFactory resultFactory,
            IDocConnectUserManager docConnectUserManager)
        {
            _mapper = mapper;
            _tokenRepository = tokenRepository;
            _tokenHandler = tokenHandler;
            _resultFactory = resultFactory;
            _docConnectUserManager = docConnectUserManager;
        }

        public async Task<IResult<ClientTokenResultDTO>> AddAsync(UserResultDTO userResultDTO)
        {
            var tokenGenerateDTO = _mapper.Map<TokenGenerateDTO>(userResultDTO);

            var accessTokenAddDTO = _tokenHandler.GenerateAccessToken(tokenGenerateDTO);
            var refreshTokenAddDTO = _tokenHandler.GenerateRefreshToken(tokenGenerateDTO);

            var accessToken = _mapper.Map<Token>(accessTokenAddDTO);
            var refreshToken = _mapper.Map<Token>(refreshTokenAddDTO);

            await _tokenRepository.AddAsync(accessToken);
            await _tokenRepository.AddAsync(refreshToken);

            var accessTokenResultDTO = _mapper.Map<TokenResultDTO>(accessToken);
            var refreshTokenResultDTO = _mapper.Map<TokenResultDTO>(refreshToken);

            var result = new ClientTokenResultDTO
            {
                AccessTokenResultDTO = accessTokenResultDTO,
                RefreshTokenResultDTO = refreshTokenResultDTO
            };

            var okResult = _resultFactory.GetOkResult(result);

            return okResult;
        }

        public async Task<IResult<TokenResultDTO>> GetByValueAsync(string value)
        {
            if (value == null)
            {
                var unauthorizedResult = _resultFactory.GetUnauthorizedResult<TokenResultDTO>(TokenMessages.TokenNotInHeader);

                return unauthorizedResult;
            }

            var token = await _tokenRepository.GetByValueAsync(value);

            if (token == null)
            {
                var unauthorizedResult = _resultFactory.GetUnauthorizedResult<TokenResultDTO>(TokenMessages.TokenNotFound);

                return unauthorizedResult;
            }

            var tokenResultDTO = _mapper.Map<TokenResultDTO>(token);
            var okResult = _resultFactory.GetOkResult(tokenResultDTO);

            return okResult;
        }

        public async Task<IResult<TokenResultDTO>> RemoveAsync(string tokenValue)
        {
            if (tokenValue == null)
            {
                var unauthorizedResult = _resultFactory.GetUnauthorizedResult<TokenResultDTO>(TokenMessages.TokenNotInHeader);

                return unauthorizedResult;
            }

            var token = await _tokenRepository.GetByValueAsync(tokenValue);

            if (token == null)
            {
                var unauthorizedResult = _resultFactory.GetUnauthorizedResult<TokenResultDTO>(TokenMessages.TokenNotFound);

                return unauthorizedResult;
            }

            await _tokenRepository.DeleteAsync(token);

            var noContentResult = _resultFactory.GetNoContentResult<TokenResultDTO>();

            return noContentResult;
        }

        public async Task<IResult<TokenResultDTO>> RequestAccessTokenAsync(string email, string accessToken, string refreshToken)
        {
            if (accessToken == null)
            {
                var unauthorizedResult = _resultFactory.GetUnauthorizedResult<TokenResultDTO>(TokenMessages.RefreshTokenValueInHeadersIsNull);

                return unauthorizedResult;
            }

            var oldAccessToken = await _tokenRepository.GetByValueAsync(accessToken);

            if (oldAccessToken == null)
            {
                var unauthorizedResult = _resultFactory.GetUnauthorizedResult<TokenResultDTO>(TokenMessages.TokenNotFound);

                return unauthorizedResult;
            }

            var user = await _docConnectUserManager.FindByEmailAsync(email);

            if (user == null)
            {
                var badRequest = _resultFactory.GetBadRequestResult<TokenResultDTO>(AuthorizeMessages.UserNotFound);

                return badRequest;
            }

            var tokenGenerateDTO = _mapper.Map<TokenGenerateDTO>(user);

            var generatedAccessToken = _tokenHandler.GenerateAccessToken(tokenGenerateDTO);
            var newAccessToken = _mapper.Map<Token>(generatedAccessToken);

            await _tokenRepository.DeleteAsync(oldAccessToken);
            await _tokenRepository.AddAsync(newAccessToken);

            var newAccessTokenResultDTO = _mapper.Map<TokenResultDTO>(newAccessToken);

            var okResult = _resultFactory.GetOkResult(newAccessTokenResultDTO);

            return okResult;
        }
    }
}
