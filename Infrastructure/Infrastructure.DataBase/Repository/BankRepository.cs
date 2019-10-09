using Core.DomainModel.Entities;
using Core.DomainService.Repository;

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
