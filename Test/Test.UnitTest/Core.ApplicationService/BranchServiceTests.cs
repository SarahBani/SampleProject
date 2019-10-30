using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainService.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        public void GetCountByBankId_ReturnsOK()
        {
            // Arrange
            int count = 3;
            int bankId = 5;
            base.RepositoryMock.Setup(x => x.GetCount(It.IsAny<Expression<Func<Branch, bool>>>())).Returns(count);

            //Act
            var result = this.Service.GetCountByBankId(bankId);

            // Assert
            base.RepositoryMock.Verify(q => q.GetCount(It.IsAny<Expression<Func<Branch, bool>>>()),
                "error in calling the correct method");  // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        [Test]
        public void GetCountByBankId_BankIdIsInvalid_ReturnsZero()
        {
            // Arrange
            int count = 0;
            int bankId = -1;
            base.RepositoryMock.Setup(x => x.GetCount(It.IsAny<Expression<Func<Branch, bool>>>())).Returns(count);

            //Act
            var result = this.Service.GetCountByBankId(bankId);

            // comparing expressions

            // Assert
            base.RepositoryMock.Verify(q => q.GetCount(It.IsAny<Expression<Func<Branch, bool>>>()),
                "error in calling the correct method");  // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        public void GetListByBankId_ReturnsOK()
        {
            // Arrange
            var entityList = this.EntityList;
            int bankId = 5;
            base.RepositoryMock.Setup(q => q.GetEnumerable(It.IsAny<Expression<Func<Branch, bool>>>(), null, null))
                .Returns(entityList);

            //Act
            var result = this.Service.GetListByBankId(bankId);

            // Assert
            base.RepositoryMock.Verify(q => q.GetEnumerable(It.IsAny<Expression<Func<Branch, bool>>>(), null, null),
                "error in calling the correct method");  // Verifies that Repository.GetEnumerableAsync was called
            Assert.AreEqual(entityList, result, "error in returning correct entities");
        }

        public void GetListByBankId_BankIdIsInvalid_ReturnsNoItem()
        {
            // Arrange
            var entityList = new List<Branch>();
            int bankId = -1;
            base.RepositoryMock.Setup(q => q.GetEnumerable(It.IsAny<Expression<Func<Branch, bool>>>(), null, null))
                .Returns(entityList);

            //Act
            var result = this.Service.GetListByBankId(bankId);

            // Assert
            base.RepositoryMock.Verify(q => q.GetEnumerable(It.IsAny<Expression<Func<Branch, bool>>>(), null, null),
                "error in calling the correct method");  // Verifies that Repository.GetEnumerableAsync was called
            Assert.AreEqual(entityList, result, "error in returning correct entities");
        }

        #endregion /Methods

    }
}
