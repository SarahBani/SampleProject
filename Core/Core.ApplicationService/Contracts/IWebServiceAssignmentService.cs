using Core.DomainServices;
using System;

namespace Core.ApplicationService.Contracts
{
    public interface IWebServiceAssignmentService
    {

        TransactionResult GetValidationByToken(Guid token);

    }
}
