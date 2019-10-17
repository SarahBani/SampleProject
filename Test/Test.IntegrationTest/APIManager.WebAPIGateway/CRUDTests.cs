using Core.DomainModel;
using Core.DomainModel.Entities;
using Core.DomainService;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Test.Common.Models;

namespace Test.IntegrationTest.APIManager.WebAPIGateway
{
    [TestFixture]
    public class CRUDTests : BaseControllerTests
    {

        #region Properties

        private string _authToken;

        private readonly string _authUrl = "/auth";

        private readonly string _getBanksUrl = "/banks";

        private readonly string _getBankByIdUrl = "/bank/{0}";

        private readonly string _postBankUrl = "/bank";

        private readonly string _putBankUrl = "/bank";

        #endregion /Properties

        #region Constructors

        public CRUDTests()
            : base()
        {
        }

        #endregion /Constructors

        #region Methods

        [OneTimeSetUp]
        public override void Setup()
        {
            this._authToken = GetJWTAuthenticationToken().Result;
            base.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._authToken);
        }

        [Test]
        public async Task GetAsync_Banks_ReturnsOK()
        {
            // Arrange
            string url = this._getBanksUrl;

            //Act
            var result = await base.Client.GetAsync(url);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK, "error in returning correct response");
            var reponseBanks = await base.GetDeserializedContent<IList<Bank>>(result);
            Assert.IsInstanceOf<IList<Bank>>(reponseBanks, "error in returning the banks");
        }

        [Test]
        public async Task GetAsync_BankById_ReturnsOK()
        {
            // Arrange
            int bankId = 5;
            string url = string.Format(this._getBankByIdUrl, bankId);

            //Act
            var result = await base.Client.GetAsync(url);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK, "error in returning correct response");
            var reponseBank = await base.GetDeserializedContent<Bank>(result);
            Assert.IsInstanceOf<Bank>(reponseBank, "error in returning the bank");
        }

        [Test]
        public async Task GetAsync_BankByZeroId_ReturnsNoContent()
        {
            // Arrange
            int bankId = 0;
            Bank expectedBank = null;
            string url = string.Format(this._getBankByIdUrl, bankId);

            //Act
            var result = await base.Client.GetAsync(url);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent, "error in returning correct response");
            var reponseBank = await base.GetDeserializedContent<Bank>(result);
            Assert.AreEqual(expectedBank, reponseBank, "error in returning the null bank");
        }

        [Test]
        public async Task PostAsync_Bank_ReturnsOK()
        {
            // Arrange
            var bank = new BankModel().Entity;
            bank.Id = 0;
            bank.Name = Guid.NewGuid().ToString(); // make the bank name unique to prevent index error
            var content = base.GetSerializedContent(bank);
            string url = this._postBankUrl;

            //Act
            var result = await base.Client.PostAsync(url, content);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created, "error in returning correct response");
            var reponseBank = await base.GetDeserializedContent<Bank>(result);
            Assert.IsInstanceOf<Bank>(reponseBank, "error in returning the bank");
            Assert.Less(0, reponseBank.Id, "error in returning the correct bankId");
        }

        [Test]
        public async Task PostAsync_BankNullName_ReturnsHasDuplicateInfoException()
        {
            // Arrange
            var bank = new BankModel().Entity;
            bank.Id = 0;
            bank.Name = null;
            var modelState = new ModelStateDictionary().AddModelRequiredError("Name");
            var expectedContent = base.GetModelStateContent(modelState);
            var content = base.GetSerializedContent(bank);
            string url = this._postBankUrl;

            //Act
            var result = await base.Client.PostAsync(url, content);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest, "error in returning correct response");
            var responseContent = await base.GetDeserializedContent<string>(result);
            Assert.AreEqual(expectedContent, responseContent, "error in returning correct content");
        }

        [Test]
        public async Task PostAsync_DuplicateBank_ReturnsHasDuplicateInfoException()
        {
            // Arrange
            var existedBank = await GetBank();
            var bank = existedBank;
            bank.Id = 0;
            var content = base.GetSerializedContent(bank);
            string expectedContent = Constant.Exception_sql_HasDuplicateInfo;
            string url = this._postBankUrl;

            //Act
            var result = await base.Client.PostAsync(url, content);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest, "error in returning correct response");
            var responseContent = await base.GetDeserializedContent<string>(result);
            Assert.AreEqual(expectedContent, responseContent, "error in returning correct content");
        }

        [Test]
        public async Task PutAsync_ReturnsOK()
        {
            // Arrange
            string url = this._putBankUrl;
            var bank = new BankModel().Entity;
            bank.Id = 0;
            bank.Name = Guid.NewGuid().ToString(); // make the bank name unique to prevent index error
            var content = base.GetSerializedContent(bank);

            //Act
            var result = await base.Client.PostAsync(url, content);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created, "error in returning correct response");
            var reponseBank = await base.GetDeserializedContent<Bank>(result);
            Assert.IsInstanceOf<Bank>(reponseBank, "error in returning the bank");
            Assert.Less(0, reponseBank.Id, "error in returning the correct bankId");
        }

        private async Task<string> GetJWTAuthenticationToken()
        {
            var responseMessage = await base.Client.PostAsync(this._authUrl, GetAuthenticationContent());
            return await responseMessage.Content.ReadAsStringAsync();
        }

        private StringContent GetAuthenticationContent()
        {
            var userCredential = new UserCredentialModel().Entity;
            return base.GetSerializedContent(userCredential);
        }

        private async Task<Bank> GetBank()
        {
            int bankId = 5;
            string url = string.Format(this._getBankByIdUrl, bankId);
            var result = await base.Client.GetAsync(url);
            return await base.GetDeserializedContent<Bank>(result);
        }

        #endregion /Methods

    }
}
