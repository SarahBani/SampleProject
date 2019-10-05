using Core.DomainModel;
using Core.DomainModel.Entities;
using Core.DomainService;
using Core.DomainService.Repositoy;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Core.ApplicationService.Implementation
{
    public abstract class BaseService<TRepository, TEntity, TKey> : BaseReadOnlyService<TRepository, TEntity, TKey>
        where TRepository : IRepository<TEntity, TKey>
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
            base.EntityService.UnitOfWork.BeginTransaction(GetCallerMethod());
            //}
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected async Task<TransactionResult> CommitTransactionAsync()
        {
            if (base.EntityService.UnitOfWork.GetTransactionName().Equals(GetCallerMethod()))
            {
                await base.EntityService.UnitOfWork.Commit();
            }
            return new TransactionResult();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCallerMethod()
        {
            int intSkipFrames = 0;
            while (new StackFrame(intSkipFrames).GetMethod().GetMethodImplementationFlags() == MethodImplAttributes.NoInlining ||
                   !new StackFrame(intSkipFrames).GetMethod().DeclaringType.BaseType.Name.Equals(typeof(BaseReadOnlyService<,,>).Name))
            {
                intSkipFrames++;
            }
            var method = new StackTrace(new StackFrame(intSkipFrames)).GetFrame(0).GetMethod();
            return method.ReflectedType.Name + "." + method.Name;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected TransactionResult GetTransactionException(Exception exception)
        {
            //if (base.EntityService.UnitOfWork.GetTransactionName().Equals(GetCallerMethod()))
            //{
            base.EntityService.UnitOfWork.RollBack();
            //Utility.SaveError(exception.GetBaseException());
            if (exception is CustomException)
            {
                return new TransactionResult(exception as CustomException);
            }
            else
            {
                return new TransactionResult(new CustomException(exception));
            }
            //}
            //else
            //{
            //    throw exception;
            //}
        }

        public virtual async Task<TransactionResult> InsertAsync(TEntity entity)
        {
            try
            {
                return await GetTransactionResultAsync(() => base.Repository.InsertAsync(entity.Trim<TEntity, TKey>()));
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
                return await GetTransactionResultAsync(() => base.Repository.Update(entity.Trim<TEntity, TKey>()));
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
                return await GetTransactionResultAsync(() => base.Repository.Delete(entity));
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
                return await GetTransactionResultAsync(() => base.Repository.Delete(id));
            }
            catch (Exception ex)
            {
                return GetTransactionException(ex);
            }
        }

        #endregion /Methods

    }
}