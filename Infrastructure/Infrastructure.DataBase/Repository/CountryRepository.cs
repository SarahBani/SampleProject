using Core.DomainModel.Entities;
using Core.DomainService.Repository;

namespace Infrastructure.DataBase.Repository
{
    public class CountryRepository : Repository<Country, short>, ICountryRepository
    {
        public CountryRepository(SampleDataBaseContext dbContext)
            : base(dbContext)
        {

        }
    }
}
