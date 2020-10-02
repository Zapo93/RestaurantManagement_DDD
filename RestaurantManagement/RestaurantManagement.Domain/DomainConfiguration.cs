using Microsoft.Extensions.DependencyInjection;

namespace RestaurantManagement.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services) 
        {
            return services;
        }
    }
}
