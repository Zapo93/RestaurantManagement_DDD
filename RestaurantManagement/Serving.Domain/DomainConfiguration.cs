using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Domain.Configuration;

namespace RestaurantManagement.Serving.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection AddServingDomain(this IServiceCollection services) 
        {
            return services
                .AddCommonDomain();
        }
    }
}
