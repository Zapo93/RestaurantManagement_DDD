using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Application;
using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Common.Infrastructure;
using RestaurantManagement.Common.Infrastructure.Persistence;
using RestaurantManagement.Hosting.Infrastructure.Persistence;

namespace RestaurantManagement.Hosting.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddHostingInfrastructure(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            return services
                    .AddCommonInfrastructure<HostingDbContext>(configuration)
                    .AddDatabase(configuration);
        }

        public static IServiceCollection AddHostingInfrastructure(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString,
            string secret)
        {
            return services
                    .AddCommonInfrastructure<HostingDbContext>(dbConnectionString, secret)
                    .AddDatabase(dbConnectionString);
        }

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<IHostingDbContext>(provider => provider.GetService<HostingDbContext>())
                .AddTransient<IInitializer, DatabaseInitializer<HostingDbContext>>();

        private static IServiceCollection AddDatabase(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString)
            => services
                .AddTransient<IHostingDbContext>(provider => provider.GetService<HostingDbContext>())//Must be transient
                .AddTransient<IInitializer, DatabaseInitializer<HostingDbContext>>();
    }
}
