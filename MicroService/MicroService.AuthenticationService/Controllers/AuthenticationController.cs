using Core.ApplicationService.Contracts;
using Core.DomainModel;
using Core.DomainService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace MicroService.AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]

    public class AuthenticationController : ControllerBase
    {

        #region Properties

        private readonly IAuthenticationService _authService;

        #endregion /Properties

        #region Constructors

        public AuthenticationController(IAuthenticationService authService)
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
            if (await _authService.IsAuthenticated(request))
            {
                string token = await _authService.GetAuthenticationToken(request);
                return Ok(token);
            }
            return BadRequest(Constant.Exception_InvalidAuthentication);
        }

        #endregion /Actions

    }
}
