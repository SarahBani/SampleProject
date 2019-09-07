using Core.DomainModel.Entities;
using System;
using System.Threading.Tasks;

namespace Core.DomainServices
{
    public interface IUnitOfWork : IDisposable
    {

        SampleDataBaseContext SampleDBContext { get; set; }

        string GetTransactionName();

        void BeginTransaction(string transactionName);

        Task Commit();

        void RollBack();

    }
}
