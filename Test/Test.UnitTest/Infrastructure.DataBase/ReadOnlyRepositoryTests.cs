using Core.DomainModel.Entities;
using Core.DomainServices;
using Infrastructure.DataBase.Repositoy;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Test.UnitTest.Infrastructure.Common;

namespace Test.UnitTest.Infrastructure.DataBase
{
    public abstract class ReadOnlyRepositoryTests<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        #region Properties

        protected Mock<SampleDataBaseContext> DataBaseContextMock { get; private set; }

        protected ReadOnlyRepository<TEntity, TKey> Repository { get; private set; }

        protected abstract TEntity Entity { get; }

        protected abstract IList<TEntity> EntityList { get; }

        #endregion /Properties

        #region Consructors

        public ReadOnlyRepositoryTests()
        {
            SetDataBaseContextMock();
        }

        #endregion /Consructors

        #region Methods  

        [SetUp]
        public abstract void Setup();

        private void SetDataBaseContextMock()
        {
            var options = new DbContextOptions<SampleDataBaseContext>();
            this.DataBaseContextMock = new Mock<SampleDataBaseContext>(options);
        }

        protected void SetRepository<T>() where T : ReadOnlyRepository<TEntity, TKey>
        {
            this.Repository = (T)Activator.CreateInstance(typeof(T), this.DataBaseContextMock.Object);
        }

        protected Mock<DbSet<TEntity>> GetDbSetMock()
        {
            var dbSetMock = new Mock<DbSet<TEntity>>();
            var entityList = EntityList.AsQueryable();
            dbSetMock.As<IQueryable<TEntity>>().Setup(q => q.Provider).Returns(entityList.Provider);
            dbSetMock.As<IQueryable<TEntity>>().Setup(q => q.Expression).Returns(entityList.Expression);
            dbSetMock.As<IQueryable<TEntity>>().Setup(q => q.ElementType).Returns(entityList.ElementType);
            dbSetMock.As<IQueryable<TEntity>>().Setup(q => q.GetEnumerator()).Returns(entityList.GetEnumerator());
            return dbSetMock;
        }

        #region GetById

