﻿using Core.ApplicationService;
using Core.ApplicationService.Contracts;
using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainServices;
using Core.DomainServices.Repositoy;
using Infrastructure.DataBase;
using Infrastructure.DataBase.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Injector
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
            services.AddScoped<IReadOnlyRepository<WebServiceAssignment, short>, WebServiceAssignmentRepository>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IWebServiceAssignmentService, WebServiceAssignmentService>();

            // Can't do this for abstract classes
            //services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
            //services.AddScoped(typeof(IBaseService), typeof(BaseService<,>));

            return services;
        }

    }
}