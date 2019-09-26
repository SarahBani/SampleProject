using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainServices.Repositoy;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
            get
            {
                var country = new Country()
                {
                    Id = 5,
                    Name = "ffffff"
                };
                var address = new Address(country, "Rasht", "fhfh fghjf dgh", "17", "1998737863");
                return new Branch()
                {
                    Id = 3,
                    BankId = 5,
                    Code = 50,
                    Name = "sdgsg",
                    Address = address
                };
            }
        }

        protected override IList<Branch> EntityList
        {
            get => new List<Branch>
                {
                    new Branch()
                    {
                        Id = 3,
                        Name = "sdgsg"
                    },
                    new Branch()
                    {
                        Id = 4,
                        Name = "hjgj"
                    }
                };
        }

        #endregion /Properties

        #region Constructors

        public BranchServiceTests()
            : base()
        {
            //base.GetService<BaseService>();
        }

        #endregion /Constructors

        #region Methods

        [SetUp]
        public override void Setup()
        {
            base.SetService<BranchService>();
        }

        [Test]
        public void GetCountByBankIdAsync_ReturnsOK()
        {
            // Arrange
            int count = 3;
            int bankId = 5;
            var expression = It.IsAny<Expression<Func<Branch, bool>>>();
            base.RepositoryMock.Setup(x => x.GetCountAsync(expression)).ReturnsAsync(count);

            //Act
            var result = this.Service.GetCountByBankIdAsync(bankId).Result;

            // Assert
            base.RepositoryMock.Verify(q => q.GetCountAsync(expression),
                "error in calling the correct method");  // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        [Test]
        public void GetCountByBankIdAsync_BankIdIsInvalid_ReturnsZero()
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

        public void GetListByBankIdAsync_ReturnsOK()
        {
            // Arrange
            var entityList = EntityList;
            int bankId = 5;
            var expression = It.IsAny<Expression<Func<Branch, bool>>>();
            base.RepositoryMock.Setup(q => q.GetEnumerableAsync(expression, null, null))
                .ReturnsAsync(entityList);

            //Act
            var result = this.Service.GetListByBankIdAsync(bankId).Result;

            // Assert
            base.RepositoryMock.Verify(q => q.GetEnumerableAsync(expression, null, null), 
                "error in calling the correct method");  // Verifies that Repository.GetEnumerableAsync was called
            Assert.AreEqual(entityList, result, "error in returning correct entities");
        }

        public void GetListByBankIdAsync_BankIdIsInvalid_ReturnsNoItem()
        {
            // Arrange
            var entityList = new List<Branch>();
            int bankId = -1;
            var expression = It.IsAny<Expression<Func<Branch, bool>>>();
            base.RepositoryMock.Setup(q => q.GetEnumerableAsync(expression, null, null))
                .ReturnsAsync(entityList);

            //Act
            var result = this.Service.GetListByBankIdAsync(bankId).Result;

            // Assert
            base.RepositoryMock.Verify(q => q.GetEnumerableAsync(expression, null, null),
                "error in calling the correct method");  // Verifies that Repository.GetEnumerableAsync was called
            Assert.AreEqual(entityList, result, "error in returning correct entities");
        }

        #endregion /Methods

    }
}
