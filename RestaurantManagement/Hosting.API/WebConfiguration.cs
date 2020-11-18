using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Common.Web;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RestaurantManagement.Hosting.Web
{
    public static class WebConfiguration
    {
        public static IServiceCollection AddHostingWebComponents(this IServiceCollection services) 
        {
            services
                .AddCommonWebComponents();

            return services;
        }
    }
}
