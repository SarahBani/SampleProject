using Authentication.Core.ApplicationService.Contracts;
using Core.DomainService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace Authentication.WebAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]

    public class AuthenticationController : ControllerBase
    {

        #region Properties

        private readonly IAuthService _authService;

        #endregion /Properties

        #region Constructors

        public AuthenticationController(IAuthService authService)
        {
            this._authService = authService;
        }

        #endregion /Constructors

        #region Actions

        [HttpPost, Route("request")]
        public async Task<IActionResult> RequestToken([FromBody] UserCredential request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transactionResult = await _authService.GetAuthenticationToken(request);
            if (transactionResult.IsSuccessful)
            {
                string token = transactionResult.Content.ToString();
                return Ok(token);
            }
            return BadRequest(transactionResult.ExceptionContentResult);
            //return Unauthorized();
            //return BadRequest(Constant.Exception_InvalidAuthentication);
        }

        #endregion /Actions

    }
}
