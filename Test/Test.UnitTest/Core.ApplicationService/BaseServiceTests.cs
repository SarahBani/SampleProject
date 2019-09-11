using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainServices;
using Core.DomainServices.Repositoy;
using Moq;
using NUnit.Framework;

namespace Test.UnitTest.Core.ApplicationService
{
    public abstract class BaseServiceTests<TEntity, TKey> : BaseReadOnlyServiceTests<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        #region Properties

        protected new BaseService<TEntity, TKey> Service { get; set; }

        protected new Mock<IRepository<TEntity, TKey>> RepositoryMock { get; private set; }

        protected TransactionResult SuccessfulTransResult { get; private set; }

        #endregion /Properties

        #region Consructors

        public BaseServiceTests()
        {
            this.RepositoryMock = new Mock<IRepository<TEntity, TKey>>();
            this.SuccessfulTransResult = new TransactionResult();
        }

        #endregion /Consructors

        #region Methods

        [Test]
        public void InsertAsync_ReturnsOK()
        {
            // Arrange
            var entity = Entity;

            //Act
            var result = this.Service.InsertAsync(entity).Result;

            // Assert
            Assert.IsInstanceOf<TransactionResult>(result);
            this.RepositoryMock.Verify(q => q.InsertAsync(It.IsAny<TEntity>())); // Verifies that Repository.InsertAsync was called
            Assert.AreEqual(this.SuccessfulTransResult, result, "error in returning correct TransactionResult");
        }

        [Test]
        public void UpdateAsync_ReturnsOK()
        {
            // Arrange
            var entity = Entity;

            //Act
            var result = this.Service.UpdateAsync(entity).Result;

            // Assert
            Assert.IsInstanceOf<TransactionResult>(result);
            this.RepositoryMock.Verify(q => q.Update(It.IsAny<TEntity>())); // Verifies that Repository.UpdateAsync was called
            Assert.AreEqual(this.SuccessfulTransResult, result, "error in returning correct TransactionResult");
        }

        [Test]
        public void DeleteAsync_ById_ReturnsOK()
        {
            // Arrange
            var entity = Entity;

            //Act
            var result = this.Service.DeleteAsync(entity.Id).Result;

            // Assert
            Assert.IsInstanceOf<TransactionResult>(result);
            this.RepositoryMock.Verify(q => q.Delete(It.IsAny<TKey>())); // Verifies that Repository.DeleteAsync was called
            Assert.AreEqual(this.SuccessfulTransResult, result, "error in returning correct TransactionResult");
        }

        //[Test]
        //public void DeleteAsync_ByEntity_ReturnsOK ()
        //{
        //    // Arrange
        //    var entity = Entity;

        //    //Act
        //    var result = this.Service.DeleteAsync(entity).Result;

        //    // Assert
        //    Assert.IsInstanceOf<TransactionResult>(result);
        //    this._repositoryMock.Verify(q => q.DeleteAsync(It.IsAny<TEntity>())); // Verifies that Repository.DeleteAsync was called
        //    Assert.AreEqual(this.SuccessfulTransResult, result, "error in returning correct TransactionResult");
        //}

        #endregion /Methods

    }
}
