using Core.DomainService.Models;
using System.Threading.Tasks;

namespace Core.ApplicationService.Contracts
{
    public interface IAuthenticationService
    {

        Task<bool> IsAuthenticated(UserCredential  request);

        string GetAuthenticationToken(UserCredential  request);

    }
}
