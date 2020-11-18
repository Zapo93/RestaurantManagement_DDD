using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.Common.Application;
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

        public static IServiceCollection AddUserAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var secret = configuration
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.Secret));

            SetupUserAuthentication(services, secret);

            return services;
        }

        public static IServiceCollection AddUserAuthentication(
            this IServiceCollection services,
            string secret)
        {
            SetupUserAuthentication(services, secret);

            return services;
        }

        private static void SetupUserAuthentication(IServiceCollection services, string secret)
        {
            var key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //Since it is not added from AddIdentity it must be added manually
            //It might conflict with the one added by AddIdentity
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUserService>();
        }
    }
}
