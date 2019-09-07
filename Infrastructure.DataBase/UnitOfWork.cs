using Core.DomainModel;
using Core.DomainModel.Entities;
using Core.DomainServices;
using System;
using System.Threading.Tasks;

namespace Infrastructure.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Properties

        public string TransactionName { get; private set; }

        public SampleDataBaseContext  SampleDBContext { get; set; }

        #endregion /Properties

        #region Constructors

        public UnitOfWork(SampleDataBaseContext  dbContext)
        {
            this.SampleDBContext = dbContext;
        }

        #endregion /Constructors

        #region Destructors

        ~UnitOfWork()
        {
            Dispose();
        }

        #endregion /Destructors

        #region Methods

        public string GetTransactionName()
        {
            return this.TransactionName;
        }

        public void BeginTransaction(string transactionName)
        {
            if (string.IsNullOrEmpty(this.TransactionName))
            {
                this.TransactionName = transactionName;
            }
        }

        public async Task Commit()
        {
            if (string.IsNullOrEmpty(this.TransactionName))
            {
                throw new CustomException(ExceptionKey.NoActiveTransaction);
            }
            await this.SampleDBContext.SaveChangesAsync();
            this.TransactionName = string.Empty;
        }

        public void RollBack()
        {
        }

        public void Dispose()
        {
            if (this.SampleDBContext != null)
            {
                this.SampleDBContext.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        #endregion /Methods

    }
}
