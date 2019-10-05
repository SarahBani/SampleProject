using Core.ApplicationService.Contracts;
using Core.DomainModel;
using Core.DomainService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace MicroService.AuthenticationService.Controllers
{
    [Authorize]
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
            _authService = authService;
        }

        #endregion /Constructors

        #region Actions

        [AllowAnonymous]
        [HttpPost, Route("request")]
        public async Task<IActionResult> RequestToken([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _authService.IsAuthenticated(request))
            {
                string token = _authService.GetAuthenticationToken(request);
                return Ok(token);
            }
            return BadRequest(Constant.InvalidAuthentication);
        }

        //    private async Task<string> GenerateJwtToken(User user)
        //    {
        //        var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //        new Claim(ClaimTypes.Name, user.UserName)
        //    };

        //        var roles = await _userManager.GetRolesAsync(user);

        //        foreach (var role in roles)
        //        {
        //            claims.Add(new Claim(ClaimTypes.Role, role));
        //        }

        //        var key = new SymmetricSecurityKey(Encoding.UTF8
        //            .GetBytes(_config.GetSection("AppSettings:Token").Value));

        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(claims),
        //            Expires = DateTime.Now.AddDays(1),
        //            SigningCredentials = creds
        //        };

        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        return tokenHandler.WriteToken(token);
        //    }
        //}

        #endregion /Actions

    }
}
