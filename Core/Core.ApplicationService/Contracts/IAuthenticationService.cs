using Core.DomainService.Models;
using System.Threading.Tasks;

namespace Core.ApplicationService.Contracts
{
    public interface IAuthenticationService
    {

        Task<bool> IsAuthenticated(TokenRequest request);

        string GetAuthenticationToken(TokenRequest request);

    }
}
