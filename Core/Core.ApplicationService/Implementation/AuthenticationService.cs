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
using Core.DomainService;
using Core.DomainModel.Entities;
using Microsoft.AspNetCore.Identity;
using Core.DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace Core.ApplicationService.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {

        #region Properties

        private readonly AuthenticationAppSettings _appSettings;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<Role> _roleManager;

        #endregion /Properties

        #region Constructors

        public AuthenticationService(IOptions<AuthenticationAppSettings> appSettings,
            UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this._appSettings = appSettings.Value;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        #endregion /Constructors

        #region Methods

        public async Task<TransactionResult> Register(User user, string password)
        {
            var result = await this._userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return new TransactionResult();
            }
            else
            {
                string errors = string.Empty;
                if (result.Errors.Count() > 0)
                {
                    foreach (var error in result.Errors)
                    {
                        errors += error.Description;
                    }
                    return new TransactionResult(new CustomException(errors));
                }
                return new TransactionResult(new CustomException(Constant.Exception_RegistrationFailed));
            }
        }

        public async Task<TransactionResult> Login(string userName, string password)
        {
            var user = await this._userManager.FindByNameAsync(userName);
            if (user != null)
            {
                bool isCorrect = await this._userManager.CheckPasswordAsync(user, password);
                if (isCorrect)
                {
                    return new TransactionResult();
                }
            }
            return new TransactionResult(new CustomException(Constant.Exception_LoginFailed));
        }

        public async Task<TransactionResult> ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var user = await this._userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var identityResult = await this._userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                if (identityResult.Succeeded)
                {
                    return new TransactionResult();
                }
            }
            return new TransactionResult(new CustomException(Constant.Exception_ChangePasswordFailed));
        }

        public async Task<TransactionResult> CreateRole(string name)
        {
            var role = new Role()
            {
                Name = name,
                NormalizedName = name.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Description = null
            };
            var result = await this._roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new TransactionResult();
            }
            else
            {
                return new TransactionResult(new CustomException(Constant.Exception_RoleCreationFailed));
            }
        }

        public async Task<bool> IsAuthenticated(UserCredential userCredential)
        {
            var result = await this.Login(userCredential.Username, userCredential.Password);
            return result.IsSuccessful;
        }

        public async Task<string> GetAuthenticationToken(UserCredential request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._appSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature); //  HmacSha256Signature);

            var user = await this._userManager.FindByNameAsync(request.Username);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = this._appSettings.Issuer,
                Audience = "crud",
                //Subject = new ClaimsIdentity(new Claim[]
                //{
                //    new Claim(ClaimTypes.Name, "crud")
                //}),
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = credentials
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        #endregion /Methods

    }
}
