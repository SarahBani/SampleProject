using Authentication.Core.ApplicationService.Contracts;
using Authentication.WebAPIService.Controllers;
using Core.DomainModel;
using Core.DomainService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Test.Common;
using Test.Common.Models;

namespace Authentication.UnitTest.WebAPIService
{
    [TestFixture]
    public class AuthenticationControllerTests
    {

        #region Properties

        private Mock<IAuthService> _authServiceMock;

        private AuthenticationController _controller;

        #endregion /Properties

        #region Constructors

        public AuthenticationControllerTests()
        {

        }

        #endregion /Constructors

        #region Methods

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this._authServiceMock = new Mock<IAuthService>();
            this._controller = new AuthenticationController(this._authServiceMock.Object);
        }        

        [SetUp]
        public void SetUp()
        {
            this._controller.ModelState.Clear();
        }

        [Test]
        public async Task RequestToken_ReturnsOK()
        {
            // Arrange
            var userCredential = new UserCredentialModel().Entity;
            string authenticationToken = "sample_authentication_token";
            var expectedResult = new OkObjectResult(authenticationToken);
            this._authServiceMock.Setup(q => q.IsAuthenticated(userCredential)).ReturnsAsync(true);
            this._authServiceMock.Setup(q => q.GetAuthenticationToken(userCredential)).ReturnsAsync(authenticationToken);

            //Act
            var result = await this._controller.RequestToken(userCredential);

            // Assert
            this._authServiceMock.Verify(q => q.IsAuthenticated(userCredential),
                "error in calling the correct method");  // Verifies that authService.IsAuthenticated was called
            this._authServiceMock.Verify(q => q.GetAuthenticationToken(userCredential),
                "error in calling the correct method");  // Verifies that authService.GetAuthenticationToken was called
            TestHelper.AreEqualEntities(expectedResult, result, "error in returning the correct authentication token");
        }

        [Test]
        public async Task RequestToken_InvalidUserCredential_ReturnsBadRequest()
        {
            // Arrange
            var userCredential = new UserCredentialModel().NullUserNameEntity;
            new DataAnnotationsValidator().TryValidate(userCredential, out ICollection<ValidationResult> modelState);
            var validationResult = modelState.First();
            this._controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            var expectedResult = new BadRequestObjectResult(this._controller.ModelState);

            //Act
            var result = await this._controller.RequestToken(userCredential);

            // Assert
            TestHelper.AreEqualEntities(expectedResult, result, "error in returning the correct BadRequestObjectResult");
        }

        [Test]
        public async Task RequestToken_NotAuthenticatedUser_ReturnsBadRequest()
        {
            // Arrange
            var userCredential = new UserCredentialModel().NotAuthenticatedEntity;
            string authenticationToken = "sample_authentication_token";
            var expectedResult = new BadRequestObjectResult(Constant.Exception_InvalidAuthentication);
            this._authServiceMock.Setup(q => q.IsAuthenticated(userCredential)).ReturnsAsync(false);
            this._authServiceMock.Setup(q => q.GetAuthenticationToken(userCredential)).ReturnsAsync(authenticationToken);

            //Act
            var result = await this._controller.RequestToken(userCredential);

            // Assert
            this._authServiceMock.Verify(q => q.IsAuthenticated(userCredential),
                "error in calling the correct method");  // Verifies that authService.IsAuthenticated was called
            TestHelper.AreEqualEntities(expectedResult, result, "error in returning the correct BadRequestObjectResult");
        }

        #endregion /Methods

    }
}
