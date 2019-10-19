using Core.DomainModel.Entities;
using Core.DomainService;
using Infrastructure.DataBase.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Test.Common;

namespace Test.UnitTest.Infrastructure.DataBase
{
    public abstract class ReadOnlyRepositoryTests<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        #region Properties

        //protected SampleDataBaseContext DBContext { get; private set; } // for testing in memory database

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

        [OneTimeSetUp]
        public abstract void Setup();

        private void SetDataBaseContextMock()
        {
            var options = new DbContextOptions<SampleDataBaseContext>();
            this.DataBaseContextMock = new Mock<SampleDataBaseContext>(options);
        }

        //private SampleDataBaseContext GetInMemoryDataBaseContext()
        //{
        //    var options = new DbContextOptionsBuilder<SampleDataBaseContext>()
        //       .UseInMemoryDatabase("InMemory")
        //       .Options;
        //    var dbContext = new SampleDataBaseContext(options);
        //    dbContext.Database.EnsureDeleted();
        //    dbContext.Database.EnsureCreated();
        //    return dbContext;
        //}

        protected void SetRepository<T>() where T : ReadOnlyRepository<TEntity, TKey>
        {
            this.Repository = (T)Activator.CreateInstance(typeof(T), this.DataBaseContextMock.Object);
            //this.Repository = (T)Activator.CreateInstance(typeof(T), GetInMemoryDataBaseContext());
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
        public async Task GetById_ReturnsOK()
        {
            // Arrange
            var entity = this.Entity;
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);

            //Act
            var result = await Task.Run(() => this.Repository.GetById(entity.Id));

            // Assert
            Assert.IsInstanceOf<TEntity>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            TestHelper.AreEqualEntities(entity, result, "error in returning correct entity");
        }

        [Test]
        public async Task GetById_IdIs0_ReturnsNull()
        {
            // Arrange
            TKey id = TestHelper.GetId<TKey>(0);
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);

            //Act
            var result = await Task.Run(() => this.Repository.GetById(id));

