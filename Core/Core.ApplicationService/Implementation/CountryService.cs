using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using Core.DomainServices.Repositoy;

namespace Core.ApplicationService.Implementation
{
    public class CountryService : BaseReadOnlyService<Country, short>, ICountryService
    {

        #region Properties

        private ICountryRepository _repository;

        #endregion /Properties

        #region Constructors

        public CountryService(IEntityService entityService)
            : base(entityService)
        {
        }

        #endregion /Constructors

        #region Methods  

        protected override void SetRepository()
        {
            this._repository = base.EntityService.GetRepository<Country, short>() as ICountryRepository;
        }

        #endregion /Methods

    }
}
