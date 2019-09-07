using Core.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DomainServices.Repositoy
{
    public interface IReadOnlyRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        TEntity GetById(TKey id);

        Task<TEntity> GetByIdAsync(TKey id);

        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);

        TEntity GetSingle(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter);

        IQueryable<TEntity> GetQueryable();

        Task<IQueryable<TEntity>> GetQueryableAsync();

        IEnumerable<TEntity> GetEnumerable(
             Expression<Func<TEntity, bool>> filter = null,
             IList<Sort> sorts = null,
             Page page = null);

        Task<IEnumerable<TEntity>> GetEnumerableAsync(
             Expression<Func<TEntity, bool>> filter = null,
             IList<Sort> sorts = null,
             Page page = null);

    }
}
