using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
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
            if (!this.Configuration["Environment"].Equals("IntegrationTest"))
            {
                string connectionString = Core.DomainService.Utility.GetConnectionString(this.Configuration);
                services.AddDbContext<SampleDataBaseContext>(options => options.UseSqlServer(connectionString));
            }
            else
            {
                /// we can change the default connection string or make it in-memory
                services.AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<SampleDataBaseContext>((provider, options) =>
                    {
                        options.UseInMemoryDatabase("InMemory").UseInternalServiceProvider(provider);
                    });

                //var builder = new ConfigurationBuilder()
                //  .SetBasePath(Directory.GetCurrentDirectory())
                //     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                //IConfigurationRoot config = builder.Build();
                //string connectionString = Core.DomainService.Utility.GetConnectionString(config);
                //services.AddDbContext<SampleDataBaseContext>(options => options.UseSqlServer(connectionString));
            }
            services.SetInjection();
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressConsumesConstraintForFormFileParameters = true;
                    options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressMapClientErrors = true;
                });

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
            var appSettingsSection = Configuration.GetSection(Constant.AppSetting_AppSettings);
            var appSettings = appSettingsSection.Get<APIAppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            // configure jwt authentication
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
                    ValidAudience = appSettings.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = appSettings.Issuer,
                };
                options.RequireHttpsMetadata = false;
                options.Audience = appSettings.Issuer;
                options.SaveToken = true;
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = async ctx =>
                    {
                        int i = 0;
                    },
                    OnTokenValidated = context =>
                    {
                        System.Console.WriteLine("ddddddddddddddddddddddd");
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
