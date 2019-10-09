using Core.DomainModel.Entities;
using Core.DomainService.Repository;

namespace Infrastructure.DataBase.Repository
{
    public class BranchRepository : Repository<Branch, int>, IBranchRepository
    {
        public BranchRepository(SampleDataBaseContext dbContext)
            : base(dbContext)
        {

        }
    }
}
