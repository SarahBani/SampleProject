using Core.ApplicationService.Contracts;
using Core.DomainService.Settings;
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

        private readonly AuthenticationAppSettings _appSettings;

        #endregion /Properties

        #region Constructors

        public AuthenticationService(IOptions<AuthenticationAppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._appSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = this._appSettings.Issuer,
                Audience = "crud",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "crud")// user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = credentials
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        #endregion /Methods

    }
}
