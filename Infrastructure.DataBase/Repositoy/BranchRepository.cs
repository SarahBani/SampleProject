using Core.DomainModel.Entities;
using Core.DomainServices.Repositoy;
using Infrastructure.DataBase.Repositoy;

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
