using Core.DomainModel;
using Core.DomainService;
using Authentication.Core.DomainService;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IUnitOfWork = Authentication.Core.DomainService.IUnitOfWork;

namespace Authentication.Core.ApplicationService.Implementation
{
    public abstract class BaseService
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

        private IUnitOfWork _unitOfWork;

        #endregion /Properties

        #region Constructors

        protected BaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #endregion /Constructors

        #region Methods

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected async Task<TransactionResult> GetTransactionResultAsync(Action action)
        {
            await BeginTransactionAsync();
            action();
            return CommitTransaction();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected async Task BeginTransactionAsync()
        {
            if (!this._unitOfWork.HasTransaction())
            {
                await this._unitOfWork.BeginTransactionAsync(GetCallerMethod());
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected TransactionResult CommitTransaction(object content = null)
        {
            if (this._unitOfWork.GetTransactionName().Equals(GetCallerMethod()))
            {
                this._unitOfWork.Commit();
            }
            return new TransactionResult(content);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCallerMethod()
        {
            int intSkipFrames = 0;
            while (new StackFrame(intSkipFrames).GetMethod().GetMethodImplementationFlags() == MethodImplAttributes.NoInlining ||
                   new StackFrame(intSkipFrames).GetMethod().GetRealMethod() == null ||
                   !new StackFrame(intSkipFrames).GetMethod().GetBaseDeclaringType().IsAssignedGenericType(typeof(BaseService))) // This line is added for preventing mistakes in tests
            {
                intSkipFrames++;
            }
            var method = new StackTrace(new StackFrame(intSkipFrames)).GetFrame(0).GetMethod().GetRealMethod();
            return method.ReflectedType.Name + "." + method.Name;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected TransactionResult GetTransactionException(Exception exception)
        {
            //if (base.EntityService.UnitOfWork.GetTransactionName().Equals(GetCallerMethod()))
            //{
            this._unitOfWork.RollBack();
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

        #endregion /Methods

    }
}