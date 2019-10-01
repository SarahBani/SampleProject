using Core.ApplicationService.Contracts;
using Core.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Principal;
using System.Threading;
using WebAPI.Models;

namespace WebAPI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {

        #region Constructors

        public TokenAuthorizeAttribute()
        {
        }

        #endregion /Constructors

        #region Methods

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            string token = filterContext.HttpContext.Request.Query["token"];
            if (!string.IsNullOrEmpty(token) && Guid.TryParse(token, out Guid validToken))
            {
                var webServiceAssignmentService = filterContext.HttpContext.RequestServices
                    .GetService(typeof(IWebServiceAssignmentService)) as IWebServiceAssignmentService;
                var validation = webServiceAssignmentService.GetValidationByToken(validToken);
                if (validation.IsSuccessful)
                {
                    // setting current principle  
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(token), null);
                    return;
                    //filterContext.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.OK);
                }
            }
            filterContext.Result = new CustomUnauthorizedResult(Constant.InvalidWebServiceAssignmentToken);
        }

        #endregion /Methods

    }
}
