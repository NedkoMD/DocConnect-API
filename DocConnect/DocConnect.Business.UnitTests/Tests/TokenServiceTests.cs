using AutoMapper;
using DocConnect.Business.Abstraction.Factories;
using DocConnect.Business.Abstraction.Helpers;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Factories;
using DocConnect.Business.Models.Enums;
using DocConnect.Business.Profiles;
using DocConnect.Business.Services;
using DocConnect.Business.UnitTests.Utilities;
using DocConnect.Data.Abstraction.Helpers;
using DocConnect.Data.Abstraction.Repositories;
using Moq;
using NUnit.Framework;

namespace DocConnect.Business.UnitTests.Tests
{
    [TestFixture]
    public class TokenServiceTests
    {
        private Mock<IAuthorizeService> _authorizeService;
        private IMapper _mapper;
        private IResultFactory _resultFactory;
        private Mock<ITokenHandler> _tokenHandlerMock;
        private Mock<ITokenRepository> _tokenRepositoryMock;
        private Mock<IDocConnectUserManager> _docConnectUserManagerMock;
        private ITokenService _tokenService;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<DocConnectProfile>();
            });

            _authorizeService = new Mock<IAuthorizeService>();
            _mapper = config.CreateMapper();
            _resultFactory = new ResultFactory();
            _tokenHandlerMock = new Mock<ITokenHandler>();
            _tokenRepositoryMock = new Mock<ITokenRepository>();
            _docConnectUserManagerMock = new Mock<IDocConnectUserManager>();

            _tokenService = new TokenService(_mapper, _tokenRepositoryMock.Object, _tokenHandlerMock.Object, _resultFactory, _docConnectUserManagerMock.Object);

            _tokenRepositoryMock
                .Setup(s => s.GetByValueAsync(TokenTestConstants.TestExistingTokenValue))
                .ReturnsAsync(TokenTestConstants.TestExistingToken);

            _tokenRepositoryMock
                .Setup(s => s.GetByValueAsync(TokenTestConstants.TestNonExistingTokenValue))
                .ReturnsAsync(TokenTestConstants.TestNonExistingToken);
        }

        [Test]
        public async Task GetByValueAsync_HasTokenValueIsNull_ReturnsUnauthorizedResult()
        {
            // Act
            var result = await _tokenService.GetByValueAsync(TokenTestConstants.TestInvalidTokenValue);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.Unauthorized));
        }

        [Test]
        public async Task GetByValueAsync_HasTokenValueIsNotNull_ReturnsOkResult()
        {
            // Act
            var result = await _tokenService.GetByValueAsync(TokenTestConstants.TestExistingTokenValue);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.OK));
        }

        [Test]
        public async Task GetByValueAsync_HasTokenWithThatValueDoesNotExist_ReturnsUnathorizedResult()
        {
            // Act
            var result = await _tokenService.GetByValueAsync(TokenTestConstants.TestNonExistingTokenValue);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.Unauthorized));
        }

        [Test]
        public async Task GetByValueAsync_HasTokenWithThatValueExist_ReturnsOkResult()
        {
            // Act
            var result = await _tokenService.GetByValueAsync(TokenTestConstants.TestExistingTokenValue);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.OK));
        }

        [Test]
        public async Task DeleteAsync_HasTokenValueIsNull_ReturnsUnauthorizedResult()
        {
            // Act
            var result = await _tokenService.RemoveAsync(TokenTestConstants.TestInvalidTokenValue);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.Unauthorized));
        }

        [Test]
        public async Task DeleteAsync_HasTokenValueIsNotNull_ReturnsNoContentResult()
        {
            // Act
            var result = await _tokenService.RemoveAsync(TokenTestConstants.TestExistingTokenValue);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.NoContent));
        }

        [Test]
        public async Task DeleteAsync_HasTokenWithThatValueDoesNotExist_ReturnsUnauthorizedResult()
        {
            // Act
            var result = await _tokenService.RemoveAsync(TokenTestConstants.TestNonExistingTokenValue);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.Unauthorized));
        }

        [Test]
        public async Task DeleteAsync_HasTokenWithThatValueExist_ReturnsNoContentResult()
        {
            // Act
            var result = await _tokenService.RemoveAsync(TokenTestConstants.TestExistingTokenValue);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.NoContent));
        }

        //[Test]
        //public async Task RequestRefreshTokenAsync_ValidRefreshToken_ReturnsOkResult()
        //{
        //    // Arrange
        //    var refreshTokenValue = TokenTestConstants.TestExistingTokenValue;
        //    var oldRefreshToken = new Token
        //    {
        //        Value = refreshTokenValue,
        //        UserId = TokenTestConstants.ValidUintUserId,
        //    };

        //    var generatedRefreshToken = new TokenAddDTO
        //    {
        //        Value = TokenTestConstants.TestExistingTokenValue,
        //        UserId = TokenTestConstants.ValidUintUserId
        //    };

        //    _tokenRepositoryMock
        //        .Setup(s => s.GetByValueAsync(refreshTokenValue))
        //        .ReturnsAsync(oldRefreshToken);

        //    _tokenHandlerMock
        //        .Setup(s => s.GenerateRefreshToken(oldRefreshToken.UserId))
        //        .Returns(generatedRefreshToken);

        //    var newRefreshToken = _mapper.Map<Token>(generatedRefreshToken);

        //    // Act
        //    var result = await _tokenService.RequestRefreshTokenAsync(refreshTokenValue);

        //    // Assert
        //    Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.OK));
        //}

        //[Test]
        //public async Task RequestRefreshTokenAsync_NullRefreshToken_ReturnsUnauthorizedResult()
        //{
        //    // Arrange
        //    string refreshTokenValue = TokenTestConstants.TestInvalidTokenValue;

        //    // Act
        //    var result = await _tokenService.RequestRefreshTokenAsync(refreshTokenValue);

        //    // Assert
        //    Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.Unauthorized));
        //}

        //[Test]
        //public async Task RequestRefreshTokenAsync_NonExistingRefreshToken_ReturnsUnauthorizedResult()
        //{
        //    // Arrange
        //    var refreshTokenValue = TokenTestConstants.TestNonExistingTokenValue;

        //    _tokenRepositoryMock
        //        .Setup(s => s.GetByValueAsync(refreshTokenValue))
        //        .ReturnsAsync(TokenTestConstants.TestNullValueToken);

        //    // Act
        //    var result = await _tokenService.RequestAccessTokenAsync(refreshTokenValue);

        //    // Assert
        //    Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.Unauthorized));
        //}
    }
}
