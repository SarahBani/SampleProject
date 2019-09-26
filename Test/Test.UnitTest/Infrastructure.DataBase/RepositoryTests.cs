using Core.DomainModel.Entities;
using Infrastructure.DataBase.Repositoy;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

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
        public void Insert_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            base.DataBaseContextMock.Setup(q => q.Add(It.IsAny<TEntity>())).Verifiable();

            //Act
            this.Repository.Insert(entity);

            // Assert
            this.DataBaseContextMock.Verify(q => q.Add(It.IsAny<TEntity>()), "error in calling the correct method");
        }

        [Test]
        public void InsertAsync_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            base.DataBaseContextMock.Setup(q => q.AddAsync(It.IsAny<TEntity>(), default)).Verifiable();

            //Act
            var _ = this.Repository.InsertAsync(entity);

            // Assert
            this.DataBaseContextMock.Verify(q => q.AddAsync(It.IsAny<TEntity>(), default), "error in calling the correct method");
        }

        [Test]
        public void Update_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            base.DataBaseContextMock.Setup(q => q.Attach(It.IsAny<TEntity>())).Verifiable();

            //Act
            this.Repository.Update(entity);

            // Assert
            this.DataBaseContextMock.Verify(q => q.Attach(It.IsAny<TEntity>()), "error in calling the correct method");
        }

        #region Delete

        [Test]
        public void Delete_ById_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            base.DataBaseContextMock.Setup(q => q.Attach(It.IsAny<TEntity>())).Verifiable();
            base.DataBaseContextMock.Setup(q => q.Remove(It.IsAny<TEntity>())).Verifiable();

            //Act
            this.Repository.Delete(entity.Id);

            // Assert
            this.DataBaseContextMock.Verify(q => q.Attach(It.IsAny<TEntity>()), "error in calling the correct method");
            this.DataBaseContextMock.Verify(q => q.Remove(It.IsAny<TEntity>()), "error in calling the correct method");
        }

        [Test]
        public void Delete_ByEntity_ReturnsOK()
        {
            // Arrange
            var entity = Entity;
            base.DataBaseContextMock.Setup(q => q.Attach(It.IsAny<TEntity>())).Verifiable();
            base.DataBaseContextMock.Setup(q => q.Remove(It.IsAny<TEntity>())).Verifiable();

            //Act
            this.Repository.Delete(entity);

            // Assert
            this.DataBaseContextMock.Verify(q => q.Attach(It.IsAny<TEntity>()), "error in calling the correct method");
            this.DataBaseContextMock.Verify(q => q.Remove(It.IsAny<TEntity>()), "error in calling the correct method");
        }

        //[Test]
        //public void Delete_ByFilter_ReturnsOK()
        //{
        //    // Arrange
        //    var entities = EntityList.AsQueryable();
        //    Expression<Func<TEntity, bool>> filter = q => q.Id.Equals(6);
        //    base.DataBaseContextMock.Setup(q => q.Set<TEntity>().AsQueryable().Where(filter)).Returns(entities);
        //    base.DataBaseContextMock.Setup(q => q.RemoveRange(It.IsAny<TEntity>())).Verifiable();

        //    //Act
        //    this.Repository.Delete(filter);

        //    // Assert
        //    this.DataBaseContextMock.Verify(q => q.Set<TEntity>().AsQueryable().Where(filter), "error in calling the correct method");
        //    this.DataBaseContextMock.Verify(q => q.RemoveRange(It.IsAny<TEntity>()), "error in calling the correct method");
        //}

        #endregion /Delete

        #endregion /Methods

    }
}
