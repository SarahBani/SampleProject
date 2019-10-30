using Authentication.WebAPIService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Test.IntegrationTest
{
    public class AuthWebAPIApplicationFactory : WebApplicationFactory<Startup>
    {

        #region Methods

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("IntegrationTest");
            base.ConfigureWebHost(builder);
        }

        #endregion /Methods

    }
}
