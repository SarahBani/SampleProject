using Core.ApplicationService.Contracts;
using Core.DomainModel;
using Core.DomainModel.Entities;
using Core.DomainService;
using MicroService.CRUDService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Common;
using Test.Common.Models;
using Test.UnitTest;

namespace Test.MicroService.CRUDService
{
    [TestFixture]
    public class BankControllerTests
    {

        #region Properties

        private Mock<IBankService> _bankServiceMock;

        private BankController _controller;

        protected Bank Entity
        {
            get => new BankModel().Entity;
        }

        protected IList<Bank> EntityList
        {
            get => new BankModel().EntityList;
        }

        #endregion /Properties

        #region Constructors

        public BankControllerTests()
        {

        }

        #endregion /Constructors

        #region Methods

        [OneTimeSetUp]
        public void Setup()
        {
            this._bankServiceMock = new Mock<IBankService>();
            this._controller = new BankController(this._bankServiceMock.Object);
        }

        #region GetAsync

        [Test]
        public async Task GetAsync_ReturnsOK()
        {
            // Arrange
            var banks = this.EntityList;
            this._bankServiceMock.Setup(q => q.GetAllAsync()).ReturnsAsync(banks);
            var expectedValue = new OkObjectResult(banks);

            //Act
            var result = await this._controller.GetAsync();

            // Assert
            this._bankServiceMock.Verify(q => q.GetAllAsync(),
                "error in calling the correct method");  // Verifies that bankService.GetAllAsync was called
            Assert.IsInstanceOf<OkObjectResult>(result, "error in returning correct response");
            TestHelper.AreEqualEntities(expectedValue, result, "error in returning correct entities");
        }

        [Test]
        public async Task GetAsync_ById_ReturnsOK()
        {
            // Arrange
            var bank = this.Entity;
            int id = bank.Id;
            this._bankServiceMock.Setup(q => q.GetByIdAsync(id)).ReturnsAsync(bank);
            var expectedValue = new OkObjectResult(bank);

            //Act
            var result = await this._controller.GetAsync(id);

            // Assert
            this._bankServiceMock.Verify(q => q.GetByIdAsync(id),
                "error in calling the correct method");  // Verifies that bankService.GetByIdAsync was called
            Assert.IsInstanceOf<OkObjectResult>(result, "error in returning correct response");
            TestHelper.AreEqualEntities(expectedValue, result, "error in returning correct entity");
        }

        [Test]
        public async Task GetAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            int id = 0;
            Bank bank = null;
            this._bankServiceMock.Setup(q => q.GetByIdAsync(id)).ReturnsAsync(bank);
            var expectedValue = new OkObjectResult(bank);

            //Act
            var result = await this._controller.GetAsync(id);

            // Assert
            this._bankServiceMock.Verify(q => q.GetByIdAsync(id),
                "error in calling the correct method");  // Verifies that bankService.GetByIdAsync was called
            Assert.IsInstanceOf<OkObjectResult>(result, "error in returning correct response");
            TestHelper.AreEqualEntities(expectedValue, result, "error in returning correct entity");
        }

        #endregion /GetAsync

        #region PostAsync

