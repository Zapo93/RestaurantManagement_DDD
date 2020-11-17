using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.Common.Application;
using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Identity.Application;
using RestaurantManagement.Infrastructure.Common;
using RestaurantManagement.Infrastructure.Common.Persistence;
using RestaurantManagement.Infrastructure.Hosting;
using RestaurantManagement.Common.Infrastructure;
using RestaurantManagement.Common.Infrastructure.Persistence;
using RestaurantManagement.Identity.Infrastructure;

namespace RestaurantManagement.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            return services
                    .AddCommonInfrastructure<RestaurantManagementDbContext>(configuration)
                    .AddIdentityInfrastructure(configuration)
                    .AddDatabase(configuration);
        }

        public static IServiceCollection AddInfrastructure(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString,
            string secret)
        {
            return services
                    .AddCommonInfrastructure<RestaurantManagementDbContext>(dbConnectionString, secret)
                    .AddIdentityInfrastructure(dbConnectionString, secret)
                    .AddDatabase(dbConnectionString);
        }

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<IHostingDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())
                .AddTransient<IInitializer, DatabaseInitializer<RestaurantManagementDbContext>>();

        private static IServiceCollection AddDatabase(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString)
            => services
                .AddTransient<IHostingDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())//Must be transient
                .AddTransient<IInitializer, DatabaseInitializer<RestaurantManagementDbContext>>();
    }
}
