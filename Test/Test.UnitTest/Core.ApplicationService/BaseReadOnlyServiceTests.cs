using Core.ApplicationService;
using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainService.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Common;

namespace Test.UnitTest.Core.ApplicationService
{
    public abstract class BaseReadOnlyServiceTests<TRepository, TEntity, TKey>
        where TRepository : class, IReadOnlyRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        #region Properties

        protected Mock<IEntityService> EntityServiceMock { get; private set; }

        protected Mock<TRepository> RepositoryMock { get; private set; }

        protected BaseReadOnlyService<TRepository, TEntity, TKey> Service { get; set; }

        protected abstract TEntity Entity { get; }

        protected abstract IList<TEntity> EntityList { get; }

        #endregion /Properties

        #region Consructors

        public BaseReadOnlyServiceTests()
        {
            this.RepositoryMock = new Mock<TRepository>();
            SetEntityServiceMock();
        }

        #endregion /Consructors

        #region Methods    

        private void SetEntityServiceMock()
        {
            this.EntityServiceMock = new Mock<IEntityService>();
            this.EntityServiceMock.Setup(q => q.GetRepository<TEntity, TKey>()).Returns(this.RepositoryMock.Object);
        }

        [OneTimeSetUp]
        public abstract void Setup();

        protected void SetService<T>() where T : BaseReadOnlyService<TRepository, TEntity, TKey>
        {
            this.Service = (T)Activator.CreateInstance(typeof(T), this.EntityServiceMock.Object);
        }

        #region GetByIdAsync

        [Test]
        public async Task GetByIdAsync_ReturnsOK()
        {
            // Arrange
            var entity = this.Entity;
            this.RepositoryMock.Setup(q => q.GetByIdAsync(entity.Id, default)).ReturnsAsync(entity);

            //Act
            var result = await this.Service.GetByIdAsync(entity.Id);

            // Assert
            Assert.IsInstanceOf<TEntity>(result);
            this.RepositoryMock.Verify(q => q.GetByIdAsync(It.IsAny<TKey>(), default),
                "error in calling the correct method");  // Verifies that Repository.GetByIdAsync was called
            Assert.AreEqual(entity, result, "error in returning correct entity");
        }

        [Test]
        public async Task GetByIdAsync_IdIs0_ReturnsNull()
        {
            // Arrange
            TEntity entity = null;
            TKey id = TestHelper.GetId<TKey>(0);
            this.RepositoryMock.Setup(q => q.GetByIdAsync(id, default)).ReturnsAsync(entity);

            //Act
            var result = await this.Service.GetByIdAsync(id);

            // Assert
            this.RepositoryMock.Verify(q => q.GetByIdAsync(id, default),
                "error in calling the correct method");  // Verifies that Repository.GetByIdAsync was called
            Assert.IsNull(result, "error in returning null entity");
        }

        #endregion /GetByIdAsync

        #region GetCount

        [Test]
        public void GetCount_ReturnsOK()
        {
            // Arrange
            int count = 3;
            this.RepositoryMock.Setup(q => q.GetCount(null)).Returns(count);

            //Act
            var result = this.Service.GetCount();

            // Assert
            this.RepositoryMock.Verify(q => q.GetCount(null),
                "error in calling the correct method");  // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        #endregion /GetCount

        #region GetAll

        [Test]
        public void GetAll_ReturnsOK()
        {
            // Arrange
            var entityList = this.EntityList;
            this.RepositoryMock.Setup(q => q.GetQueryable()).Returns(entityList.AsQueryable());

            //Act
            var result = this.Service.GetAll();

            // Assert            
            Assert.IsInstanceOf<IList<TEntity>>(result);
            this.RepositoryMock.Verify(q => q.GetQueryable(),
                "error in calling the correct method");  // Verifies that Repository.GetQueryableAsync was called
            Assert.AreEqual(entityList, result, "error in returning correct entities");
        }

        #endregion /GetAll

        #endregion /Methods

    }
}
