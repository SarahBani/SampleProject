using Core.ApplicationService.Contracts;
using MicroService.AuthenticationService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroService.AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        #region Properties

        private const string LocalLoginProvider = "Local";

        private readonly IAuthenticationService _authService;

        #endregion /Properties

        #region Constructors

        public AccountController(IAuthenticationService authService)
        {
            this._authService = authService;
        }

        #endregion /Constructors

        #region Methods

        #endregion /Methods

        // GET: api/Account/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int userId)
        {
            return "value";
        }

        // POST: api/Account/Register
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await this._authService.Register(model.ConvertToUser(), model.Password);
            if (result.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.ExceptionContentResult);
            }
        }

        // PUT api/Account/ChangePassword
        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await this._authService.ChangePassword(model.UsertName, model.OldPassword, model.NewPassword);
            if (result.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.ExceptionContentResult);
            }
        }

    }
}
