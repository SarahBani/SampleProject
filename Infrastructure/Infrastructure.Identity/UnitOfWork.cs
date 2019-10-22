using Core.DomainModel;
using Core.DomainModel.Entities;
using Core.DomainService.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Properties

        private string _transactionName;

        private SampleDataBaseContext _dbContext;

        private IDbContextTransaction _transaction;

        #endregion /Properties

        #region Constructors

        public UnitOfWork(SampleDataBaseContext dbContext)
        {
            this._dbContext = dbContext;
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
            return this._transactionName;
        }

        public bool HasTransaction()
        {
            return !string.IsNullOrEmpty(this._transactionName);
        }

        public async Task BeginTransactionAsync(string transactionName)
        {
            if (string.IsNullOrEmpty(this._transactionName))
            {
                this._transactionName = transactionName;
                this._transaction = await this._dbContext.Database.BeginTransactionAsync();
            }
        }

        public void Commit()
        {
            if (string.IsNullOrEmpty(this._transactionName))
            {
                throw new CustomException(ExceptionKey.NoActiveTransaction);
            }
            this._transaction.Commit();
            this._transactionName = string.Empty;
        }

        public void RollBack()
        {
            if (string.IsNullOrEmpty(this._transactionName))
            {
                throw new CustomException(ExceptionKey.NoActiveTransaction);
            }
            this._transaction.Rollback();
            this._transactionName = string.Empty;
        }

        public void Dispose()
        {
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
            }
            if (this._transaction != null)
            {
                this._transaction.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        #endregion /Methods

    }
}
