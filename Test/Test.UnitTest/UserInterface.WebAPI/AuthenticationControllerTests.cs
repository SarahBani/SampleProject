﻿using Core.ApplicationService.Contracts;
using Core.DomainModel;
using Core.DomainService;
using Core.DomainService.Models;
using MicroService.AuthenticationService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Test.Common;
using Test.Common.Models;

namespace Test.UnitTest.UserInterface.WebAPI
{
    [TestFixture]
    public class AuthenticationControllerTests
    {

        #region Properties

        private Mock<IAuthenticationService> _authServiceMock;

        private AuthenticationController _controller;

        #endregion /Properties

        #region Constructors

        public AuthenticationControllerTests()
        {

        }

        #endregion /Constructors

        #region Methods

        [OneTimeSetUp]
        public void Setup()
        {
            this._authServiceMock = new Mock<IAuthenticationService>();
            this._controller = new AuthenticationController(this._authServiceMock.Object);
        }

        [Test]
        public async Task RequestToken_ReturnsOK()
        {
            // Arrange
            var request = new UserCredentialModel().Entity;
            string authenticationToken = "sample_authentication_token";
            var expectedResult = new OkObjectResult(authenticationToken);
            this._authServiceMock.Setup(q => q.IsAuthenticated(request)).ReturnsAsync(true);
            this._authServiceMock.Setup(q => q.GetAuthenticationToken(request)).Returns(authenticationToken);

            //Act
            var result = await this._controller.RequestToken(request);

            // Assert
            this._authServiceMock.Verify(q => q.IsAuthenticated(It.IsAny<UserCredential>()),
                "error in calling the correct method");  // Verifies that authService.IsAuthenticated was called
            this._authServiceMock.Verify(q => q.GetAuthenticationToken(It.IsAny<UserCredential>()),
                "error in calling the correct method");  // Verifies that authService.GetAuthenticationToken was called
            TestHelper.AreEqualEntities(expectedResult, result, "error in returning the correct authentication token");
        }

        [Test]
        public async Task RequestToken_InvalidUserCredential_ReturnsBadRequest()
        {
            // Arrange
            var request = new UserCredentialModel().NullUserNameEntity;
            new DataAnnotationsValidator().TryValidate(request, out ICollection<ValidationResult> modelState);
            var validationResult = modelState.First();
            this._controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            var expectedResult = new BadRequestObjectResult(this._controller.ModelState);

            //Act
            var result = await this._controller.RequestToken(request);

            // Assert
            TestHelper.AreEqualEntities(expectedResult, result, "error in returning the correct BadRequestObjectResult");
        }

        [Test]
        public async Task RequestToken_NotAuthenticatedUser_ReturnsBadRequest()
        {
            // Arrange
            var request = new UserCredentialModel().NotAuthenticatedEntity;
            string authenticationToken = "sample_authentication_token";
            var expectedResult = new BadRequestObjectResult(Constant.Exception_InvalidAuthentication);
            this._authServiceMock.Setup(q => q.IsAuthenticated(request)).ReturnsAsync(false);
            this._authServiceMock.Setup(q => q.GetAuthenticationToken(request)).Returns(authenticationToken);

            //Act
            var result = await this._controller.RequestToken(request);

            // Assert
            this._authServiceMock.Verify(q => q.IsAuthenticated(It.IsAny<UserCredential>()),
                "error in calling the correct method");  // Verifies that authService.IsAuthenticated was called
            TestHelper.AreEqualEntities(expectedResult, result, "error in returning the correct BadRequestObjectResult");
        }

        #endregion /Methods

    }
}
