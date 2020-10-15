using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Application.Common.Contracts;
using RestaurantManagement.Application.Kitchen;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RestaurantManagement.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services.AddMediatR(Assembly.GetExecutingAssembly())
                .AddEventHandlers();
        }

        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            return services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        }
    }
}
