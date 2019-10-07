using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using Core.DomainService;
using Core.DomainService.Repositoy;

namespace Core.ApplicationService
{
    public interface IEntityService
    {

        IUnitOfWork UnitOfWork { get; }

        ICountryRepository CountryRepository { get; }

        IBankRepository BankRepository { get; }

        IBranchRepository BranchRepository { get; }

        ICountryService CountryService { get; }

        IBankService BankService { get; }

        IBranchService BranchService { get; }

        IReadOnlyRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : Entity<TKey>;

    }
}
