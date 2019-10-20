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

        public const string Validation_RequiredField = "The {0} field is required!.";
        public const string Validation_StringLength_Max = "The {0} cannot be more than {1} characters long.!";
        public const string Validation_StringLength_Min = "The {0} must be at least {2} characters long.!";
        public const string Validation_StringLength_MinMax = "The {0} must be between {2} and {1} characters long.!";
        public const string Validation_StringLength = "The {0} must be at least {2} characters long.!";
        public const string Validation_RegularExpression = "The {0} is invalid!";
        public const string Validation_UrlRegularExpression = "The {0} contains invalid characters!";
        public const string Validation_Compare = "The {0} is not correct!";

        public const string Validation_Address_City_Required = "The address in city is required!";

        #endregion /Validations

        #region RegularExpressions

        public const string RegularExpression_Image = "^.+\\.(JPEG|jpeg|JPG|jpg|GIF|gif|BMP|bmp|PNG|png)$";
        public const string RegularExpression_Url = "[^-\\]~!*'();:@&=+$,/?%#[A-z0-9]";
        public const string RegularExpression_Email = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

        #endregion /RegularExpressions

        #region Exceptions

        public const string Exception_HasError = "An error has occured!";

        public const string Exception_NoActiveTransaction = "There is no active transation!";
        public const string Exception_RegistrationFailed = "An error occured in registeration!";
        public const string Exception_ChangePasswordFailed = "Changing password failed!";
        public const string Exception_LoginFailed = "Username or password is incorrect!";
        public const string Exception_RoleCreationFailed = "An error occured in creation role!";

        public const string Exception_NoFileSelected = "No file is chosen!";
        public const string Exception_SendEmailError = "There has been an error in sending the email!";
        public const string Exception_EmailAlreadyRegisteredError = "This email has already registered!";
        public const string Exception_EmailNotRegisteredError = "This email has not registered yet!";
        public const string Exception_PathNotFound = "The path could not be found!";
        public const string Exception_FailedAuthentication = "Authentication failed!";

        public const string Exception_sql_TimeoutExpired = "Database timeout has expired!";
        public const string Exception_sql_HasDepandantInfo = "The record has depandant information!";
        public const string Exception_sql_HasDuplicateInfo = "The record has duplicate information!";
        public const string Exception_sql_ArithmeticOverflow = "The record field value is too big!";

        public const string Exception_InvalidWebServiceAssignmentToken = "Token is invalid!";
        public const string Exception_WebServiceAssignmentExpired = "Your token has been expired!";
        public const string Exception_InvalidAuthentication = "The authentication is invalid!";

        #endregion /Exceptions        

    }
}
