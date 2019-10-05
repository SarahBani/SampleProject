using Core.DomainService;
using System;

namespace Core.ApplicationService.Contracts
{
    public interface IWebServiceAssignmentService
    {

        TransactionResult GetValidationByToken(Guid token);

    }
}
