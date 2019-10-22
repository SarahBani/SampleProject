using System;
using System.Threading.Tasks;

namespace Authentication.Core.DomainService
{
    public interface IUnitOfWork : IDisposable
    {

        string GetTransactionName();

        Task BeginTransactionAsync(string transactionName);

        bool HasTransaction();

        void Commit();

        void RollBack();

    }
}
