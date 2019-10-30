using Core.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Core.DomainService.Repository
{
    public interface IReadOnlyRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        TEntity GetById(TKey id);

        Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        TEntity GetSingle(Expression<Func<TEntity, bool>> filter);

        IQueryable<TEntity> GetQueryable();

        IEnumerable<TEntity> GetEnumerable(
             Expression<Func<TEntity, bool>> filter = null,
             IList<Sort> sorts = null,
             Page page = null);

    }
}
