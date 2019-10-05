using Core.DomainModel.Entities;
using Core.DomainService.Repositoy;
using Infrastructure.DataBase.Repositoy;

namespace Infrastructure.DataBase.Repository
{
    public class BankRepository : Repository<Bank, int>, IBankRepository
    {
        public BankRepository(SampleDataBaseContext dbContext)
            : base(dbContext)
        {

        }
    }
}
