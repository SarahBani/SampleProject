using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.DomainModel.Entities
{

//    public class MyOwnUserStore : UserStore<User>
//    {
//        public MyOwnUserStore(SampleDataBaseContext context, IdentityErrorDescriber describer = null)
//            : base(context, describer)
//        {
//        }

//}
    //public class ApplicationUserStore : IUserStore<User>,
    //                     IUserClaimStore<User>,
    //                     IUserLoginStore<User>,
    //                     IUserRoleStore<User>,
    //                     IUserPasswordStore<User>,
    //                     IUserSecurityStampStore<User>
    //{


    //    private SampleDataBaseContext _dbContext;

    //    public ApplicationUserStore(SampleDataBaseContext dbContext)
    //    {
    //        this._dbContext = dbContext;
    //    }

    //    public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
