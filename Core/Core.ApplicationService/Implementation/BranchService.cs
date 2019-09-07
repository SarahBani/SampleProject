using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ApplicationService.Implementation
{
    public class BranchService : BaseService<Branch, int>, IBranchService
    {

        #region Properties

        #endregion /Properties

        #region Constructors

        public BranchService(EntityService entityService)
            : base(entityService)
        {
        }

        #endregion /Constructors

        #region Methods

        public async Task<IList<Branch>> GetListByBankIdAsync(int bankId)
        {
            return await Task.Run(() =>
            base.GetQueryableAsync().Result
                .Where(q => q.BankId.Equals(bankId))
                .ToList());
        }

        #endregion /Methods

    }
}
