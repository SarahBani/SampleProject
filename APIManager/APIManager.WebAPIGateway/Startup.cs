using Core.DomainModel;
using Core.DomainService.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Text;
using System.Threading.Tasks;

namespace APIManager.WebAPIGateway
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
            SetAuthentication(services);
            services.AddOcelot(this.Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseOcelot().Wait();
        }

        private void SetAuthentication(IServiceCollection services)
        {
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection(Constant.AppSetting_AppSettings);
            services.Configure<APIGatewayAppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<APIGatewayAppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            var identityUrl = Configuration.GetValue<string>(Constant.AppSetting_IdentityUrl);
            string authenticationProviderKey = "IdentityApiKey";
            string[] audiences = appSettings.Audiences.Split(";");

            // configure jwt authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(authenticationProviderKey, options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = true,
                    ValidAudiences = audiences,
                    ValidateIssuer = true,
                    ValidIssuer = appSettings.Issuer,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = async ctx =>
                    {
                        int i = 0;
                    },
                    OnTokenValidated = context =>
                    {
                        var identity = context.Principal.Identity;
                        var user = context.Principal.Identity.Name;
                        //Grab the http context user and validate the things you need to
                        //if you are not satisfied with the validation, fail the request using the below commented code
                        context.Fail("Unauthorized");

                        //otherwise succeed the request
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = async ctx =>
                    {
                        int i = 0;
                    }
                };
            });
        }

        #endregion /Methods

    }
}
