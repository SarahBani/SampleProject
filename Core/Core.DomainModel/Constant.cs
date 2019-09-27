namespace Core.DomainModel
{
    public static class Constant
    {

        #region AppSetting

        public const string AppSetting_DefaultConnection = "DefaultConnection";
        public const string AppSetting_WebSiteEmail = "WebSiteEmail";
        public const string AppSetting_WebSiteEmail_Name = "WebSiteEmail:Name";
        public const string AppSetting_WebSiteEmail_Address = "WebSiteEmail:Address";
        public const string AppSetting_ErrorLogFile = "ErrorLogFile";

        #endregion /AppSetting

        #region Validations

        public const string Invalid_Address_City_Required = "The address in city is required!";

        #endregion /Validations

        #region Exceptions

        public const string Exception_HasError = "An error has occured!";

        public const string Exception_NoActiveTransaction = "There is no active transation!";
        public const string Exception_NoFileSelected = "No file is chosen!";
        public const string Exception_SendEmailError = "There has been an error in sending the email!";
        public const string Exception_EmailAlreadyRegisteredError = "This email has already registered!";
        public const string Exception_EmailNotRegisteredError = "This email has not registered yet!";

        public const string Exception_sql_TimeoutExpired = "Database timeout has expired!";
        public const string Exception_sql_HasDepandantInfo = "The record has depandant information!";
        public const string Exception_sql_HasDuplicateInfo = "The record has duplicate information!";
        public const string Exception_sql_ArithmeticOverflow = "The record field value is too big!";

        public const string InvalidWebServiceAssignmentToken = "Token is invalid!";
        public const string WebServiceAssignmentExpired = "Your token has been expired!";

        #endregion /Exceptions

    }
}
