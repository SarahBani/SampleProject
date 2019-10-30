using Core.DomainModel.Entities;
using Core.DomainService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.ApplicationService.Contracts
{
    public interface IBankService
    {

        Task<Bank> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        int GetCount();

        IList<Bank> GetAll();

        Task<TransactionResult> InsertAsync(Bank bank);

        Task<TransactionResult> UpdateAsync(Bank bank);

        Task<TransactionResult> DeleteAsync(int id);

    }
}