        [Test]
        public void GetById_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);

            //Act
            var result = this.Repository.GetById(entity.Id);

            // Assert
            Assert.IsInstanceOf<TEntity>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            AssertHelper.AreEqualEntities(entity, result, "error in returning correct entity");
        }

        [Test]
        public void GetById_IdIs0_ReturnsNull()
        {
            // Arrange
            TKey id = (TKey)Convert.ChangeType(0, typeof(TKey));
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);

            //Act
            var result = this.Repository.GetById(id);

            // Assert
            Assert.IsNotInstanceOf<TEntity>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            Assert.IsNull(result, "error in returning null entity");
        }

        [Test]
        public void GetByIdAsync_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>().FindAsync(entity.Id)).ReturnsAsync(entity);

            //Act
            var result = this.Repository.GetByIdAsync(entity.Id).Result;

            // Assert
            Assert.IsInstanceOf<TEntity>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>().FindAsync(entity.Id),
                "error in calling the correct method"); // Verifies that DBContext.Set<TEntity>().FindAsync was called
            Assert.AreEqual(entity, result, "error in returning correct entity");
        }

        [Test]
        public void GetByIdAsync_IdIs0_ReturnsNull()
        {
            // Arrange
            TEntity entity = null;
            TKey id = (TKey)Convert.ChangeType(0, typeof(TKey));
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>().FindAsync(id)).ReturnsAsync(entity);

            //Act
            var result = this.Repository.GetByIdAsync(id).Result;

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>().FindAsync(id),
                "error in calling the correct method");  // Verifies that DBContext.Set<TEntity>().FindAsync was called
            Assert.IsNull(result, "error in returning null entity");
        }

        #endregion /GetById

        #region GetCount

        [Test]
        public void GetCount_ReturnsOK()
        {
            // Arrange
            int count = this.EntityList.Count();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => true;

            //Act
            var result = this.Repository.GetCount(filter);

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        [Test]
        public void GetCountAsync_ReturnsOK()
        {
            // Arrange
            int count = this.EntityList.Count();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => true;

            //Act
            var result = this.Repository.GetCountAsync(filter).Result;

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        #endregion /GetCount

        #region GetSingle
   
        [Test]
        public void GetSingle_ReturnsOK()
        {
            // Arrange
            TKey id = (TKey)Convert.ChangeType(3, typeof(TKey));
            var entity = this.EntityList.Where(q => q.Id.Equals(id)).Single();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => q.Id.Equals(id);

            //Act
            var result = this.Repository.GetSingle(filter);

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            AssertHelper.AreEqualEntities(entity, result, "error in returning correct entity");
        }

        [Test]
        public void GetSingle_IdIsInvalid_ReturnsNull()
        {
            // Arrange
            TEntity entity = null;
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => q.Id.Equals(-1);

            //Act
            var result = this.Repository.GetSingle(filter);

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            AssertHelper.AreEqualEntities(entity, result, "error in returning correct entity");
        }

        [Test]
        public void GetSingleAsync_ReturnsOK()
        {
            // Arrange
            TKey id = (TKey)Convert.ChangeType(3, typeof(TKey));
            var entity = EntityList.Single(q => q.Id.Equals(id));
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => q.Id.Equals(id);

            //Act
            var result = this.Repository.GetSingleAsync(filter).Result;

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            AssertHelper.AreEqualEntities(entity, result, "error in returning correct entity");
        }

        [Test]
        public void GetSingleAsync_IdIsInvalid_ReturnsNull()
        {
            // Arrange
            TEntity entity = null;
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => q.Id.Equals(-1);

            //Act
            var result = this.Repository.GetSingleAsync(filter).Result;

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            Assert.AreEqual(entity, result, "error in returning correct entity");
        }

        #endregion /GetSingle

        #region GetQueryable

        [Test]
        public void GetQueryable_ReturnsOK()
        {
            // Arrange
            var entityList = EntityList.AsQueryable();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);

            //Act
            var result = this.Repository.GetQueryable();

            // Assert            
            Assert.IsInstanceOf<IQueryable<TEntity>>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            AssertHelper.AreEqualEntities(entityList, result, "error in returning correct entities");
        }

        [Test]
        public void GetQueryableAsync_ReturnsOK()
        {
            // Arrange
            var entityList = EntityList.AsQueryable();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);

            //Act
            var result = this.Repository.GetQueryableAsync().Result;

            // Assert            
            Assert.IsInstanceOf<IQueryable<TEntity>>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            AssertHelper.AreEqualEntities(entityList, result, "error in returning correct entities");
        }

        #endregion /GetQueryable

        #region GetEnumerable

        [Test]
        public void GetEnumerable_ReturnsOK()
        {
            // Arrange
            var entityList = EntityList.OrderByDescending(q => q.Id).Skip(0).Take(5).AsEnumerable();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => true;
            var sorts = new List<Sort>() { new Sort("Id", SortDirection.DESC) };
            Page page = new Page(1, 5);

            //Act
            var result = this.Repository.GetEnumerable(filter, sorts, page);

            // Assert            
            Assert.IsInstanceOf<IEnumerable<TEntity>>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            AssertHelper.AreEqualEntities(entityList, result, "error in returning correct entities");
        }

        [Test]
        public void GetEnumerableAsync_ReturnsOK()
        {
            // Arrange
            var entityList = EntityList.OrderByDescending(q => q.Id).Skip(0).Take(5).AsEnumerable();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => true;
            IList<Sort> sorts = new List<Sort>() { new Sort("Id", SortDirection.DESC) };
            Page page = new Page(1, 5);

            //Act
            var result = this.Repository.GetEnumerableAsync(filter, sorts, page).Result;

            // Assert            
            Assert.IsInstanceOf<IEnumerable<TEntity>>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            AssertHelper.AreEqualEntities(entityList, result, "error in returning correct entities");
        }

        #endregion /GetEnumerable

        #endregion /Methods  

    }
}
