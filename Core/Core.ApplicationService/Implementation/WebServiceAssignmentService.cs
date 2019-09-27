using Core.ApplicationService.Contracts;
using Core.DomainModel;
using Core.DomainModel.Entities;
using Core.DomainServices;
using Core.DomainServices.Repositoy;
using System;

namespace Core.ApplicationService.Implementation
{
    public class WebServiceAssignmentService : BaseReadOnlyService<WebServiceAssignment, short>,
        IWebServiceAssignmentService
    {

        #region Properties

        private IWebServiceAssignmentRepository _repository;

        #endregion /Properties

        #region Constructors

        public WebServiceAssignmentService(IEntityService entityService)
            : base(entityService)
        {
        }

        #endregion /Constructors

        #region Methods  

        protected override void SetRepository()
        {
            this._repository = base.EntityService.GetRepository<WebServiceAssignment, short>() as IWebServiceAssignmentRepository;
        }

        public TransactionResult GetValidationByToken(Guid token)
        {
            var webServiceAssignment = this._repository.GetByTokenAsync(token).Result;
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