        [Test]
        public async Task PostAsync_NullBank_ReturnsNoContentResult()
        {
            // Arrange
            Bank bank = null;
            var expectedValue = new NoContentResult();

            //Act
            var result = await this._controller.PostAsync(bank);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result, "error in returning correct response");
            TestHelper.AreEqualEntities(expectedValue, result, "error in returning correct response");
        }

        [Test]
        public async Task PostAsync_BankNullName_ReturnsBadRequestValidationException()
        {
            // Arrange
            var bank = this.Entity;
            bank.Name = null;
            var error = string.Format(Constant.Validation_RequiredField, "Name");
            var expectedValue = new BadRequestObjectResult(error);
            this._controller.ModelState.AddModelError("Name", error);

            //Act
            var result = await this._controller.PostAsync(bank);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result, "error in returning correct response");
            TestHelper.AreEqualEntities(expectedValue, result, "error in returning correct exception");
        }
               
        [Test]
        public async Task PostAsync_BankDuplicateName_ReturnsBadRequestExceptionHasDuplicateInfo()
        {
            // Arrange
            var bank = this.Entity;
            var transactionResult = new TransactionResult(new CustomException(Constant.Exception_sql_HasDuplicateInfo));
            this._bankServiceMock.Setup(q => q.InsertAsync(bank)).ReturnsAsync(transactionResult);
            var expectedValue = new BadRequestObjectResult(transactionResult.ExceptionContentResult);

            //Act
            var result = await this._controller.PostAsync(bank);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result, "error in returning correct response");
            TestHelper.AreEqualEntities(expectedValue, result, "error in returning correct exception");
        }

        [Test]
        public async Task PostAsync_ReturnsOK()
        {
            // Arrange
            var bank = this.Entity;
            var transactionResult = new TransactionResult();
            this._bankServiceMock.Setup(q => q.InsertAsync(bank)).ReturnsAsync(transactionResult);

            //Act
            var result = await this._controller.PostAsync(bank);

            // Assert
            this._bankServiceMock.Verify(q => q.InsertAsync(bank),
                "error in calling the correct method");  // Verifies that bankService.InsertAsync was called
            Assert.IsInstanceOf<CreatedAtActionResult>(result, "error in returning correct response");
            var responseContent = (result as CreatedAtActionResult).Value;
            Assert.IsInstanceOf<Bank>(responseContent, "error in returning correct content");
            TestHelper.AreEqualEntities(bank, responseContent, "error in returning correct entities");
        }

        #endregion /PostAsync

        #region PutAsync

        [Test]
        public async Task PutAsync_NullBank_ReturnsNoContentResult()
        {
            // Arrange
            Bank bank = null;
            var expectedValue = new NoContentResult();

            //Act
            var result = await this._controller.PutAsync(bank);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result, "error in returning correct response");
            TestHelper.AreEqualEntities(expectedValue, result, "error in returning correct response");
        }

        [Test]
        public async Task PutAsync_BankNullName_ReturnsBadRequestValidationException()
        {
            // Arrange
            var bank = this.Entity;
            bank.Name = null;
            var error = string.Format(Constant.Validation_RequiredField, "Name");
            var expectedValue = new BadRequestObjectResult(error);
            this._controller.ModelState.AddModelError("Name", error);

            //Act
            var result = await this._controller.PutAsync(bank);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result, "error in returning correct response");
            TestHelper.AreEqualEntities(expectedValue, result, "error in returning correct exception");
        }

        [Test]
        public async Task PutAsync_BankDuplicateName_ReturnsBadRequestExceptionHasDuplicateInfo()
        {
            // Arrange
            var bank = this.Entity;
            var transactionResult = new TransactionResult(new CustomException(Constant.Exception_sql_HasDuplicateInfo));
            this._bankServiceMock.Setup(q => q.UpdateAsync(bank)).ReturnsAsync(transactionResult);
            var expectedValue = new BadRequestObjectResult(transactionResult.ExceptionContentResult);

            //Act
            var result = await this._controller.PutAsync(bank);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result, "error in returning correct response");
            TestHelper.AreEqualEntities(expectedValue, result, "error in returning correct exception");
        }

        [Test]
        public async Task PutAsync_ReturnsOK()
        {
            // Arrange
            var bank = this.Entity;
            var transactionResult = new TransactionResult();
            this._bankServiceMock.Setup(q => q.UpdateAsync(bank)).ReturnsAsync(transactionResult);

            //Act
            var result = await this._controller.PutAsync(bank);

            // Assert
            this._bankServiceMock.Verify(q => q.UpdateAsync(bank),
                "error in calling the correct method");  // Verifies that bankService.UpdateAsync was called
            Assert.IsInstanceOf<OkResult>(result, "error in returning correct content");
        }

        #endregion /PutAsync

        #region DeleteAsync
               
        [Test]
        public async Task DeleteAsync_ReturnsOK()
        {
            // Arrange
            int id = this.Entity.Id;
            var transactionResult = new TransactionResult();
            this._bankServiceMock.Setup(q => q.DeleteAsync(id)).ReturnsAsync(transactionResult);

            //Act
            var result = await this._controller.DeleteAsync(id);

            // Assert
            this._bankServiceMock.Verify(q => q.DeleteAsync(id),
                "error in calling the correct method");  // Verifies that bankService.DeleteAsync was called
            Assert.IsInstanceOf<OkResult>(result, "error in returning correct response");
        }

        #endregion /DeleteAsync

        #endregion /Methods

    }
}
