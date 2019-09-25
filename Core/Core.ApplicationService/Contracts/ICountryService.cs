using Core.DomainModel.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.ApplicationService.Contracts
{
    public interface ICountryService
    {

        Task<Country> GetByIdAsync(short id);

        Task<IList<Country>> GetAllAsync();

    }
}
