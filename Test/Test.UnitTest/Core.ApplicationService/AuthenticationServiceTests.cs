using Core.ApplicationService.Implementation;
using Core.DomainService.Models;
using Core.DomainService.Settings;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

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

        [SetUp]
        public void Setup()
        {
            this._appSettingsMock = new Mock<IOptions<AuthenticationAppSettings>>();
            this._appSettingsMock.Setup(q => q.Value.SecretKey).Returns("just_some_secret_big_key_value");
            this._service = new AuthenticationService(this._appSettingsMock.Object);
        }

        #endregion /Methods

        [Test]
        public void IsAuthenticated_ReturnsOK()
        {
            // Arrange
            var request = new UserCredentials ()
            {
                Username = "User",
                Password = "123"
            };

            //Act
            var result = _service.IsAuthenticated(request).Result;

            // Assert
            //base.RepositoryMock.Verify(q => q.GetCountAsync(expression),
            //    "error in calling the correct method");  // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(true, result, "error in returning correct value");
        }

        [Test]
        public void GetAuthenticationToken_ReturnsOK()
        {
            // Arrange
            var request = new UserCredentials ()
            {
                Username = "User",
                Password = "123"
            };

            //Act
            var result = _service.GetAuthenticationToken(request);

            // Assert
            Assert.AreNotEqual(string.Empty, result, "error in returning a valid token");
        }

    }
}
