using System.IO;
using Core.DomainModel.Entities;
using Core.DomainService.Settings;
using DependencyInjection.Injector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroService.AuthenticationService
{
    public class Startup
    {

        #region Properties

        public IConfiguration Configuration { get; }

        #endregion /Properties

        #region Constructors

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        #endregion /Constructors

        #region Methods

        public void ConfigureServices(IServiceCollection services)
        {
            if (Configuration["Environment"] != "IntegrationTest")
            {
                string connectionString = Core.DomainService.Utility.GetConnectionString(this.Configuration);
                services.AddDbContext<SampleDataBaseContext>(options => options.UseSqlServer(connectionString));
            }
            else
            {
                var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                IConfigurationRoot config = builder.Build();
                string connectionString = Core.DomainService.Utility.GetConnectionString(config);
                services.AddDbContext<SampleDataBaseContext>(options => options.UseSqlServer(connectionString));
            }
            services.SetInjection();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AuthenticationAppSettings>(appSettingsSection);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        #endregion /Methods

    }
}
