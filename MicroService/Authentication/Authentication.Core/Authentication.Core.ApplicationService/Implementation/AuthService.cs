using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using Authentication.Core.ApplicationService.Contracts;
using Authentication.Core.DomainService.Settings;
using Core.DomainService;
using Core.DomainModel;
using Core.DomainService.Models;
using Core.DomainModel.Entities;
using IUnitOfWork = Authentication.Core.DomainService.IUnitOfWork;

namespace Authentication.Core.ApplicationService.Implementation
{
    public class AuthService : BaseService, IAuthService
    {

        #region Properties

        private readonly AppSettings _appSettings;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<Role> _roleManager;

        #endregion /Properties

        #region Constructors

        public AuthService(IOptions<AppSettings> appSettings,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._appSettings = appSettings.Value;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        #endregion /Constructors

        #region Methods

        public async Task<TransactionResult> Register(User user, string password)
        {
            try
            {
                await BeginTransactionAsync();
                var identityResult = await this._userManager.CreateAsync(user, password);
                if (identityResult.Succeeded)
                {
                    await AddRole(user, "Member");
                }
                else
                {
                    string errors = string.Empty;
                    if (identityResult.Errors.Count() > 0)
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            errors += error.Description;
                        }
                        throw new CustomException(errors);
                    }
                    throw new CustomException(Constant.Exception_RegistrationFailed);
                }
                return CommitTransaction();
            }
            catch (Exception ex)
            {
                return GetTransactionException(ex);
            }
        }

        private async Task<TransactionResult> AddRole(User user, string role)
        {
            var result = await this._userManager.AddToRoleAsync(user, role);
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
                    throw new CustomException(errors);
                }
                throw new CustomException(Constant.Exception_RegistrationFailed);
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
