using Authentication.Core.ApplicationService.Contracts;
using Authentication.Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Authentication.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Authentication.Core.DomainService;
using Microsoft.AspNetCore.Http;

namespace Authentication.DependencyInjector
{
    public static class StartupExtension
    {
        public static IServiceCollection SetInjection(this IServiceCollection services)
        {
            /// custom user & Role with int key
            services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<SampleDataBaseContext>()
                .AddDefaultTokenProviders();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

    }
}
