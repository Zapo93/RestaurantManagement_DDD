using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Hosting.Services;

namespace RestaurantManagement.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services) 
        {
            return services.AddFactories()
                .AddTransient<ITablesScheduleService,TablesScheduleService>();
        }

        private static IServiceCollection AddFactories(this IServiceCollection services)
            => services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IFactory<>)))
                    .AsMatchingInterface()
                    .WithTransientLifetime());
    }
}
