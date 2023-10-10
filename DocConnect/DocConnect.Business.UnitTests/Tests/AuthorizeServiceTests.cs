using AutoMapper;
using DocConnect.Business.Abstraction.Factories;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Factories;
using DocConnect.Business.Models.DTOs.User;
using DocConnect.Business.Models.Enums;
using DocConnect.Business.Profiles;
using DocConnect.Business.Services;
using DocConnect.Business.UnitTests.Utilities;
using DocConnect.Data.Abstraction.Helpers;
using Moq;
using NUnit.Framework;
using System.Text;

namespace DocConnect.Business.UnitTests.Tests
{
    [TestFixture]
    public class AuthorizeServiceTests
    {
        private IMapper _mapper;
        private IResultFactory _resultFactory;
        private IAuthorizeService _authorizeService;
        private Mock<IDocConnectSignInManager> _docConnectSignInManager;
        private Mock<IDocConnectUserManager> _docConnectUserManagerMock;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<DocConnectProfile>();
            });

            _mapper = config.CreateMapper();
            _resultFactory = new ResultFactory();
            _docConnectSignInManager = new Mock<IDocConnectSignInManager>();
            _docConnectUserManagerMock = new Mock<IDocConnectUserManager>();

            _authorizeService = new AuthorizeService(_docConnectUserManagerMock.Object,
                _docConnectSignInManager.Object,
                _resultFactory,
                _mapper);

            _docConnectUserManagerMock
                .Setup(s => s.FindByEmailAsync(AuthorizeTestConstants.TestNonExistingUserEmail))
                .ReturnsAsync(AuthorizeTestConstants.TestNonExistingUser);

            _docConnectUserManagerMock
                .Setup(s => s.FindByEmailAsync(AuthorizeTestConstants.TestExistingUserEmail))
                .ReturnsAsync(AuthorizeTestConstants.TestExistingUser);

            _docConnectUserManagerMock
                .Setup(s => s.GenerateConfirmEmailTokenAsync(AuthorizeTestConstants.TestNonExistingUser))
                .ReturnsAsync(AuthorizeTestConstants.TestEmailConfirmationToken);

            _docConnectUserManagerMock
                .Setup(s => s.GenerateConfirmEmailTokenAsync(AuthorizeTestConstants.TestExistingUser))
                .ReturnsAsync(AuthorizeTestConstants.TestEmailInvalidToken);

            //_docConnectSignInManager
            //    .Setup(s => s.PasswordSignInAsync(AuthorizeTestConstants.TestExistingUser, AuthorizeTestConstants.TestValidPassword, false, false))
            //    .ReturnsAsync(true);

            //_docConnectSignInManager
            //    .Setup(s => s.PasswordSignInAsync(AuthorizeTestConstants.TestExistingUser, AuthorizeTestConstants.TestInvalidPassword, false, false))
            //    .ReturnsAsync(false);
        }

        [Test]
        public async Task SignUpAsync_HasUserLoginDTOWithInvalidEmail_ReturnsBadRequestResult()
        {
            // Act
            var result = await _authorizeService.SignUpAsync(AuthorizeTestConstants.TestInvalidUserRegistrationDTO);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.BadRequest));
        }

        //[Test]
        //public async Task SignUpAsync_HasUserLoginDTOWithValidEmail_ReturnsNoContentResult()
        //{
        //    // Act
        //    var result = await _authorizeService.SignUpAsync(AuthorizeTestConstants.TestValidUserRegistrationDTO);

        //    // Assert
        //    Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.NoContent));
        //}

        [Test]
        public async Task LoginAsync_HasLoginDTOWithInvalidEmail_ReturnsBadRequestResult()
        {
            // Act
            var result = await _authorizeService.LoginAsync(AuthorizeTestConstants.TestUserLoginDTOWithInvalidEmail);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.BadRequest));
        }

        //[Test]
        //public async Task LoginAsync_HasLoginDTOWithValidEmail_ReturnsOkResult()
        //{
        //    // Act
        //    var result = await _authorizeService.LoginAsync(AuthorizeTestConstants.TestValidUserLoginDTO);

        //    // Assert
        //    Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.OK));
        //}

        //[Test]
        //public async Task LoginAsync_HasLoginDTOWithInvalidPassword_ReturnsBadRequestResult()
        //{
        //    // Act
        //    var result = await _authorizeService.LoginAsync(AuthorizeTestConstants.TestUserLoginDTOWithInvalidPassword);

        //    // Assert
        //    Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.BadRequest));
        //}

        //[Test]
        //public async Task LoginAsync_HasLoginDTOWithValidPassword_ReturnsOkResult()
        //{
        //    // Act
        //    var result = await _authorizeService.LoginAsync(AuthorizeTestConstants.TestValidUserLoginDTO);

        //    // Assert
        //    Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.OK));
        //}

        [Test]
        public async Task ResendVerificationEmailAsync_UserNotFound_ReturnsNotFoundResult()
        {
            // Act
            var result = await _authorizeService.ResendVerificationEmailAsync(AuthorizeTestConstants.TestNonExistingUserEmail);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.NotFound));
        }

        [Test]
        public async Task ResendVerificationEmailAsync_ValidUser_ReturnsOkResult()
        {
            // Act
            var result = await _authorizeService.ResendVerificationEmailAsync(AuthorizeTestConstants.TestExistingUserEmail);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.OK));
        }

        [Test]
        public async Task ForgotPasswordAsync_UserNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var email = AuthorizeTestConstants.TestNonExistingUserEmail;

            // Act
            var result = await _authorizeService.ForgotPasswordAsync(email);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.NotFound));
        }

        [Test]
        public async Task ForgotPasswordAsync_PasswordResetTokenExists_ReturnsOKResult()
        {
            // Arrange
            var email = AuthorizeTestConstants.TestExistingUserEmail;

            // Act
            var result = await _authorizeService.ForgotPasswordAsync(email);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.OK));
        }

        [Test]
        public async Task ValidateResetTokenAsync_InvalidToken_ReturnsNotFoundResult()
        {
            // Arrange
            var email = AuthorizeTestConstants.TestInvalidBase64Email;
            var token = AuthorizeTestConstants.TestInvalidBase64Token;

            // Act
            var result = await _authorizeService.ValidateResetTokenAsync(email, token);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.NotFound));
        }

        [Test]
        public async Task ResetPasswordAsync_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var email = Convert.ToBase64String(Encoding.UTF8.GetBytes(AuthorizeTestConstants.TestNonExistingUserEmail));
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(AuthorizeTestConstants.TestEmailInvalidToken));
            var resetPasswordDTO = new UserResetPasswordDTO
            {
                NewPassword = AuthorizeTestConstants.TestNewPassword,
                ConfirmPassword = AuthorizeTestConstants.TestNewPassword
            };

            // Act
            var result = await _authorizeService.ResetPasswordAsync(email, token, resetPasswordDTO);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.BadRequest));
        }
    }
}
