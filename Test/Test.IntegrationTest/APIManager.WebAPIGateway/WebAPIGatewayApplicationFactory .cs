using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using APIManager.WebAPIGateway;

namespace Test.IntegrationTest
{
    public class WebAPIGatewayApplicationFactory : WebApplicationFactory<Startup>
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
