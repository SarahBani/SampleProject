using Core.ApplicationService.Contracts;
using Core.DomainModel;
using Core.DomainModel.Entities;
using Core.DomainService;
using Core.DomainService.Repositoy;
using System;

namespace Core.ApplicationService.Implementation
{
    public class WebServiceAssignmentService : BaseReadOnlyService<IWebServiceAssignmentRepository, WebServiceAssignment, short>,
        IWebServiceAssignmentService
    {

        #region Properties

        #endregion /Properties

        #region Constructors

        public WebServiceAssignmentService(IEntityService entityService)
            : base(entityService)
        {
        }

        #endregion /Constructors

        #region Methods  

        public TransactionResult GetValidationByToken(Guid token)
        {
            var webServiceAssignment = base.Repository.GetByTokenAsync(token).Result;
            if (webServiceAssignment == null)
            {
                return new TransactionResult(new CustomException(ExceptionKey.InvalidWebServiceAssignmentToken));
            }
            if (webServiceAssignment.ValidationDate < DateTime.Now)
            {
                return new TransactionResult(new CustomException(ExceptionKey.WebServiceAssignmentExpired));
            }
            return new TransactionResult();
        }

        #endregion /Methods

    }
}
