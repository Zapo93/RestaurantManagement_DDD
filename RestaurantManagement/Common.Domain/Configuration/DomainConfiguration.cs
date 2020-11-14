using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace RestaurantManagement.Common.Domain.Configuration
{
    public static class DomainConfiguration
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IServiceCollection AddCommonDomain(this IServiceCollection services)
        {
            //If you dont use the MethodImpl attribute the result of GetCallingAssembly may be wrong
            Assembly callingAssembly = Assembly.GetCallingAssembly();

            return services.AddFactories(callingAssembly);
        }

        private static IServiceCollection AddFactories(this IServiceCollection services, Assembly targetAssembly)
        {
            return services
                    .Scan(scan => scan
                        .FromAssemblies(targetAssembly)
                        .AddClasses(classes => classes
                            .AssignableTo(typeof(IFactory<>)))
                        .AsMatchingInterface()
                        .WithTransientLifetime()); ;
        }
        
    }
}
