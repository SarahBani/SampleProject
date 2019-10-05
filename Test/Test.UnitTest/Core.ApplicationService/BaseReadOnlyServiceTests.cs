using Core.ApplicationService;
using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainService.Repositoy;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [SetUp]
        public abstract void Setup();

        protected void SetService<T>() where T : BaseReadOnlyService<TRepository, TEntity, TKey>
        {
            this.Service = (T)Activator.CreateInstance(typeof(T), this.EntityServiceMock.Object);
        }

        #region GetByIdAsync

        [Test]
        public void GetByIdAsync_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            this.RepositoryMock.Setup(q => q.GetByIdAsync(entity.Id)).ReturnsAsync(entity);

            //Act
            var result = this.Service.GetByIdAsync(entity.Id).Result;

            // Assert
            Assert.IsInstanceOf<TEntity>(result);
            this.RepositoryMock.Verify(q => q.GetByIdAsync(It.IsAny<TKey>()),
                "error in calling the correct method");  // Verifies that Repository.GetByIdAsync was called
            Assert.AreEqual(entity, result, "error in returning correct entity");
        }

        [Test]
        public void GetByIdAsync_IdIs0_ReturnsNull()
        {
            // Arrange
            TEntity entity = null;
            TKey id = (TKey)Convert.ChangeType(0, typeof(TKey));
            this.RepositoryMock.Setup(q => q.GetByIdAsync(id)).ReturnsAsync(entity);

            //Act
            var result = this.Service.GetByIdAsync(id).Result;

            // Assert
            this.RepositoryMock.Verify(q => q.GetByIdAsync(id),
                "error in calling the correct method");  // Verifies that Repository.GetByIdAsync was called
            Assert.IsNull(result, "error in returning null entity");
        }

        #endregion /GetByIdAsync

        #region GetCountAsync

        [Test]
        public void GetCountAsync_ReturnsOK()
        {
            // Arrange
            int count = 3;
            this.RepositoryMock.Setup(q => q.GetCountAsync(null)).ReturnsAsync(count);

            //Act
            var result = this.Service.GetCountAsync().Result;

            // Assert
            this.RepositoryMock.Verify(q => q.GetCountAsync(null),
                "error in calling the correct method");  // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        #endregion /GetCountAsync

        #region GetAllAsync

        [Test]
        public void GetAllAsync_ReturnsOK()
        {
            // Arrange
            var entityList = EntityList;
            this.RepositoryMock.Setup(q => q.GetQueryableAsync()).ReturnsAsync(entityList.AsQueryable());

            //Act
            var result = this.Service.GetAllAsync().Result;

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
