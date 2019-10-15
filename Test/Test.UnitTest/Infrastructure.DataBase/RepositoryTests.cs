using Core.DomainModel.Entities;
using Infrastructure.DataBase.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Test.UnitTest.Infrastructure.DataBase
{
    public abstract class RepositoryTests<TEntity, TKey> : ReadOnlyRepositoryTests<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        #region Properties

        protected new Repository<TEntity, TKey> Repository
        {
            get => base.Repository as Repository<TEntity, TKey>;
        }

        #endregion /Properties

        #region Consructors

        public RepositoryTests()
        {
        }

        #endregion /Consructors

        #region Methods  

        [Test]
        public async Task Insert_ReturnsOK()
        {
            // Arrange
            var entity = this.Entity;
            base.DataBaseContextMock.Setup(q => q.Add(It.IsAny<TEntity>())).Verifiable();

            //Act
            await Task.Run(() => this.Repository.Insert(entity));

            // Assert
            this.DataBaseContextMock.Verify(q => q.Add(It.IsAny<TEntity>()), "error in calling the correct method");
        }

        [Test]
        public async Task InsertAsync_ReturnsOK()
        {
            // Arrange
            var entity = this.Entity;
            base.DataBaseContextMock.Setup(q => q.AddAsync(It.IsAny<TEntity>(), default)).Verifiable();

            //Act
            await this.Repository.InsertAsync(entity);

            // Assert
            this.DataBaseContextMock.Verify(q => q.AddAsync(It.IsAny<TEntity>(), default), "error in calling the correct method");
        }

        [Test]
        public async Task Update_ReturnsOK()
        {
            // Arrange
            var entity = this.Entity;
            base.DataBaseContextMock.Setup(q => q.Attach(It.IsAny<TEntity>())).Verifiable();
            this.DataBaseContextMock.Setup(q => q.SetModified(It.IsAny<TEntity>())).Verifiable();

            //Act
            await Task.Run(() => this.Repository.Update(entity));

            // Assert
            this.DataBaseContextMock.Verify(q => q.Attach(It.IsAny<TEntity>()), "error in calling the correct method");
            this.DataBaseContextMock.Verify(q => q.SetModified(It.IsAny<TEntity>()), "error in calling the correct method");
        }

        #region Delete

        [Test]
        public async Task Delete_ById_ReturnsOK()
        {
            // Arrange
            var entity = this.Entity;
            base.DataBaseContextMock.Setup(q => q.Attach(It.IsAny<TEntity>())).Verifiable();
            base.DataBaseContextMock.Setup(q => q.Remove(It.IsAny<TEntity>())).Verifiable();

            //Act
            await Task.Run(() => this.Repository.Delete(entity.Id));

            // Assert
            this.DataBaseContextMock.Verify(q => q.Attach(It.IsAny<TEntity>()), "error in calling the correct method");
            this.DataBaseContextMock.Verify(q => q.Remove(It.IsAny<TEntity>()), "error in calling the correct method");
        }

        [Test]
        public async Task Delete_ByEntity_ReturnsOK()
        {
            // Arrange
            var entity = this.Entity;
            base.DataBaseContextMock.Setup(q => q.Attach(It.IsAny<TEntity>())).Verifiable();
            base.DataBaseContextMock.Setup(q => q.Remove(It.IsAny<TEntity>())).Verifiable();

            //Act
            await Task.Run(() => this.Repository.Delete(entity));

            // Assert
            this.DataBaseContextMock.Verify(q => q.Attach(It.IsAny<TEntity>()), "error in calling the correct method");
            this.DataBaseContextMock.Verify(q => q.Remove(It.IsAny<TEntity>()), "error in calling the correct method");
        }

        [Test]
        public async Task Delete_ByFilter_ReturnsOK()
        {
            // Arrange
            Expression<Func<TEntity, bool>> filter = q => long.Parse(q.Id.ToString()) >= 3;
            var entities = EntityList.AsQueryable().Where(filter);
            var dbSetMock = base.GetDbSetMock();
            base.DataBaseContextMock.Setup(q => q.Set<TEntity>()).Returns(dbSetMock.Object);
            base.DataBaseContextMock.Setup(q => q.RemoveRange(It.IsAny<IEnumerable<TEntity>>())).Verifiable();

            //Act
            await Task.Run(() => this.Repository.Delete(filter));

            // Assert
            this.DataBaseContextMock.Verify(q => q.Set<TEntity>(), "error in calling the correct method");
            this.DataBaseContextMock.Verify(q => q.RemoveRange(It.IsAny<IEnumerable<TEntity>>()), "error in calling the correct method");
        }

        #endregion /Delete

        #endregion /Methods

    }
}
