using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Application;
using RestaurantManagement.Common.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace RestaurantManagement.Common.Application
{
    public static class ApplicationConfiguration
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IServiceCollection AddCommonApplication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            //If you dont use the MethodImpl attribute the result of GetCallingAssembly may be wrong
            Assembly callingAssembly = Assembly.GetCallingAssembly();

            return services
                .Configure<ApplicationSettings>(
                    configuration.GetSection(nameof(ApplicationSettings)),
                    options => options.BindNonPublicProperties = true)
                .AddMediatR(callingAssembly)
                .AddEventHandlers(callingAssembly);
                
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IServiceCollection AddCommonApplication(
            this IServiceCollection services)
        {
            //If you dont use the MethodImpl attribute the result of GetCallingAssembly may be wrong
            Assembly callingAssembly = Assembly.GetCallingAssembly();

            return services
                .AddMediatR(callingAssembly)
                .AddEventHandlers(callingAssembly);
        }

        public static IServiceCollection AddEventHandlers(this IServiceCollection services, Assembly targetAssembly)
        {
            return services
                .Scan(scan => scan
                    .FromAssemblies(targetAssembly)
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        }
    }
}
