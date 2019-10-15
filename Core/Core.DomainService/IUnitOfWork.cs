using Core.DomainModel.Entities;
using System;
using System.Threading.Tasks;

namespace Core.DomainService
{
    public interface IUnitOfWork : IDisposable
    {

        string GetTransactionName();

        void BeginTransaction(string transactionName);

        bool HasTransaction();

        Task Commit();

        void RollBack();

    }
}
