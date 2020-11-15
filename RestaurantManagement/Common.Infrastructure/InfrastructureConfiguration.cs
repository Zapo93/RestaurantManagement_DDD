using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Application;
using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Common.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace RestaurantManagement.Common.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IServiceCollection AddCommonInfrastructure<TDbContext>(
            this IServiceCollection services,
            IConfiguration configuration)
                where TDbContext : DbContext
        {
            //If you dont use the MethodImpl attribute the result of GetCallingAssembly may be wrong
            Assembly callingAssembly = Assembly.GetCallingAssembly();

            return services
                    .AddRepositories(callingAssembly)
                    .AddDatabase<TDbContext>(configuration)
                    .AddTransient<IEventDispatcher, EventDispatcher>();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IServiceCollection AddCommonInfrastructure<TDbContext>(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString,
            string secret)
                where TDbContext : DbContext
        {
            //If you dont use the MethodImpl attribute the result of GetCallingAssembly may be wrong
            Assembly callingAssembly = Assembly.GetCallingAssembly();

            return services
                    .AddRepositories(callingAssembly)
                    .AddDatabase<TDbContext>(dbConnectionString)
                    .AddTransient<IEventDispatcher, EventDispatcher>();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services, Assembly targetAssembly)
            => services
                .Scan(scan => scan
                    .FromAssemblies(targetAssembly)
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IRepository<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

        private static IServiceCollection AddDatabase<TDbContext>(
            this IServiceCollection services,
            IConfiguration configuration)
                where TDbContext : DbContext
            => services
                .AddDbContext<TDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(TDbContext).Assembly.FullName)));

        private static IServiceCollection AddDatabase<TDbContext>(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString)
                where TDbContext : DbContext
            => services
                .AddDbContext<TDbContext>(options => options
                    .UseSqlServer(
                        dbConnectionString,
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(TDbContext).Assembly.FullName)), ServiceLifetime.Transient); //,ServiceLifetime.Transient
    }
}
