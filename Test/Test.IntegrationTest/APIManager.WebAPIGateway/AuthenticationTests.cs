using Core.ApplicationService.Contracts;
using Core.DomainModel;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Test.Common;
using Test.Common.Models;
using APIManager.WebAPIGateway;

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

        #endregion /Methods

    }
}
