using Core.DomainModel.Entities;
using Core.DomainService;
using Core.DomainService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Core.ApplicationService.Implementation
{
    public abstract class BaseReadOnlyService<TRepository, TEntity, TKey>
        where TRepository : IReadOnlyRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        #region Properties

        protected IEntityService EntityService { get; set; }

        protected TRepository Repository { get; private set; }

        #endregion /Properties

        #region Constructors

        public BaseReadOnlyService(IEntityService entityService)
        {
            this.EntityService = entityService;
            this.Repository = (TRepository)this.EntityService.GetRepository<TEntity, TKey>();
        }

        public BaseReadOnlyService()
        {

        }

        #endregion /Constructors

        #region Methods

        public virtual Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return this.Repository.GetByIdAsync(id, cancellationToken);
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return this.Repository.GetCount(filter);
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            return this.Repository.GetSingle(filter);
        }

        public virtual IList<TEntity> GetAll()
        {
            return this.GetQueryable().ToList();
        }

        protected IQueryable<TEntity> GetQueryable()
        {
            return this.Repository.GetQueryable();
        }

        protected IEnumerable<TEntity> GetEnumerable(
            Expression<Func<TEntity, bool>> filter = null,
            IList<Sort> sorts = null,
            Page page = null)
        {
            return this.Repository.GetEnumerable(filter, sorts, page);
        }

        #endregion /Methods
    }
}
