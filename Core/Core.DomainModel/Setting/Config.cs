using Microsoft.Extensions.Configuration;

namespace Core.DomainModel.Settings
{
    public class Config
    {

        #region Properties

        protected IConfiguration Configuration { get; set; }

        #endregion /Properties

        #region Constructors

        public Config(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        #endregion /Constructors

        #region Methods

        public T GetApplicationSettingSecion<T>() where T : class, ISetting
        {
            return this.Configuration.GetSection(typeof(T).Name).Get<T>();
        }

        public string GetApplicationSetting(string key)
        {
            return this.Configuration.GetSection(key).Value;
        }

        #endregion /Methods

    }
}
