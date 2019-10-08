using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;
using Core.DomainModel.Entities;
using Core.DomainService.Settings;
using DependencyInversion.Injector;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MicroService.CRUDService
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

        // This method gets called by the runtime. Use this method to add services to the container.
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ConfigureAuthService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            // global cors policy
            app.UseCors(q => q.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureAuthService(IServiceCollection services)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var identityUrl = Configuration.GetValue<string>("IdentityUrl");

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AuthenticationAppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                //options.ClaimsIssuer
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = true,
                    ValidAudience = "crud",
                    ValidateIssuer = true,
                    ValidIssuer = appSettings.Issuer,
                };
                //options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
               //options.Audience = appSettings.Issuer;
            });
        }

        //private void SetAuthorization(IServiceCollection services)
        //{
        //    // configure strongly typed settings objects
        //    var appSettingsSection = Configuration.GetSection("AppSettings");
        //    services.Configure<AuthenticationAppSettings>(appSettingsSection);

        //    // configure jwt authentication
        //    var appSettings = appSettingsSection.Get<AuthenticationAppSettings>();
        //    var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
        //    services.AddAuthentication(x =>
        //    {
        //        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(x =>
        //    {
        //        x.Authority = identityUrl;
        //        x.RequireHttpsMetadata = false;
        //        x.Audience = "crud";
        //        x.SaveToken = true;
        //        x.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false
        //        };
        //    });
        //}

        #endregion /Methods

    }
}
