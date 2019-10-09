using Core.ApplicationService;
using Core.ApplicationService.Contracts;
using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainService;
using Core.DomainService.Repository;
using Infrastructure.DataBase;
using Infrastructure.DataBase.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInversion.Injector
{
    public static class StartupExtension
    {
        public static IServiceCollection SetInjection(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEntityService, EntityService>();

            services.AddScoped<IReadOnlyRepository<Country, short>, CountryRepository>();
            services.AddScoped<IRepository<Bank, int>, BankRepository>();
            services.AddScoped<IRepository<Branch, int>, BranchRepository>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IBranchService, BranchService>();

            // Can't do this for abstract classes
            //services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
            //services.AddScoped(typeof(IBaseService), typeof(BaseService<,>));

            return services;
        }

    }
}
