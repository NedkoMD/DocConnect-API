using DocConnect.Business.Abstraction.Helpers;
using DocConnect.Business.Models.DTOs.Token;
using DocConnect.Business.Models.Options;
using DocConnect.Business.Models.Utilities;
using DocConnect.Data.Models.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DocConnect.Business.Helpers
{
    public class TokenHandler : ITokenHandler
    {
        private readonly TokenOptions _tokenOptions;

        public TokenHandler(TokenOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }

        public TokenAddDTO GenerateAccessToken(TokenGenerateDTO tokenGenerateDTO)
        {
            var claims = GetClaims(tokenGenerateDTO);
            var accessToken = GetAccessToken(claims);

            var value = $"{TokenConfigurations.TokenType} {accessToken}";

            var JWTAccessToken = new TokenAddDTO()
            {
                UserId = tokenGenerateDTO.Id,
                Value = value,
                Type = TokenConfigurations.AccessTokenType,
                ValidUntil = DateTime.UtcNow.AddSeconds(_tokenOptions.TokenLifetimeSeconds),
            };

            return JWTAccessToken;
        }

        public TokenAddDTO GenerateRefreshToken(TokenGenerateDTO tokenGenerateDTO)
        {
            var claims = GetClaims(tokenGenerateDTO);
            var refreshToken = GetRefreshToken(claims);

            var JWTRefreshToken = new TokenAddDTO()
            {
                UserId = tokenGenerateDTO.Id,
                Value = refreshToken,
                Type = TokenConfigurations.RefreshTokenType,
                ValidUntil = DateTime.UtcNow.AddHours(_tokenOptions.RefreshTokenLifetimeHours),
            };

            return JWTRefreshToken;
        }

        private string GetAccessToken(IEnumerable<Claim> claims)
        {
            var securityKeyAsBytes = Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey);
            var signingKey = new SymmetricSecurityKey(securityKeyAsBytes);

            var claimsIdentity = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature),
                Issuer = _tokenOptions.Issuer,
                Audience = _tokenOptions.Audience,
                Expires = DateTime.UtcNow.AddSeconds(_tokenOptions.TokenLifetimeSeconds)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        private IEnumerable<Claim> GetClaims(TokenGenerateDTO tokenGenerateDTO)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, tokenGenerateDTO.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, tokenGenerateDTO.Email),
                new Claim(ClaimConstants.FirstName, tokenGenerateDTO.FirstName),
                new Claim(ClaimConstants.LastName, tokenGenerateDTO.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
            };

            return claims;
        }

        private string GetRefreshToken(IEnumerable<Claim> claims)
        {
            var securityKeyAsBytes = Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey);
            var signingKey = new SymmetricSecurityKey(securityKeyAsBytes);

            var claimsIdentity = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature),
                Issuer = _tokenOptions.Issuer,
                Audience = _tokenOptions.Audience,
                Expires = DateTime.UtcNow.AddHours(_tokenOptions.RefreshTokenLifetimeHours)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
