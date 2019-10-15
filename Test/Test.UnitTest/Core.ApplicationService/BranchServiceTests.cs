using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainService.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Test.Common.Models;

namespace Test.UnitTest.Core.ApplicationService
{
    [TestFixture]
    public class BranchServiceTests : BaseServiceTests<IBranchRepository, Branch, int>
    {

        #region Properties

        protected new BranchService Service
        {
            get => base.Service as BranchService;
        }

        protected override Branch Entity
        {
            get => new BranchModel().Entity;
        }

        protected override IList<Branch> EntityList
        {
            get => new BranchModel().EntityList;
        }

        #endregion /Properties

        #region Constructors

        public BranchServiceTests()
            : base()
        {
        }

        #endregion /Constructors

        #region Methods

        [OneTimeSetUp]
        public override void Setup()
        {
            base.SetService<BranchService>();
        }

        [Test]
        public async Task GetCountByBankIdAsync_ReturnsOK()
        {
            // Arrange
            int count = 3;
            int bankId = 5;
            var expression = It.IsAny<Expression<Func<Branch, bool>>>();
            base.RepositoryMock.Setup(x => x.GetCountAsync(expression)).ReturnsAsync(count);

            //Act
            var result = await this.Service.GetCountByBankIdAsync(bankId);

            // Assert
            base.RepositoryMock.Verify(q => q.GetCountAsync(expression),
                "error in calling the correct method");  // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        [Test]
        public async Task GetCountByBankIdAsync_BankIdIsInvalid_ReturnsZero()
        {
            // Arrange
            int count = 0;
            int bankId = -1;
            var expression = It.IsAny<Expression<Func<Branch, bool>>>();
            base.RepositoryMock.Setup(x => x.GetCountAsync(expression)).ReturnsAsync(count);

            //Act
            var result = this.Service.GetCountByBankIdAsync(bankId).Result;

            // comparing expressions

            // Assert
            base.RepositoryMock.Verify(q => q.GetCountAsync(expression),
                "error in calling the correct method");  // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        public async Task GetListByBankIdAsync_ReturnsOK()
        {
            // Arrange
            var entityList = this.EntityList;
            int bankId = 5;
            var expression = It.IsAny<Expression<Func<Branch, bool>>>();
            base.RepositoryMock.Setup(q => q.GetEnumerableAsync(expression, null, null))
                .ReturnsAsync(entityList);

            //Act
            var result = await this.Service.GetListByBankIdAsync(bankId);

            // Assert
            base.RepositoryMock.Verify(q => q.GetEnumerableAsync(expression, null, null),
                "error in calling the correct method");  // Verifies that Repository.GetEnumerableAsync was called
            Assert.AreEqual(entityList, result, "error in returning correct entities");
        }

        public async Task GetListByBankIdAsync_BankIdIsInvalid_ReturnsNoItem()
        {
            // Arrange
            var entityList = new List<Branch>();
            int bankId = -1;
            var expression = It.IsAny<Expression<Func<Branch, bool>>>();
            base.RepositoryMock.Setup(q => q.GetEnumerableAsync(expression, null, null))
                .ReturnsAsync(entityList);

            //Act
            var result = await this.Service.GetListByBankIdAsync(bankId);

            // Assert
            base.RepositoryMock.Verify(q => q.GetEnumerableAsync(expression, null, null),
                "error in calling the correct method");  // Verifies that Repository.GetEnumerableAsync was called
            Assert.AreEqual(entityList, result, "error in returning correct entities");
        }

        #endregion /Methods

    }
}
