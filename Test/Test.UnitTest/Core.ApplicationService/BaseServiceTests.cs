using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainServices;
using Core.DomainServices.Repositoy;
using Moq;
using NUnit.Framework;
using System;

namespace Test.UnitTest.Core.ApplicationService
{
    public abstract class BaseServiceTests<TRepository, TEntity, TKey> : BaseReadOnlyServiceTests<TRepository, TEntity, TKey>
        where TRepository : class, IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        #region Properties

        protected new BaseService<TRepository, TEntity, TKey> Service
        {
            get => base.Service as BaseService<TRepository, TEntity, TKey>;
        }

        #endregion /Properties

        #region Consructors

        public BaseServiceTests()
        {
            SetEntityServiceMock();
        }

        #endregion /Consructors

        #region Methods

        private void SetEntityServiceMock()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(q => q.BeginTransaction(It.IsAny<string>()))
                .Callback<string>(q => unitOfWorkMock.Setup(x => x.GetTransactionName()).Returns(q));
            base.EntityServiceMock.Setup(q => q.UnitOfWork).Returns(unitOfWorkMock.Object);
        }

        [Test]
        public void InsertAsync_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            base.RepositoryMock.Setup(q => q.InsertAsync(It.IsAny<TEntity>())).Verifiable();

            //Act
            var result = this.Service.InsertAsync(entity).Result;

            // Assert
            Assert.IsInstanceOf<TransactionResult>(result);
            base.RepositoryMock.Verify(q => q.InsertAsync(It.IsAny<TEntity>()),
                "error in calling the correct method");  // Verifies that Repository.InsertAsync was called
            Assert.AreEqual(true, result.IsSuccessful, "error in returning correct TransactionResult");
        }

        [Test]
        public void UpdateAsync_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            base.RepositoryMock.Setup(q => q.Update(It.IsAny<TEntity>())).Verifiable();

            //Act
            var result = this.Service.UpdateAsync(entity).Result;

            // Assert
            Assert.IsInstanceOf<TransactionResult>(result);
            base.RepositoryMock.Verify(q => q.Update(It.IsAny<TEntity>()),
                "error in calling the correct method");  // Verifies that Repository.UpdateAsync was called
            Assert.AreEqual(true, result.IsSuccessful, "error in returning correct TransactionResult");
        }

        [Test]
        public void DeleteAsync_ById_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            base.RepositoryMock.Setup(q => q.Delete(entity.Id)).Verifiable();

            //Act
            var result = this.Service.DeleteAsync(entity.Id).Result;

            // Assert
            Assert.IsInstanceOf<TransactionResult>(result);
            base.RepositoryMock.Verify(q => q.Delete(entity.Id),
                "error in calling the correct method");  // Verifies that Repository.DeleteAsync was called
            Assert.AreEqual(true, result.IsSuccessful, "error in returning correct TransactionResult");
        }

        [Test]
        public void DeleteAsync_ByEntity_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            base.RepositoryMock.Setup(q => q.Delete(It.IsAny<TEntity>())).Verifiable();

            //Act
            var result = this.Service.DeleteAsync(entity).Result;

            // Assert
            Assert.IsInstanceOf<TransactionResult>(result);
            base.RepositoryMock.Verify(q => q.Delete(It.IsAny<TEntity>()), 
                "error in calling the correct method");  // Verifies that Repository.DeleteAsync was called
            Assert.AreEqual(true, result.IsSuccessful, "error in returning correct TransactionResult");
        }

        #endregion /Methods

    }
}
