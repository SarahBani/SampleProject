using Core.DomainService;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "error in returning correct response");
        }

        [Test]
        public async Task PostAsync_NullUserName_ReturnsValidationException()
        {
            // Arrange
            var userCredential = new UserCredentialModel().NullUserNameEntity;
            var requestContent = base.GetSerializedContent(userCredential);
            var modelState = new ModelStateDictionary().AddModelRequiredError("Username");
            var expectedContent = base.GetModelStateContent(modelState);
            string url = this._postUrl;

            //Act
            var result = await base.Client.PostAsync(url, requestContent);

            // Assert
            string content = await base.GetDeserializedContent<string>(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode, "error in returning correct response");
            Assert.AreEqual(expectedContent, content, "error in returning correct content");
        }

        [Test]
        public async Task PostAsync_NullPassword_ReturnsValidationException()
        {
            // Arrange
            var userCredential = new UserCredentialModel().NullPasswordEntity;
            var requestContent = base.GetSerializedContent(userCredential);
            var modelState = new ModelStateDictionary().AddModelRequiredError("Password");
            var expectedContent = base.GetModelStateContent(modelState);
            string url = this._postUrl;

            //Act
            var result = await base.Client.PostAsync(url, requestContent);

            // Assert
            string content = await base.GetDeserializedContent<string>(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode, "error in returning correct response");
            Assert.AreEqual(expectedContent, content, "error in returning correct content");
        }

        //[Test]
        //public async Task PostAsync_InvalidAuthentication_ReturnsInvalidAuthenticationException()
        //{
        //    // Arrange
        //    var userCredential = new UserCredentialModel().NotAuthenticatedEntity;
        //    var requestContent = base.GetSerializedContent(userCredential);
        //    var expectedContent = Constant.Exception_InvalidAuthentication;
        //    string url = this._postUrl;

        //    //Act
        //    var result = await base.Client.PostAsync(url, requestContent);

        //    // Assert
        //    string content = await base.GetDeserializedContent<string>(result);
        //    Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode, "error in returning correct response");
        //    Assert.AreEqual(expectedContent, content, "error in returning correct content");
        //}

        #endregion /Methods

    }
}
