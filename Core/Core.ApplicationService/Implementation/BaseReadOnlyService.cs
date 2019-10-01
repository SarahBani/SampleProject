﻿using Core.DomainModel.Entities;
using Core.DomainServices;
using Core.DomainServices.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await this.Repository.GetByIdAsync(id);
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await this.Repository.GetCountAsync(filter);
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await this.Repository.GetSingleAsync(filter);
        }

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await Task.Run(() => this.GetQueryableAsync().Result.ToList());
        }

        protected async Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            return await this.Repository.GetQueryableAsync();
        }

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
