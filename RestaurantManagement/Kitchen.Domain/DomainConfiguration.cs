using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Domain.Configuration;

namespace RestaurantManagement.Kitchen.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection AddKitchenDomain(this IServiceCollection services) 
            => services.AddCommonDomain();
    }
}
