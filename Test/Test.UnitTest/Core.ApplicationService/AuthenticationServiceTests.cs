﻿using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainService.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Test.Common.Models;

namespace Test.UnitTest.Core.ApplicationService
{
    [TestFixture]
    public class AuthenticationServiceTests
    {

        #region Properties

        private Mock<IOptions<AuthenticationAppSettings>> _appSettingsMock;

        private AuthenticationService _service;

        #endregion /Properties

        #region Constructors

        public AuthenticationServiceTests()
        {
        }

        #endregion /Constructors

        #region Methods

        [OneTimeSetUp]
        public void Setup()
        {
            this._appSettingsMock = new Mock<IOptions<AuthenticationAppSettings>>();
            this._appSettingsMock.Setup(q => q.Value.SecretKey).Returns("just_some_secret_big_key_value");

            var options = new DbContextOptions<SampleDataBaseContext>();
            var dbContextMock = new Mock<SampleDataBaseContext>(options);
            var userStore = new UserStore<IdentityUser>(dbContextMock.Object);
            var userManager = new UserManager<IdentityUser>(userStore, null, null, null, null, null, null, null, null);
            var roleStore = new RoleStore<IdentityRole>(dbContextMock.Object);
            var roleManager = new RoleManager<IdentityRole>(roleStore, null, null, null, null);
            //this._service = new AuthenticationService(this._appSettingsMock.Object, userManager, roleManager);
        }

        [Test]
        public async Task IsAuthenticated_ReturnsOK()
        {
            // Arrange
            var request = new UserCredentialModel().Entity;

            //Act
            var result = await this._service.IsAuthenticated(request);

            // Assert
            //base.RepositoryMock.Verify(q => q.GetCountAsync(expression),
            //    "error in calling the correct method");  // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(true, result, "error in returning correct value");
        }

        [Test]
        public async Task GetAuthenticationToken_ReturnsOK()
        {
            // Arrange
            var request = new UserCredentialModel().Entity;

            //Act
            var result = await Task.Run(() => this._service.GetAuthenticationToken(request));

            // Assert
            Assert.AreNotEqual(string.Empty, result, "error in returning a valid token");
        }

        #endregion /Methods

    }
}
