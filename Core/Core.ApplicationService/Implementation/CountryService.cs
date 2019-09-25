using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;

namespace Core.ApplicationService.Implementation
{
    public class CountryService : BaseReadOnlyService<Country, short>, ICountryService
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
