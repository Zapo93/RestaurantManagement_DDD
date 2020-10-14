using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Application.Common.Contracts;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddRepositories()
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
    }
}
