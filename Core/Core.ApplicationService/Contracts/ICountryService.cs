using Core.DomainModel.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.ApplicationService.Contracts
{
    public interface ICountryService
    {

        Task<Country> GetByIdAsync(short id, CancellationToken cancellationToken = default);

        IList<Country> GetAll();

    }
}
