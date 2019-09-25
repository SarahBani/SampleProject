using Core.DomainModel.Entities;
using Core.DomainServices.Repositoy;
using Infrastructure.DataBase.Repositoy;

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
