using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Application;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RestaurantManagement.Kitchen.Application
{

    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddKitchenApplication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddCommonApplication(configuration);
                
        }

        public static IServiceCollection AddKitchenApplication(
            this IServiceCollection services)
        {
            return services
                .AddCommonApplication();
        }
    }
}
