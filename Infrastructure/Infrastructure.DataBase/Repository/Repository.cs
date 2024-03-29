﻿using Core.DomainModel.Entities;
using Core.DomainService.Repository;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.DataBase.Repository
{
    public abstract class Repository<TEntity, TKey> : ReadOnlyRepository<TEntity, TKey>, IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        #region Properties

        #endregion /Properties

        #region Constructors

        public Repository(SampleDataBaseContext dbContext)
            : base(dbContext)
        {
        }

        #endregion /Constructors

        #region Methods

        public virtual void Insert(TEntity entity)
        {
            base.MyDBContext.Add(entity);
        }

        public virtual Task InsertAsync(TEntity entity)
        {
            return base.MyDBContext.AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            base.MyDBContext.Attach(entity);
            base.MyDBContext.SetModified(entity);
        }

        public virtual void Delete(TKey id)
        {
            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = id;
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            base.MyDBContext.Attach(entity);
            base.MyDBContext.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> filter = null)
        {
            var entities = base.GetEnumerable(filter);
            base.MyDBContext.RemoveRange(entities);
        }

        #endregion /Methods

    }
}
