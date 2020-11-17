using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Application;
using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Serving.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using RestaurantManagement.Common.Infrastructure;
using RestaurantManagement.Common.Infrastructure.Persistence;
using RestaurantManagement.Serving.Infrastructure.Persistence;

namespace RestaurantManagement.Serving.Infrastructure.Configuration
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddServingInfrastructure(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            return services
                    .AddCommonInfrastructure<ServingDbContext>(configuration)
                    .AddDatabase(configuration);
        }

        public static IServiceCollection AddServingInfrastructure(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString,
            string secret)
        {
            return services
                    .AddCommonInfrastructure<ServingDbContext>(dbConnectionString,secret)
                    .AddDatabase(dbConnectionString);
        }

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<IServingDbContext>(provider => provider.GetService<ServingDbContext>())
                .AddTransient<IInitializer, DatabaseInitializer<ServingDbContext>>();

        private static IServiceCollection AddDatabase(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString)
            => services
                .AddScoped<IServingDbContext>(provider => provider.GetService<ServingDbContext>())
                .AddTransient<IInitializer, DatabaseInitializer<ServingDbContext>>();
    }
}
