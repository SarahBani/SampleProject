using Core.DomainModel;
using Core.DomainModel.Entities;
using Core.DomainService;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Test.Common;
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

        private readonly string _deleteBankUrl = "/bank/{0}";

        protected Bank Entity
        {
            get => new BankModel().Entity;
        }

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

        #region GetAsync

        [Test]
        public async Task GetAsync_Banks_ReturnsOK()
        {
            // Arrange
            string url = this._getBanksUrl;

            //Act
            var result = await base.Client.GetAsync(url);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "error in returning correct response");
            var reponseBanks = await base.GetDeserializedContent<IList<Bank>>(result);
            Assert.IsInstanceOf<IList<Bank>>(reponseBanks, "error in returning the banks");
        }

        [Test]
        public async Task GetAsync_BankById_ReturnsOK()
        {
            // Arrange
            var bank = await GetLastBank();
            if (bank == null) // no bank exists
            {
                Assert.Pass();
            }
            string url = string.Format(this._getBankByIdUrl, bank.Id);

            //Act
            var result = await base.Client.GetAsync(url);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "error in returning correct response");
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
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode, "error in returning correct response");
            var reponseBank = await base.GetDeserializedContent<Bank>(result);
            Assert.AreEqual(expectedBank, reponseBank, "error in returning the null bank");
        }

        [Test]
        public async Task GetAsync_BankNotExistId_ReturnsNoContent()
        {
            // Arrange
            int bankId = await GetLastBankId() + 1;
            Bank expectedBank = null;
            string url = string.Format(this._getBankByIdUrl, bankId);

            //Act
            var result = await base.Client.GetAsync(url);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode, "error in returning correct response");
            var reponseBank = await base.GetDeserializedContent<Bank>(result);
            Assert.AreEqual(expectedBank, reponseBank, "error in returning the null bank");
        }

        #endregion /GetAsync

        #region PostAsync

        [Test]
        public async Task PostAsync_Bank_ReturnsOK()
        {
            // Arrange
            var bank = this.Entity;
            bank.Id = 0;
            bank.Name = Guid.NewGuid().ToString(); // make the bank name unique to prevent index error
            var content = base.GetSerializedContent(bank);
            string url = this._postBankUrl;

            //Act
            var result = await base.Client.PostAsync(url, content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode, "error in returning correct response");
            var reponseBank = await base.GetDeserializedContent<Bank>(result);
            Assert.IsInstanceOf<Bank>(reponseBank, "error in returning the bank");
            Assert.Less(0, reponseBank.Id, "error in returning the correct bankId");
        }

        [Test]
        public async Task PostAsync_BankNull_ReturnsBadRequestValidationException()
        {
            // Arrange
            Bank bank = null;
            var content = base.GetSerializedContent(bank);
            string url = this._postBankUrl;

            //Act
            var result = await base.Client.PostAsync(url, content);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode, "error in returning correct response");
        }

        [Test]
        public async Task PostAsync_BankNullName_ReturnsBadRequestValidationException()
        {
            // Arrange
            var bank = this.Entity;
            bank.Id = 0;
            bank.Name = null;
            var content = base.GetSerializedContent(bank);
            var modelState = new ModelStateDictionary().AddModelRequiredError("Name");
            string expectedContent = base.GetModelStateContent(modelState);
            string url = this._postBankUrl;

            //Act
            var result = await base.Client.PostAsync(url, content);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode, "error in returning correct response");
            var responseContent = await base.GetDeserializedContent<string>(result);
            Assert.AreEqual(expectedContent, responseContent, "error in returning correct content");
        }

        [Test]
        public async Task PostAsync_DuplicateBank_ReturnsHasDuplicateInfoException()
        {
            // Arrange
            var existedBank = await GetLastBank();
            if (existedBank == null) // no bank exists
            {
                Assert.Pass();
                return;
            }
            var bank = existedBank;
            bank.Id = 0;
            var content = base.GetSerializedContent(bank);
            string expectedContent = Constant.Exception_sql_HasDuplicateInfo;
            string url = this._postBankUrl;

            //Act
            var result = await base.Client.PostAsync(url, content);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode, "error in returning correct response");
            var responseContent = await base.GetDeserializedContent<string>(result);
            Assert.AreEqual(expectedContent, responseContent, "error in returning correct content");
        }

        #endregion /PostAsync

        #region PutAsync

        [Test]
        public async Task PutAsync_ReturnsOK()
        {
            // Arrange
            var bank = await GetLastBank();
            if (bank == null) // no bank exists
            {
                Assert.Pass();
                return;
            }
            var content = base.GetSerializedContent(bank);
            string url = this._putBankUrl;

            //Act
            var result = await base.Client.PutAsync(url, content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "error in returning correct response");
        }

        [Test]
        public async Task PutAsync_BankNull_ReturnsBadRequestValidationException()
        {
            // Arrange
            Bank bank = null;
            var content = base.GetSerializedContent(bank);
            string url = this._putBankUrl;

            //Act
            var result = await base.Client.PutAsync(url, content);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode, "error in returning correct response");
        }

        [Test]
        public async Task PutAsync_BankNullName_ReturnsBadRequestValidationException()
        {
            // Arrange
            var bank = this.Entity;
            bank.Name = null;
            var content = base.GetSerializedContent(bank);
            var modelState = new ModelStateDictionary().AddModelRequiredError("Name");
            string expectedContent = base.GetModelStateContent(modelState);
            string url = this._putBankUrl;

            //Act
            var result = await base.Client.PutAsync(url, content);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode, "error in returning correct response");
            var responseContent = await base.GetDeserializedContent<string>(result);
            Assert.AreEqual(expectedContent, responseContent, "error in returning correct content");
        }

        #endregion /PutAsync

        #region DeleteAsync

        [Test]
        public async Task DeleteAsync_Bank_ReturnsOK()
        {
            // Arrange
            int bankId = await GetLastBankId();
            if (bankId == 0) // no bank exists
            {
                Assert.Pass();
            }
            string url = string.Format(this._deleteBankUrl, bankId);

            //Act
            var result = await base.Client.DeleteAsync(url);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "error in returning correct response");
        }


        #endregion /DeleteAsync

        #region Post_Put_Get_Delete

        [Test]
        public async Task Post_Put_Get_Delete_ReturnsOK()
        {
            // Arrange
            var bank = this.Entity;
            bank.Id = 0;
            bank.Name = Guid.NewGuid().ToString(); // make the bank name unique to prevent index error
            var expectedBank = bank;
            expectedBank.Name = Guid.NewGuid().ToString();
            var content = base.GetSerializedContent(bank);

            //Act
            var result = await base.Client.PostAsync(this._postBankUrl, content);

            // Arrange
            bank = await base.GetDeserializedContent<Bank>(result);
            expectedBank.Id = bank.Id;
            bank.Name = expectedBank.Name;
            content = base.GetSerializedContent(bank);
            string getUrl = string.Format(this._getBankByIdUrl, expectedBank.Id);
            string deleteUrl = string.Format(this._deleteBankUrl, expectedBank.Id);

            //Act
            await base.Client.PutAsync(this._putBankUrl, content);
            result = await base.Client.GetAsync(getUrl);
            bank = await base.GetDeserializedContent<Bank>(result);
            result = await base.Client.DeleteAsync(deleteUrl);

            // Assert
            bank = await base.GetDeserializedContent<Bank>(result);
            TestHelper.AreEqualEntities(bank, expectedBank, "error in returning correct bank");
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "error in returning correct response");
        }

        [Test]
        public async Task Post_Delete_Get_ReturnsOK()
        {
            // Arrange
            var bank = this.Entity;
            bank.Id = 0;
            bank.Name = Guid.NewGuid().ToString(); // make the bank name unique to prevent index error
            var content = base.GetSerializedContent(bank);
            Bank expectedBank = null;

            //Act
            var result = await base.Client.PostAsync(this._postBankUrl, content);

            // Arrange
            bank = await base.GetDeserializedContent<Bank>(result);
            int id = bank.Id;
            string deleteUrl = string.Format(this._deleteBankUrl, id);
            string getUrl = string.Format(this._getBankByIdUrl, id);

            //Act
            await base.Client.DeleteAsync(deleteUrl);
            result = await base.Client.GetAsync(getUrl);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode, "error in returning correct response");
            bank = await base.GetDeserializedContent<Bank>(result);
            TestHelper.AreEqualEntities(bank, expectedBank, "error in returning correct bank");
        }

        #endregion /Post_Put_Get_Delete

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

        private async Task<Bank> GetLastBank()
        {
            string url = this._getBanksUrl;
            var result = await base.Client.GetAsync(url);
            var banks = await base.GetDeserializedContent<IList<Bank>>(result);
            return banks.OrderByDescending(q => q.Id).FirstOrDefault();
        }

        private async Task<int> GetLastBankId()
        {
            return (await GetLastBank())?.Id ?? 0;
        }

        #endregion /Methods

    }
}
