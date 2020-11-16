using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Application;
using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Common.Infrastructure;
using RestaurantManagement.Kitchen.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Kitchen.Infrastructure.Configuration
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddKitchenInfrastructure(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            return services
                    .AddCommonInfrastructure<KitchenDbContext>(configuration)
                    .AddDatabase(configuration);
        }

        public static IServiceCollection AddKitchenInfrastructure(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString,
            string secret)
        {
            return services
                    .AddCommonInfrastructure<KitchenDbContext>(dbConnectionString,secret)
                    .AddDatabase(dbConnectionString);
        }

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<IKitchenDbContext>(provider => provider.GetService<KitchenDbContext>())
                .AddTransient<IInitializer, DatabaseInitializer>();

        private static IServiceCollection AddDatabase(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString)
            => services
                .AddScoped<IKitchenDbContext>(provider => provider.GetService<KitchenDbContext>())
                .AddTransient<IInitializer, DatabaseInitializer>();
    }
}
