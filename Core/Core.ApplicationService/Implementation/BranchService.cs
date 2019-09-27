using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using Core.DomainServices;
using Core.DomainServices.Repositoy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ApplicationService.Implementation
{
    public class BranchService : BaseService<Branch, int>, IBranchService
    {

        #region Properties

        private IBranchRepository _repository;

        #endregion /Properties

        #region Constructors

        public BranchService(IEntityService entityService)
            : base(entityService)
        {
        }

        #endregion /Constructors

        #region Methods

        protected override void SetRepository()
        {
            this._repository = base.EntityService.GetRepository<Branch, int>() as IBranchRepository;
        }

        public Task<int> GetCountByBankIdAsync(int bankId)
        {
            return base.GetCountAsync(q => q.BankId.Equals(bankId));
        }

        public async Task<IList<Branch>> GetListByBankIdAsync(int bankId)
        {
            return await Task.Run(() => base.GetEnumerableAsync(q => q.BankId.Equals(bankId))
                .Result.ToList());
        }

        public async Task<TransactionResult> DeleteByBankIdAsync(int bankId)
        {
            foreach (var bank in GetListByBankIdAsync(bankId).Result)
            {
                await base.DeleteAsync(bank);
            }
            return new TransactionResult();
        }

        #endregion /Methods

    }
}
