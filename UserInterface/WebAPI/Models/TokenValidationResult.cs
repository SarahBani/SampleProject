namespace WebAPI.Models
{
    public class TokenValidationResult
    {

        #region Properties

        public string ErrorMessage { get; private set; }

        public bool IsSuccessful { get; private set; }

        #endregion /Properties

        #region Constructors

        public TokenValidationResult()
        {
            this.IsSuccessful = true;
        }

        #endregion /Constructors

        #region Methods

        public void SetError(string message)
        {
            this.IsSuccessful = false;
            this.ErrorMessage = message;
        }

        #endregion /Methods

    }
}
