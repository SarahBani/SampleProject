namespace Core.DomainService
{
    public class AppSettings
    {

        #region Properties

        public virtual string SecretKey { get; set; }

        #endregion /Properties

        #region Methods

        //public void Configure(IConfiguration config)
        //{
        //    this.SecretKey = Utility.GetApplicationSetting(config, "TokenSignature.SecretKey");
        //}

        #endregion /Methods

    }
}
