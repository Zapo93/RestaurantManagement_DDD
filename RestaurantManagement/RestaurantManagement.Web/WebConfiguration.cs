using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Application.Common.Contracts;
using RestaurantManagement.Web.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RestaurantManagement.Web
{
    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(this IServiceCollection services) 
        {
            services
                .AddScoped<ICurrentUser, CurrentUserService>()
                .AddControllers()
                .AddNewtonsoftJson();

            return services;
        }
    }
}
