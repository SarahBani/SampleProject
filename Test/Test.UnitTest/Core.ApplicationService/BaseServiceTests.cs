using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainService;
using Core.DomainService.Repository;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

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
        public async Task InsertAsync_ReturnsOK()
        {
            // Arrange
            var entity = this.Entity;
            base.RepositoryMock.Setup(q => q.InsertAsync(It.IsAny<TEntity>())).Verifiable();

            //Act
            var result = await this.Service.InsertAsync(entity);

            // Assert
            Assert.IsInstanceOf<TransactionResult>(result);
            base.RepositoryMock.Verify(q => q.InsertAsync(It.IsAny<TEntity>()),
                "error in calling the correct method");  // Verifies that Repository.InsertAsync was called
            Assert.AreEqual(true, result.IsSuccessful, "error in returning correct TransactionResult");
        }

        [Test]
        public async Task UpdateAsync_ReturnsOK()
        {
            // Arrange
            var entity = this.Entity;
            base.RepositoryMock.Setup(q => q.Update(It.IsAny<TEntity>())).Verifiable();

            //Act
            var result = await this.Service.UpdateAsync(entity);

            // Assert
            Assert.IsInstanceOf<TransactionResult>(result);
            base.RepositoryMock.Verify(q => q.Update(It.IsAny<TEntity>()),
                "error in calling the correct method");  // Verifies that Repository.UpdateAsync was called
            Assert.AreEqual(true, result.IsSuccessful, "error in returning correct TransactionResult");
        }

        [Test]
        public async Task DeleteAsync_ById_ReturnsOK()
        {
            // Arrange
            var entity = this.Entity;
            base.RepositoryMock.Setup(q => q.Delete(entity.Id)).Verifiable();

            //Act
            var result = await this.Service.DeleteAsync(entity.Id);

            // Assert
            Assert.IsInstanceOf<TransactionResult>(result);
            base.RepositoryMock.Verify(q => q.Delete(entity.Id),
                "error in calling the correct method");  // Verifies that Repository.DeleteAsync was called
            Assert.AreEqual(true, result.IsSuccessful, "error in returning correct TransactionResult");
        }

        [Test]
        public async Task DeleteAsync_ByEntity_ReturnsOK()
        {
            // Arrange
            var entity = this.Entity;
            base.RepositoryMock.Setup(q => q.Delete(It.IsAny<TEntity>())).Verifiable();

            //Act
            var result = await this.Service.DeleteAsync(entity);

            // Assert
            Assert.IsInstanceOf<TransactionResult>(result);
            base.RepositoryMock.Verify(q => q.Delete(It.IsAny<TEntity>()),
                "error in calling the correct method");  // Verifies that Repository.DeleteAsync was called
            Assert.AreEqual(true, result.IsSuccessful, "error in returning correct TransactionResult");
        }

        #endregion /Methods

    }
}
