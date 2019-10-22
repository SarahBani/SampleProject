namespace Authentication.Core.DomainService.Settings
{
    public class AppSettings
    {

        public virtual string SecretKey { get; set; }

        public virtual string Issuer { get; set; }

    }
}
