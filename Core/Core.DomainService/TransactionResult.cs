using Core.DomainModel;

namespace Core.DomainServices
{
    public class TransactionResult
    {

        #region Properties

        public bool IsSuccessful { get; private set; }

        public string ExceptionContentResult { get; private set; }

        public ExceptionKey ExceptionKey { get; set; }

        #endregion /Properties

        #region Constructors

        public TransactionResult()
        {
            this.IsSuccessful = true;
            this.ExceptionContentResult = string.Empty;
        }

        public TransactionResult(CustomException exception)
        {
            this.IsSuccessful = false;
            this.ExceptionContentResult = exception.CustomMessage;
            this.ExceptionKey = exception.ExceptionKey;
        }

        #endregion /Constructors

    }
}