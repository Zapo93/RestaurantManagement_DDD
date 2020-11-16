using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Common.Web.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RestaurantManagement.Common.Web
{
    public static class WebConfiguration
    {
        public static IServiceCollection AddCommonWebComponents(this IServiceCollection services) 
        {
            services
                .AddControllers()
                .AddNewtonsoftJson();
            return services;
        }

        public static IServiceCollection AddCurrentUserService(this IServiceCollection services)
            => services.AddScoped<ICurrentUser, CurrentUserService>();
    }
}
