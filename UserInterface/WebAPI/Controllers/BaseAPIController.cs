using Core.ApplicationService.Contracts;
using Core.DomainModel;
using Microsoft.AspNetCore.Mvc;
using System;
using WebAPI.Models;

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

        protected TokenValidationResult CheckTokenValidation(string token)
        {
            var tokenValidationResult = new TokenValidationResult();
            Guid validToken;
            if (!string.IsNullOrEmpty(token) && Guid.TryParse(token, out validToken))
            {
                var validation = this._webServiceAssignmentService.GetValidationByToken(validToken);
                if (!validation.IsSuccessful)
                {
                    tokenValidationResult.SetError(validation.ExceptionContentResult);
                }
            }
            else
            {
                tokenValidationResult.SetError(GetError(Constant.InvalidWebServiceAssignmentToken));
            }
            return tokenValidationResult;
        }

        protected string GetError(string message)
        {
            string xml = string.Empty;
            xml += "<Error>" + "\n";
            xml += "<Message>" + "\n";
            xml += message + "\n";
            xml += "</Message>" + "\n";
            xml += "</Error>" + "\n";
            return xml;
        }

        #endregion /Methods

    }
}
