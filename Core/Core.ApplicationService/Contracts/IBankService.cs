using Core.DomainModel.Entities;
using Core.DomainServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.ApplicationService.Contracts
{
    public interface IBankService
    {

        Task<Bank> GetByIdAsync(int id);

        Task<int> GetCountAsync();

        Task<IList<Bank>> GetAllAsync();

        Task<TransactionResult> InsertAsync(Bank bank);

        Task<TransactionResult> UpdateAsync(Bank bank);

        Task<TransactionResult> DeleteAsync(int id);

    }
}
