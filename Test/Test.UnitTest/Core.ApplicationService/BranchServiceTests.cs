//using Core.ApplicationService.Implementation;
//using Core.DomainModel.Entities;
//using Core.DomainServices.Repositoy;
//using NUnit.Framework;
//using System.Collections.Generic;

//namespace Test.UnitTest.Core.ApplicationService
//{
//    [TestFixture]
//    public class BranchServiceTests : BaseServiceTests<IBranchRepository, Branch, int>
//    {

//        #region Properties

//        protected override Branch Entity
//        {
//            get => new Branch()
//            {
//                Id = 3,
//                BankId = 5,
//                Name = "sdgsg"

//            this.BankId = bankId;
//            this.Code = code;
//            this.Name = name;
//            this.Address = address;
//            };
//        }

//        protected override IList<Branch> EntityList
//        {
//            get => new List<Branch>
//                {
//                    new Branch()
//                    {
//                        Id = 3,
//                        Name = "sdgsg"
//                    },
//                    new Branch()
//                    {
//                        Id = 4,
//                        Name = "سیبسیب"
//                    }
//                };
//        }

//        #endregion /Properties

//        #region Constructors

//        public BranchServiceTests()
//            : base()
//        {
//        }

//        #endregion /Constructors

//        #region Methods

//        [SetUp]
//        public override void Setup()
//        {
//            base.SetService<BranchService>();
//        }

//        //[Test]
//        //public void UpdateAsync_ReturnsOK()
//        //{
//        //    // Arrange
//        //    var entity = Entity;

//        //    //Act
//        //    var result = this.Service.UpdateAsync(entity).Result;

//        //    // Assert
//        //    Assert.IsInstanceOf<TransactionResult>(result);
//        //    base.RepositoryMock.Verify(q => q.Update(It.IsAny<Branch>())); // Verifies that Repository.UpdateAsync was called
//        //    Assert.AreEqual(this.SuccessfulTransResult, result, "error in returning correct TransactionResult");
//        //}

//        #endregion /Methods

//    }
//}
