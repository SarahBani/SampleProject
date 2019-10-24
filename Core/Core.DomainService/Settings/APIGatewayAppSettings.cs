namespace Core.DomainService.Settings
{
    public class APIGatewayAppSettings
    {

        #region Properties

        public virtual string SecretKey { get; set; }

        public virtual string Issuer { get; set; }

        public virtual string Audiences { get; set; }

        #endregion /Properties

    }
}
