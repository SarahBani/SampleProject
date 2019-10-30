using Core.DomainModel.Entities;
using Core.DomainService;
using Core.DomainService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.DataBase.Repository
{
    public abstract class ReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        #region Properties

        protected readonly SampleDataBaseContext MyDBContext;

        #endregion /Properties

        #region Constructors

        public ReadOnlyRepository(SampleDataBaseContext dbContext)
        {
            this.MyDBContext = dbContext;
        }

        #endregion /Constructors

        #region Methods

        public virtual TEntity GetById(TKey id)
        {
            return GetSingle(q => q.Id.Equals(id));
        }

        public virtual Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return this.MyDBContext.Set<TEntity>().FindAsync(id, cancellationToken);
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return this.MyDBContext.Set<TEntity>().Count(filter);
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            return this.MyDBContext.Set<TEntity>().Where(filter).SingleOrDefault();
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return this.MyDBContext.Set<TEntity>().AsQueryable();
        }

        public virtual IEnumerable<TEntity> GetEnumerable(
            Expression<Func<TEntity, bool>> filter = null,
            IList<Sort> sorts = null,
            Page page = null)
        {
            return this.MyDBContext.Set<TEntity>()
                .Where(filter)
                .SetOrder(sorts)
                .SetPage(page);
        }

        #endregion /Methods

    }
}
