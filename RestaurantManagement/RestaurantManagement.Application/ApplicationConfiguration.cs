﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Application;
using RestaurantManagement.Identity.Application;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RestaurantManagement.Application
{

    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddCommonApplication(configuration)
                .AddIdentityApplication(configuration);
        }

        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            return services
                .AddCommonApplication()
                .AddIdentityApplication();
        }
    }
}
