using Core.DomainModel.Entities;
using Core.DomainServices.Repositoy;
using Infrastructure.DataBase.Repositoy;
using System;
using System.Threading.Tasks;

namespace Infrastructure.DataBase.Repository
{
    public class WebServiceAssignmentRepository : Repository<WebServiceAssignment, short>,
        IWebServiceAssignmentRepository
    {

        #region Constructors

        public WebServiceAssignmentRepository(SampleDataBaseContext dbContext)
            : base(dbContext)
        {

        }

        #endregion /Constructors

        #region Methods

        public async Task<WebServiceAssignment> GetByTokenAsync(Guid token)
        {
            return await base.GetSingleAsync(q => q.Token.Equals(token));
        }

        #endregion /Methods

    }
}