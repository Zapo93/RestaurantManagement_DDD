using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Domain.Configuration;
using RestaurantManagement.Hosting.Domain.Services;

namespace RestaurantManagement.Hosting.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection AddHostingDomain(this IServiceCollection services) 
        {
            return services
                .AddCommonDomain()
                .AddTransient<ITablesScheduleService,TablesScheduleService>();
        }
    }
}
