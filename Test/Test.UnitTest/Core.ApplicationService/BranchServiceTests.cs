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
            base.RepositoryMock.Setup(x => x.GetCountAsync(It.IsAny<Expression<Func<Branch, bool>>>())).ReturnsAsync(count);

            //Act
            var result = await this.Service.GetCountByBankIdAsync(bankId);

            // Assert
            base.RepositoryMock.Verify(q => q.GetCountAsync(It.IsAny<Expression<Func<Branch, bool>>>()), 
                "error in calling the correct method");  // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        [Test]
        public async Task GetCountByBankIdAsync_BankIdIsInvalid_ReturnsZero()
        {
            // Arrange
            int count = 0;
            int bankId = -1;
            base.RepositoryMock.Setup(x => x.GetCountAsync(It.IsAny<Expression<Func<Branch, bool>>>())).ReturnsAsync(count);

            //Act
            var result = await this.Service.GetCountByBankIdAsync(bankId);

            // comparing expressions

            // Assert
            base.RepositoryMock.Verify(q => q.GetCountAsync(It.IsAny<Expression<Func<Branch, bool>>>()),
                "error in calling the correct method");  // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        public async Task GetListByBankIdAsync_ReturnsOK()
        {
            // Arrange
            var entityList = this.EntityList;
            int bankId = 5;
            base.RepositoryMock.Setup(q => q.GetEnumerableAsync(It.IsAny<Expression<Func<Branch, bool>>>(), null, null))
                .ReturnsAsync(entityList);

            //Act
            var result = await this.Service.GetListByBankIdAsync(bankId);

            // Assert
            base.RepositoryMock.Verify(q => q.GetEnumerableAsync(It.IsAny<Expression<Func<Branch, bool>>>(), null, null),
                "error in calling the correct method");  // Verifies that Repository.GetEnumerableAsync was called
            Assert.AreEqual(entityList, result, "error in returning correct entities");
        }

        public async Task GetListByBankIdAsync_BankIdIsInvalid_ReturnsNoItem()
        {
            // Arrange
            var entityList = new List<Branch>();
            int bankId = -1;
            base.RepositoryMock.Setup(q => q.GetEnumerableAsync(It.IsAny<Expression<Func<Branch, bool>>>(), null, null))
                .ReturnsAsync(entityList);

            //Act
            var result = await this.Service.GetListByBankIdAsync(bankId);

            // Assert
            base.RepositoryMock.Verify(q => q.GetEnumerableAsync(It.IsAny<Expression<Func<Branch, bool>>>(), null, null),
                "error in calling the correct method");  // Verifies that Repository.GetEnumerableAsync was called
            Assert.AreEqual(entityList, result, "error in returning correct entities");
        }

        #endregion /Methods

    }
}
