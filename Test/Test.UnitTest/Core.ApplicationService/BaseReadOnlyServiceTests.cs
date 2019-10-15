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
            this.RepositoryMock.Setup(q => q.GetByIdAsync(entity.Id)).ReturnsAsync(entity);

            //Act
            var result =await this.Service.GetByIdAsync(entity.Id);

            // Assert
            Assert.IsInstanceOf<TEntity>(result);
            this.RepositoryMock.Verify(q => q.GetByIdAsync(It.IsAny<TKey>()),
                "error in calling the correct method");  // Verifies that Repository.GetByIdAsync was called
            Assert.AreEqual(entity, result, "error in returning correct entity");
        }

        [Test]
        public async Task GetByIdAsync_IdIs0_ReturnsNull()
        {
            // Arrange
            TEntity entity = null;
            TKey id = TestHelper.GetId<TKey>(0);
            this.RepositoryMock.Setup(q => q.GetByIdAsync(id)).ReturnsAsync(entity);

            //Act
            var result =await this.Service.GetByIdAsync(id);

            // Assert
            this.RepositoryMock.Verify(q => q.GetByIdAsync(id),
                "error in calling the correct method");  // Verifies that Repository.GetByIdAsync was called
            Assert.IsNull(result, "error in returning null entity");
        }

        #endregion /GetByIdAsync

        #region GetCountAsync

        [Test]
        public async Task GetCountAsync_ReturnsOK()
        {
            // Arrange
            int count = 3;
            this.RepositoryMock.Setup(q => q.GetCountAsync(null)).ReturnsAsync(count);

            //Act
            var result =await this.Service.GetCountAsync();

            // Assert
            this.RepositoryMock.Verify(q => q.GetCountAsync(null),
                "error in calling the correct method");  // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        #endregion /GetCountAsync

        #region GetAllAsync

        [Test]
        public async Task GetAllAsync_ReturnsOK()
        {
            // Arrange
            var entityList = this.EntityList;
            this.RepositoryMock.Setup(q => q.GetQueryableAsync()).ReturnsAsync(entityList.AsQueryable());

            //Act
            var result =await this.Service.GetAllAsync();

            // Assert            
            Assert.IsInstanceOf<IList<TEntity>>(result);
            this.RepositoryMock.Verify(q => q.GetQueryableAsync(),
                "error in calling the correct method");  // Verifies that Repository.GetQueryableAsync was called
            Assert.AreEqual(entityList, result, "error in returning correct entities");
        }

        #endregion /GetAllAsync

        #endregion /Methods

    }
}
