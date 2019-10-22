using Core.DomainModel.Entities;
using Core.DomainService;
using Core.DomainService.Models;
using System.Threading.Tasks;

namespace Authentication.Core.ApplicationService.Contracts
{
    public interface IAuthService
    {

        Task<TransactionResult> Register(User user, string password);

        Task<TransactionResult> Login(string userName, string password);

        Task<TransactionResult> ChangePassword(string userName, string oldPassword, string newPassword);

        Task<bool> IsAuthenticated(UserCredential request);

        Task<string> GetAuthenticationToken(UserCredential request);

    }
}
