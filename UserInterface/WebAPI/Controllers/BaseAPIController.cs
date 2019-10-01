using Core.ApplicationService.Contracts;
using Core.DomainModel;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    public class BaseAPIController : ControllerBase
    {

        #region Properties

        private readonly IWebServiceAssignmentService _webServiceAssignmentService;

        #endregion /Properties

        #region Constructors

        public BaseAPIController(IWebServiceAssignmentService webServiceAssignmentService)
        {
            this._webServiceAssignmentService = webServiceAssignmentService;
        }

        #endregion /Constructors

        #region Methods

        protected bool IsTokenValid(string token)
        {
            if (!string.IsNullOrEmpty(token) && Guid.TryParse(token, out Guid validToken))
            {
                var validation = this._webServiceAssignmentService.GetValidationByToken(validToken);
                return validation.IsSuccessful;
            }
            return false;
        }

        #endregion /Methods

    }
}
