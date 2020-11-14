using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Domain.Configuration;
using RestaurantManagement.Domain.Hosting.Services;

namespace RestaurantManagement.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services) 
        {
            return services
                .AddCommonDomain()
                .AddTransient<ITablesScheduleService,TablesScheduleService>();
        }
    }
}
