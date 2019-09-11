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

        protected IReadOnlyRepository<TEntity, TKey> Repository { get; private set; }

        protected IEntityService EntityService { get; set; }

        #endregion /Properties

        #region Constructors

        public BaseReadOnlyService(IEntityService entityService)
        {
            this.EntityService = entityService;
            this.Repository = this.EntityService.GetRepository<TEntity, TKey>();
        }

        public BaseReadOnlyService()
        {

        }

        #endregion /Constructors

        #region Methods

        //public virtual TEntity GetById(TKey id)
        //{
        //    return this.Repository.GetById(id);
        //}

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await this.Repository.GetByIdAsync(id);
        }

        //public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        //{
        //    return this.Repository.GetCount(filter);
        //}

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await this.Repository.GetCountAsync(filter);
        }

        //public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        //{
        //    return this.Repository.GetSingle(filter);
        //}

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await this.Repository.GetSingleAsync(filter);
        }

        //public virtual IList<TEntity> GetAll()
        //{
        //    return this.Repository.GetQueryable().ToList();
        //}

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await Task.Run(() => this.GetQueryableAsync().Result.ToList());
        }

        //protected IQueryable<TEntity> GetQueryable()
        //{
        //    return await this.Repository.GetQueryable();
        //}

        protected async Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            return await this.Repository.GetQueryableAsync();
        }

        //protected IEnumerable<TEntity> GetEnumerable(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    IList<Sort> sorts = null,
        //    Page page = null)
        //{
        //    return this.Repository.GetEnumerable(filter, sorts, page);
        //}

        protected async Task<IEnumerable<TEntity>> GetEnumerableAsync(
            Expression<Func<TEntity, bool>> filter = null,
            IList<Sort> sorts = null,
            Page page = null)
        {
            return await this.Repository.GetEnumerableAsync(filter, sorts, page);
        }

        #endregion /Methods
    }
}
