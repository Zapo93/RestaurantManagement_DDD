using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Application.Common.Contracts;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Infrastructure.Common;
using RestaurantManagement.Infrastructure.Common.Persistence;
using RestaurantManagement.Infrastructure.Hosting;
using RestaurantManagement.Infrastructure.Kitchen;
using RestaurantManagement.Infrastructure.Serving;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            return services
                    .AddRepositories()
                    .AddDatabase(configuration)
                    .AddTransient<IEventDispatcher, EventDispatcher>();
        }

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string dbConnectionString)
        {
            return services
                    .AddRepositories()
                    .AddDatabase(dbConnectionString)
                    .AddTransient<IEventDispatcher, EventDispatcher>();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IRepository<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<RestaurantManagementDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(RestaurantManagementDbContext).Assembly.FullName)))
                .AddScoped<IKitchenDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())
                .AddScoped<IServingDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())
                .AddScoped<IHostingDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())
                .AddTransient<IInitializer, DatabaseInitializer>();

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            string dbConnectionString)
            => services
                .AddDbContext<RestaurantManagementDbContext>(options => options
                    .UseSqlServer(
                        dbConnectionString,
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(RestaurantManagementDbContext).Assembly.FullName))) //,ServiceLifetime.Transient
                .AddScoped<IKitchenDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())
                .AddScoped<IServingDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())
                .AddScoped<IHostingDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())
                .AddTransient<IInitializer, DatabaseInitializer>();

    }
}
