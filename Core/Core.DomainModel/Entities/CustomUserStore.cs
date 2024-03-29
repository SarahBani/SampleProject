﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Core.DomainModel.Entities
{

    public class CustomUserStore : IUserPasswordStore<User>, IUserEmailStore<User>, IUserRoleStore<User>, IUserClaimStore<User>
    {

        #region Properties

        private readonly SampleDataBaseContext _context;

        #endregion /Properties

        #region Constructors

        public CustomUserStore(SampleDataBaseContext context)
        {
            _context = context;
        }

        #endregion /Constructors

        #region Methods

        public void Dispose()
        {
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Add(user);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError() { Description = $"Could not create user {user.UserName}." });
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userFromDb = await _context.Users.FindAsync(user.Id);
            _context.Remove(userFromDb);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError() { Description = $"Could not delete user {user.UserName}." });
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _context.Users.SingleOrDefaultAsync(u => u.Id.Equals(Guid.Parse(userId)), cancellationToken);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _context.Users.SingleOrDefaultAsync(u => u.UserName.Equals(normalizedUserName.ToLower()), cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<object>(null);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.UserName = userName;
            return Task.FromResult<object>(null);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Update(user);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError() { Description = $"Could not update user {user.UserName}." });
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult<object>(null);
        }

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _context.Users.SingleOrDefaultAsync(u => u.UserName.Equals(normalizedEmail), cancellationToken);
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.UserName);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.UserName);
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.UserName = email;
            return Task.FromResult<object>(null);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<object>(null);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<object>(null);
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var role = await this._context.Roles.FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
            if (role == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            var userRole = new IdentityUserRole<int>()
            {
                UserId = user.Id,
                RoleId = role.Id
            };
            await this._context.AddAsync(userRole);
            await this._context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var role = this._context.Roles.FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
            if (role == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            var userRoles = await this._context.UserRoles.FirstOrDefaultAsync(q => q.UserId.Equals(user.Id) &&
                  q.RoleId.Equals(role.Id),
                  cancellationToken);
            this._context.Remove(userRoles);
            await this._context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await this._context.UserRoles.Where(q => q.UserId.Equals(user.Id))
                 .Join(_context.Roles,
                     userRole => userRole.RoleId,
                     role => role.Id,
                     (userRole, role) => new
                     {
                         Role = role
                     })
                 .Select(q => q.Role.Name)
                 .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var role = await this._context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            return await this._context.UserRoles.AnyAsync(q => q.UserId.Equals(user.Id) && q.RoleId.Equals(role.Id), cancellationToken);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            return await this._context.UserRoles
                 .Join(_context.Roles,
                     userRole => userRole.RoleId,
                     role => role.Id,
                     (userRole, role) => new
                     {
                         UserRole = userRole,
                         Role = role
                     })
                 .Join(_context.Users,
                     roleJoin => roleJoin.UserRole.UserId,
                     user => user.Id,
                     (roleJoin, user) => new
                     {
                         Role = roleJoin.Role,
                         User = user
                     })
                 .Where(q => q.Role.Name.Equals(roleName))
                 .Select(q => q.User)
                 .ToListAsync(cancellationToken);
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await this._context.UserClaims.Where(q => q.UserId.Equals(user.Id)).Select(q => q.ToClaim()).ToListAsync(cancellationToken);
        }

        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }
            var userClaims = new List<IdentityUserClaim<int>>();
            foreach (var claim in claims)
            {
                userClaims.Add(new IdentityUserClaim<int>()
                {
                    UserId = user.Id,
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                });
            }
            return Task.FromResult(this._context.UserClaims.AddRangeAsync(userClaims, cancellationToken));
        }

        public async Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            if (newClaim == null)
            {
                throw new ArgumentNullException(nameof(newClaim));
            }
            var userClaim = await this._context.UserClaims.SingleOrDefaultAsync(q => q.UserId == user.Id && q.ToClaim() == claim, cancellationToken);
            userClaim.ClaimType = newClaim.Type;
            userClaim.ClaimValue = newClaim.Value;
            this._context.UserClaims.Update(userClaim);
        }

        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }
            var userClaims = this._context.UserClaims.Where(q => q.UserId == user.Id && claims.Any(x => x == q.ToClaim()));
            this._context.UserClaims.RemoveRange(userClaims);
            return Task.FromResult<object>(null);
        }

        public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            return await this._context.UserClaims
                .Join(this._context.Users,
                      userClaim => userClaim.UserId,
                      user => user.Id,
                     (userClaim, user) => new
                     {
                         User = user,
                         UserClaim = userClaim
                     })
                .Where(q => q.UserClaim.ToClaim() == claim)
                .Select(q=>q.User)
                .ToListAsync(cancellationToken);
        }

        #endregion /Methods

    }
}
