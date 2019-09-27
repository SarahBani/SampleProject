using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using Core.DomainServices;
using Core.DomainServices.Repositoy;
using System.Threading.Tasks;

namespace Core.ApplicationService.Implementation
{
    public class BankService : BaseService<Bank, int>, IBankService
    {

        #region Properties

        private IBankRepository _repository;

        #endregion /Properties

        #region Constructors

        public BankService(IEntityService entityService)
            : base(entityService)
        {
        }

        #endregion /Constructors

        #region Methods

        protected override void SetRepository()
        {
            this._repository = base.EntityService.GetRepository<Bank, int>() as IBankRepository;
        }

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
