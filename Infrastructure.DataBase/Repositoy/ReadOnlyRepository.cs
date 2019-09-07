using Core.DomainModel.Entities;
using Core.DomainServices;
using Core.DomainServices.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.DataBase.Repositoy
{
    public abstract class ReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        #region Properties

        protected readonly SampleDataBaseContext  MyDBContext;

        #endregion /Properties

        #region Constructors

        public ReadOnlyRepository(SampleDataBaseContext  dbContext)
        {
            this.MyDBContext = dbContext;
        }

        #endregion /Constructors

        #region Methods

        public virtual TEntity GetById(TKey id)
        {
            return GetSingle(q => q.Id.Equals(id));
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await this.MyDBContext.Set<TEntity>().FindAsync(id);
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable().Count(filter);
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await Task.Run(() => this.MyDBContext.Set<TEntity>()
                .Count(filter));
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            return GetQueryable().Where(filter).SingleOrDefault();
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Task.Run(() => this.MyDBContext.Set<TEntity>()
                .Where(filter)
                .SingleOrDefault());
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return this.MyDBContext.Set<TEntity>().AsQueryable();
        }

        public virtual async Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            return await Task.Run(() => this.MyDBContext.Set<TEntity>().AsQueryable());
        }

        public virtual IEnumerable<TEntity> GetEnumerable(
            Expression<Func<TEntity, bool>> filter = null,
            IList<Sort> sorts = null,
            Page page = null)
        {
            return GetQueryable()
                .Where(filter)
                .SetOrder(sorts)
                .SetPage(page);
        }

        public virtual async Task<IEnumerable<TEntity>> GetEnumerableAsync(
            Expression<Func<TEntity, bool>> filter = null,
            IList<Sort> sorts = null,
            Page page = null)
        {
            return await Task.Run(() =>
            this.MyDBContext.Set<TEntity>()
                .Where(filter)
                .SetOrder(sorts)
                .SetPage(page));
        }

        #endregion /Methods

    }
}
