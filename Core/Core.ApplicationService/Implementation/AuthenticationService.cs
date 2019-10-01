using Core.ApplicationService.Contracts;
using Core.DomainService;
using Core.DomainService.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationService.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {

        #region Properties

        private readonly AppSettings _appSettings;

        #endregion /Properties

        #region Constructors

        public AuthenticationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        #endregion /Constructors

        #region Methods

        public async Task<bool> IsAuthenticated(TokenRequest request)
        {
            //var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            //// return null if user not found
            //if (user == null)
            //    return null;
            return true;
        }

        public string GetAuthenticationToken(TokenRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "sdfsdf")// user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        #endregion /Methods

    }
}
