using Core.DomainModel.Entities;
using System;
using System.Threading.Tasks;

namespace Core.DomainServices.Repositoy
{
    public interface IWebServiceAssignmentRepository : IReadOnlyRepository<WebServiceAssignment, short>
    {

        Task<WebServiceAssignment> GetByTokenAsync(Guid token);

    }
}
