using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using Core.DomainService.Repository;
using System.Threading.Tasks;

namespace Core.ApplicationService.Implementation
{
    public class BankService : BaseService<IBankRepository, Bank, int>, IBankService
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

        public int GetCount()
        {
            return base.GetCount();
        }

        #endregion /Methods

    }
}
