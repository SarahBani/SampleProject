using Authentication.Core.ApplicationService.Contracts;
using Authentication.WebAPIService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Authentication.WebAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {

        #region Properties

        private const string LocalLoginProvider = "Local";

        private readonly IAuthService _authService;

        #endregion /Properties

        #region Constructors

        public AccountController(IAuthService authService)
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

        // POST: api/Account/Login
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await this._authService.Login(model.UsertName, model.Password);
            if (result.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.ExceptionContentResult);
            }
        }

        // POST: api/Account/Register
        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
