using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using Core.DomainService.Repository;

namespace Core.ApplicationService.Implementation
{
    public class CountryService : BaseReadOnlyService<ICountryRepository, Country, short>, ICountryService
    {

        #region Properties

        #endregion /Properties

        #region Constructors

        public CountryService(IEntityService entityService)
            : base(entityService)
        {
        }

        #endregion /Constructors

        #region Methods  

        #endregion /Methods

    }
}
