using Core.DomainModel.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DomainServices.Repositoy
{
    public interface IRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TKey id);

        void Delete(TEntity entity);

        void Delete(Expression<Func<TEntity, bool>> filter = null);

    }
}
