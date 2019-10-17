using Core.DomainModel;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using Test.Common.Models;

namespace Test.IntegrationTest.APIManager.WebAPIGateway
{
    [TestFixture]
    public class AuthenticationTests : BaseControllerTests
    {

        #region Properties

        private readonly string _postUrl = "/auth";

        #endregion /Properties

        #region Constructors

        public AuthenticationTests()
            : base()
        {
        }

        #endregion /Constructors

        #region Methods

        [OneTimeSetUp]
        public override void Setup()
        {
        }

        [Test]
        public async Task PostAsync_ReturnsOK()
        {
            // Arrange
            var userCredential = new UserCredentialModel().Entity;
            var requestContent = base.GetSerializedContent(userCredential);
            string url = this._postUrl;

            //Act
            var result = await base.Client.PostAsync(url, requestContent);

            // Assert
            string token = await base.GetDeserializedContent<string>(result);
            Assert.IsInstanceOf<string>(token, "error in returning the token");
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK, "error in returning correct response");
        }

        [Test]
        public async Task PostAsync_NullUserName_ReturnsBadRequest()
        {
            // Arrange
            var userCredential = new UserCredentialModel().NullUserNameEntity;
            var requestContent = base.GetSerializedContent(userCredential);
            string expectedContent = Constant.InvalidAuthentication;
            string url = this._postUrl;

            //Act
            var result = await base.Client.PostAsync(url, requestContent);

            // Assert
            string content = await base.GetDeserializedContent<string>(result);
            Assert.IsInstanceOf<string>(content, "error in returning the token");
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest, "error in returning correct response");
            Assert.AreEqual(expectedContent, content, "error in returning correct content");
        }

        [Test]
        public async Task PostAsync_NullPassword_ReturnsBadRequest()
        {
            // Arrange
            var userCredential = new UserCredentialModel().NullPasswordEntity;
            var requestContent = base.GetSerializedContent(userCredential);
            string expectedContent = Constant.InvalidAuthentication;
            string url = this._postUrl;

            //Act
            var result = await base.Client.PostAsync(url, requestContent);

            // Assert
            string content = await base.GetDeserializedContent<string>(result);
            Assert.IsInstanceOf<string>(content, "error in returning the token");
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest, "error in returning correct response");
            Assert.AreEqual(expectedContent, content, "error in returning correct content");
        }

        [Test]
        public async Task PostAsync_InvalidAuthentication_ReturnsBadRequest()
        {
            // Arrange
            var userCredential = new UserCredentialModel().Entity;
            var requestContent = base.GetSerializedContent(userCredential);
            string url = this._postUrl;

            //Act
            var result = await base.Client.PostAsync(url, requestContent);

            // Assert
            string token = await base.GetDeserializedContent<string>(result);
            Assert.IsInstanceOf<string>(token, "error in returning the token");
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest, "error in returning correct response");
        }

        #endregion /Methods

    }
}
