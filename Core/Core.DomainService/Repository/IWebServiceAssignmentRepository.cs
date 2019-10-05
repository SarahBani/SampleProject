using Core.DomainModel.Entities;
using System;
using System.Threading.Tasks;

namespace Core.DomainService.Repositoy
{
    public interface IWebServiceAssignmentRepository : IReadOnlyRepository<WebServiceAssignment, short>
    {

        Task<WebServiceAssignment> GetByTokenAsync(Guid token);

    }
}
