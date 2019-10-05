using Core.ApplicationService.Implementation;
using Core.DomainModel;
using Core.DomainModel.Entities;
using Core.DomainService;
using Core.DomainService.Repositoy;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Test.UnitTest.Common;
using Test.UnitTest.Common.Models;
using Test.UnitTest.Infrastructure.Common;

namespace Test.UnitTest.Core.ApplicationService
{
    [TestFixture]
    public class BankServiceTests : BaseServiceTests<IBankRepository, Bank, int>
    {

        #region Properties

        protected new BankService Service
        {
            get => base.Service as BankService;
        }

        protected override Bank Entity
        {
            get => new BankModel().Entity;
        }

        protected override IList<Bank> EntityList
        {
            get => new BankModel().EntityList;
        }

        #endregion /Properties

        #region Constructors

        public BankServiceTests()
            : base()
        {
        }

        #endregion /Constructors

        #region Methods

        [SetUp]
        public override void Setup()
        {
            base.SetService<BankService>();
        }

        [Test]
        public void InsertAsync_DuplicateName_ReturnsDuplicateException()
        {
            // Arrange
            var entity = Entity;
            var exceptionKey = ExceptionKey.RecordAlreadyExsits;
            var expectedResult = new TransactionResult(new CustomException(exceptionKey));
            base.RepositoryMock.Setup(q => q.InsertAsync(It.IsAny<Bank>()))
                .Throws(SqlExceptionGenerator.CreateSqlException((int)exceptionKey));

            //Act
            var result = this.Service.InsertAsync(entity).Result;

            // Assert
            Assert.IsInstanceOf<TransactionResult>(result);
            base.RepositoryMock.Verify(q => q.InsertAsync(It.IsAny<Bank>()),
                "error in calling the correct method");  // Verifies that Repository.InsertAsync was called
            AssertHelper.AreEqualEntities(expectedResult, result, "error in raising the correct exception");
        }

        #endregion /Methods

    }
}
