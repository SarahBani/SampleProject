using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using Core.DomainServices;
using Core.DomainServices.Repositoy;

namespace Core.ApplicationService
{
    public interface IEntityService
    {

        IUnitOfWork UnitOfWork { get; }

        IBankRepository BankRepository { get; }

        IBranchRepository BranchRepository { get; }

        IBankService BankService { get; }

        IBranchService BranchService { get; }

        IReadOnlyRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : Entity<TKey>;

    }
}
