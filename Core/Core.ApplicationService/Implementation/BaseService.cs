using Core.DomainModel;
using Core.DomainModel.Entities;
using Core.DomainServices;
using Core.DomainServices.Repositoy;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Core.ApplicationService.Implementation
{
    public abstract class BaseService<TEntity, TKey> : BaseReadOnlyService<TEntity, TKey>
         where TEntity : Entity<TKey>
    {

        #region Properties

        protected string EntityTypeName
        {
            get
            {
                return this.GetType().Name.Replace("Service", "");
            }
        }

        protected string MethodName
        {
            get
            {
                StackFrame frame = new StackTrace().GetFrame(1);
                return frame.GetMethod().Name;
            }
        }
        protected new IRepository<TEntity, TKey> Repository
        {
            get
            {
                return base.Repository as IRepository<TEntity, TKey>;
            }
        }

        #endregion /Properties

        #region Constructors

        protected BaseService(IEntityService entityService)
            : base(entityService)
        {
        }

        #endregion /Constructors

        #region Methods
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected async Task<TransactionResult> GetTransactionResultAsync(Action action)
        {
            BeginTransaction();
            action();
            return await CommitTransactionAsync();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void BeginTransaction()
        {
            //if (string.IsNullOrEmpty(this.UnitOfWork.GetTransactionName()))
            //{            
            this.EntityService.UnitOfWork.BeginTransaction(GetCallerMethod());
            //}
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected async Task<TransactionResult> CommitTransactionAsync()
        {
            if (this.EntityService.UnitOfWork.GetTransactionName().Equals(GetCallerMethod()))
            {
                await this.EntityService.UnitOfWork.Commit();
            }
            return new TransactionResult();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCallerMethod()
        {
            int intSkipFrames = 0;
            while (new StackFrame(intSkipFrames).GetMethod().GetMethodImplementationFlags() == MethodImplAttributes.NoInlining ||
                   !new StackFrame(intSkipFrames).GetMethod().DeclaringType.BaseType.Name.Equals(typeof(BaseReadOnlyService<,>).Name))
            {
                intSkipFrames++;
            }
            var method = new StackTrace(new StackFrame(intSkipFrames)).GetFrame(0).GetMethod();
            return method.ReflectedType.Name + "." + method.Name;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected TransactionResult GetTransactionException(Exception exception)
        {
            if (this.EntityService.UnitOfWork.GetTransactionName().Equals(GetCallerMethod()))
            {
                this.EntityService.UnitOfWork.RollBack();
                //Utility.SaveError(exception.GetBaseException());
                if (exception is CustomException)
                {
                    return new TransactionResult(exception as CustomException);
                }
                else
                {
                    return new TransactionResult(new CustomException(exception));
                }
            }
            else
            {
                throw exception;
            }
        }

        public virtual async Task<TransactionResult> InsertAsync(TEntity entity)
        {
            try
            {
                return await GetTransactionResultAsync(() => 
                    this.Repository.InsertAsync(entity.Trim<TEntity, TKey>()));
            }
            catch (Exception ex)
            {
                return GetTransactionException(ex);
            }
        }

        public virtual async Task<TransactionResult> UpdateAsync(TEntity entity)
        {
            try
            {
                return await GetTransactionResultAsync(() => 
                    this.Repository.Update(entity.Trim<TEntity, TKey>()));
            }
            catch (Exception ex)
            {
                return GetTransactionException(ex);
            }
        }

        public virtual async Task<TransactionResult> DeleteAsync(TEntity entity)
        {
            try
            {
                return await GetTransactionResultAsync(() => this.Repository.Delete(entity));
            }
            catch (Exception ex)
            {
                return GetTransactionException(ex);
            }
        }

        public virtual async Task<TransactionResult> DeleteAsync(TKey id)
        {
            try
            {
                return await GetTransactionResultAsync(() => this.Repository.Delete(id));
            }
            catch (Exception ex)
            {
                return GetTransactionException(ex);
            }
        }

        #endregion /Methods

    }
}