            // Assert
            Assert.IsNotInstanceOf<TEntity>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            Assert.IsNull(result, "error in returning null entity");
        }

        [Test]
        public async Task GetByIdAsync_ReturnsOK()
        {
            // Arrange
            var entity = this.Entity;
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>().FindAsync(entity.Id)).ReturnsAsync(entity);

            //Act
            var result = await this.Repository.GetByIdAsync(entity.Id);

            // Assert
            Assert.IsInstanceOf<TEntity>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>().FindAsync(entity.Id),
                "error in calling the correct method"); // Verifies that DBContext.Set<TEntity>().FindAsync was called
            Assert.AreEqual(entity, result, "error in returning correct entity");
        }

        [Test]
        public async Task GetByIdAsync_IdIs0_ReturnsNull()
        {
            // Arrange
            TEntity entity = null;
            TKey id = TestHelper.GetId<TKey>(0);
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>().FindAsync(id)).ReturnsAsync(entity);

            //Act
            var result = await this.Repository.GetByIdAsync(id);

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>().FindAsync(id),
                "error in calling the correct method");  // Verifies that DBContext.Set<TEntity>().FindAsync was called
            Assert.IsNull(result, "error in returning null entity");
        }

        #endregion /GetById

        #region GetCount

        [Test]
        public async Task GetCount_ReturnsOK()
        {
            // Arrange
            int count = this.EntityList.Count();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => true;

            //Act
            var result = await Task.Run(() => this.Repository.GetCount(filter));

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        [Test]
        public async Task GetCountAsync_ReturnsOK()
        {
            // Arrange
            int count = this.EntityList.Count();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => true;

            //Act
            var result = await this.Repository.GetCountAsync(filter);

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            Assert.AreEqual(count, result, "error in returning correct entity count");
        }

        #endregion /GetCount

        #region GetSingle

        [Test]
        public async Task GetSingle_ReturnsOK()
        {
            // Arrange
            TKey id = TestHelper.GetId<TKey>(3);
            var entity = this.EntityList.Where(q => q.Id.Equals(id)).Single();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => q.Id.Equals(id);

            //Act
            var result = await Task.Run(() => this.Repository.GetSingle(filter));

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            TestHelper.AreEqualEntities(entity, result, "error in returning correct entity");
        }

        [Test]
        public async Task GetSingle_IdIsInvalid_ReturnsNull()
        {
            // Arrange
            TEntity entity = null;
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => q.Id.Equals(-1);

            //Act
            var result = await Task.Run(() => this.Repository.GetSingle(filter));

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            TestHelper.AreEqualEntities(entity, result, "error in returning correct entity");
        }

        [Test]
        public async Task GetSingleAsync_ReturnsOK()
        {
            // Arrange
            TKey id = TestHelper.GetId<TKey>(3);
            var entity = this.EntityList.Single(q => q.Id.Equals(id));
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => q.Id.Equals(id);

            //Act
            var result = await this.Repository.GetSingleAsync(filter);

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            TestHelper.AreEqualEntities(entity, result, "error in returning correct entity");
        }

        [Test]
        public async Task GetSingleAsync_IdIsInvalid_ReturnsNull()
        {
            // Arrange
            TEntity entity = null;
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => q.Id.Equals(-1);

            //Act
            var result = await this.Repository.GetSingleAsync(filter);

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            Assert.AreEqual(entity, result, "error in returning correct entity");
        }

        #endregion /GetSingle

        #region GetQueryable

        [Test]
        public async Task GetQueryable_ReturnsOK()
        {
            // Arrange
            var entityList = EntityList.AsQueryable();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);

            //Act
            var result = await Task.Run(() => this.Repository.GetQueryable());

            // Assert            
            Assert.IsInstanceOf<IQueryable<TEntity>>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            TestHelper.AreEqualEntities(entityList, result, "error in returning correct entities");
        }

        [Test]
        public async Task GetQueryableAsync_ReturnsOK()
        {
            // Arrange
            var entityList = EntityList.AsQueryable();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);

            //Act
            var result = await this.Repository.GetQueryableAsync();

            // Assert            
            Assert.IsInstanceOf<IQueryable<TEntity>>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            TestHelper.AreEqualEntities(entityList, result, "error in returning correct entities");
        }

        #endregion /GetQueryable

        #region GetEnumerable

        [Test]
        public async Task GetEnumerable_ReturnsOK()
        {
            // Arrange
            var entityList = EntityList.OrderByDescending(q => q.Id).Skip(0).Take(5).AsEnumerable();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => true;
            var sorts = new List<Sort>() { new Sort("Id", SortDirection.DESC) };
            Page page = new Page(1, 5);

            //Act
            var result = await Task.Run(() => this.Repository.GetEnumerable(filter, sorts, page));

            // Assert            
            Assert.IsInstanceOf<IEnumerable<TEntity>>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            TestHelper.AreEqualEntities(entityList, result, "error in returning correct entities");
        }

        [Test]
        public async Task GetEnumerableAsync_ReturnsOK()
        {
            // Arrange
            var entityList = EntityList.OrderByDescending(q => q.Id).Skip(0).Take(5).AsEnumerable();
            var dbSetMock = GetDbSetMock();
            this.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            Expression<Func<TEntity, bool>> filter = q => true;
            IList<Sort> sorts = new List<Sort>() { new Sort("Id", SortDirection.DESC) };
            Page page = new Page(1, 5);

            //Act
            var result = await this.Repository.GetEnumerableAsync(filter, sorts, page);

            // Assert            
            Assert.IsInstanceOf<IEnumerable<TEntity>>(result);
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            TestHelper.AreEqualEntities(entityList, result, "error in returning correct entities");
        }

        #endregion /GetEnumerable

        #endregion /Methods  

    }
}
