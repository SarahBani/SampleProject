using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Test.UnitTest.Core.ApplicationService
{
    [TestFixture]
    public class BankServiceTests : BaseServiceTests<Bank, int>
    {

        #region Properties

        protected override Bank Entity
        {
            get => new Bank()
            {
                Id = 3,
                Name = "sdgsg"
            };
        }

        protected override IList<Bank> EntityList
        {
            get => new List<Bank>
                {
                    new Bank()
                    {
                        Id = 3,
                        Name = "sdgsg"
                    },
                    new Bank()
                    {
                        Id = 4,
                        Name = "سیبسیب"
                    }
                };
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
        public void Setup()
        {
            base.Service = new BankService(base.EntityServiceMock.Object);
        }

        //[Test]
        //public void UpdateAsync_ReturnsOK()
        //{
        //    // Arrange
        //    var entity = Entity;

        //    //Act
        //    var result = this.Service.UpdateAsync(entity).Result;

        //    // Assert
        //    Assert.IsInstanceOf<TransactionResult>(result);
        //    base.RepositoryMock.Verify(q => q.Update(It.IsAny<Bank>())); // Verifies that Repository.UpdateAsync was called
        //    Assert.AreEqual(this.SuccessfulTransResult, result, "error in returning correct TransactionResult");
        //}

        #endregion /Methods

    }
}
