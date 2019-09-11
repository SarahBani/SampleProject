using Core.ApplicationService;
using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainServices.Repositoy;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test.UnitTest.Core.ApplicationService
{
    public abstract class BaseReadOnlyServiceTests<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        #region Properties

        protected abstract TEntity Entity { get; }

        protected abstract IList<TEntity> EntityList { get; }

        protected BaseReadOnlyService<TEntity, TKey> Service { get; set; }
        
        protected Mock<IEntityService> EntityServiceMock { get; private set; }

        protected Mock<IReadOnlyRepository<TEntity, TKey>> RepositoryMock { get; private set; }

        #endregion /Properties

        #region Consructors

        public BaseReadOnlyServiceTests()
        {
            this.EntityServiceMock = new Mock<IEntityService>();
            this.RepositoryMock = new Mock<IReadOnlyRepository<TEntity, TKey>>();
            this.EntityServiceMock.Setup(q => q.GetRepository<TEntity, TKey>()).Returns(this.RepositoryMock.Object);
        }

        #endregion /Consructors

        #region Methods

        [Test]
        public void GetByIdAsync_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            this.RepositoryMock.Setup(q => q.GetByIdAsync(It.IsAny<TKey>())).ReturnsAsync(entity);

            //Act
            var result = this.Service.GetByIdAsync(entity.Id).Result;

            // Assert
            Assert.IsInstanceOf<TEntity>(result);
            this.RepositoryMock.Verify(q => q.GetByIdAsync(It.IsAny<TKey>())); // Verifies that Repository.GetByIdAsync was called
            Assert.AreEqual(entity, result, "error in returning correct entity");
        }

        [Test]
        public void GetByIdAsync_IdIs0_ReturnsNull()
        {
            // Arrange
            TEntity entity = null;
            this.RepositoryMock.Setup(q => q.GetByIdAsync(It.IsAny<TKey>())).ReturnsAsync(entity);
            TKey id = (TKey)Convert.ChangeType(0, typeof(TKey));

            //Act
            var result = this.Service.GetByIdAsync(id).Result;

            // Assert
            this.RepositoryMock.Verify(q => q.GetByIdAsync(It.IsAny<TKey>())); // Verifies that Repository.GetByIdAsync was called
            Assert.IsNull(result, "error in returning null entity");
        }

        [Test]
        public void GetCountAsync_ReturnsOK()
        {
            // Arrange
            int count = 3;
            this.RepositoryMock.Setup(q => q.GetCountAsync(null)).ReturnsAsync(count);

            //Act
            var result = this.Service.GetCountAsync().Result;

            // Assert
            this.RepositoryMock.Verify(q => q.GetCountAsync(null)); // Verifies that Repository.GetCountAsync was called
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

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
            this.RepositoryMock.Verify(q => q.GetQueryableAsync()); // Verifies that Repository.GetQueryableAsync was called
            Assert.AreEqual(entityList, result, "error in returning correct entities");
        }

        #endregion /Methods

    }
}
