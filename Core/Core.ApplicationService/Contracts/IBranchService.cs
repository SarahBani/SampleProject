using Core.DomainModel.Entities;
using Core.DomainServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.ApplicationService.Contracts
{
    public interface IBranchService
    {

        Task<Branch> GetByIdAsync(int id);

        Task<IList<Branch>> GetAllAsync();

        Task<IList<Branch>> GetListByBankIdAsync(int bankId);

        Task<TransactionResult> InsertAsync(Branch branch);

        Task<TransactionResult> UpdateAsync(Branch branch);

        Task<TransactionResult> DeleteAsync(int id);

    }
}
