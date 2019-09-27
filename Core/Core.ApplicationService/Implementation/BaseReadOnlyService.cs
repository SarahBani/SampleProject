using Core.DomainModel.Entities;
using Core.DomainServices;
using Core.DomainServices.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.ApplicationService.Implementation
{
    public abstract class BaseReadOnlyService<TEntity, TKey>
         where TEntity : Entity<TKey>
    {

        #region Properties

        private IReadOnlyRepository<TEntity, TKey> _repository;

        protected IEntityService EntityService { get; set; }

        #endregion /Properties

        #region Constructors

        public BaseReadOnlyService(IEntityService entityService)
        {
            this.EntityService = entityService;
            this._repository = this.EntityService.GetRepository<TEntity, TKey>();
            SetRepository();
        }

        public BaseReadOnlyService()
        {

        }

        #endregion /Constructors

        #region Methods

        protected abstract void SetRepository();

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await this._repository.GetByIdAsync(id);
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await this._repository.GetCountAsync(filter);
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await this._repository.GetSingleAsync(filter);
        }

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await Task.Run(() => this.GetQueryableAsync().Result.ToList());
        }

        protected async Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            return await this._repository.GetQueryableAsync();
        }

        protected async Task<IEnumerable<TEntity>> GetEnumerableAsync(
            Expression<Func<TEntity, bool>> filter = null,
            IList<Sort> sorts = null,
            Page page = null)
        {
            return await this._repository.GetEnumerableAsync(filter, sorts, page);
        }

        #endregion /Methods
    }
}
