using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using Core.DomainServices;
using System.Threading.Tasks;

namespace Core.ApplicationService.Implementation
{
    public class BankService : BaseService<Bank, int>, IBankService
    {

        #region Properties

        #endregion /Properties

        #region Constructors

        public BankService(IEntityService entityService)
            : base(entityService)
        {
        }

        #endregion /Constructors

        #region Methods

        public async Task<int> GetCountAsync()
        {
            return await base.GetCountAsync();
        }

        public override async Task<TransactionResult> InsertAsync(Bank bank)
        {
            return await base.InsertAsync(bank);
        }

        #endregion /Methods

    }
}
