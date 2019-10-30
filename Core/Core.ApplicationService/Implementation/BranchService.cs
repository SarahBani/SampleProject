using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using Core.DomainService;
using Core.DomainService.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ApplicationService.Implementation
{
    public class BranchService : BaseService<IBranchRepository, Branch, int>, IBranchService
    {

        #region Properties

        #endregion /Properties

        #region Constructors

        public BranchService(IEntityService entityService)
            : base(entityService)
        {
        }

        #endregion /Constructors

        #region Methods

        public int GetCountByBankId(int bankId)
        {
            return base.GetCount(q => q.BankId.Equals(bankId));
        }

        public IList<Branch> GetListByBankId(int bankId)
        {
            return  base.GetEnumerable(q => q.BankId.Equals(bankId)).ToList();
        }

        public async Task<TransactionResult> DeleteByBankIdAsync(int bankId)
        {
            foreach (var bank in GetListByBankId(bankId))
            {
                await base.DeleteAsync(bank);
            }
            return new TransactionResult();
        }

        #endregion /Methods

    }
}
