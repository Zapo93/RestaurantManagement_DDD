using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Domain.Configuration;
using RestaurantManagement.Domain.Hosting.Services;
using RestaurantManagement.Kitchen.Domain;
using RestaurantManagement.Serving.Domain;

namespace RestaurantManagement.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services) 
        {
            return services
                .AddCommonDomain()
                .AddServingDomain()
                .AddTransient<ITablesScheduleService,TablesScheduleService>();
        }
    }
}
