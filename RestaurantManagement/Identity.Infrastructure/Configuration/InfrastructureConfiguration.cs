using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.Common.Application;
using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Identity.Application;
using RestaurantManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using RestaurantManagement.Common.Infrastructure;
using RestaurantManagement.Identity.Infrastructure.Persistence;
using RestaurantManagement.Common.Infrastructure.Persistence;

namespace RestaurantManagement.Identity.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddIdentityInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                    .AddCommonInfrastructure<UsersDbContext>(configuration)
                    .AddTransient<IInitializer, DatabaseInitializer<UsersDbContext>>()
                    .AddIdentity(configuration);
        }

        public static IServiceCollection AddIdentityInfrastructure(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString,
            string secret)
        {
            return services
                    .AddCommonInfrastructure<UsersDbContext>(dbConnectionString, secret)
                    .AddTransient<IInitializer, DatabaseInitializer<UsersDbContext>>()
                    .AddIdentity(secret);
        }

        private static IServiceCollection AddIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var secret = configuration
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.Secret));

            SetUpIdentity(services, secret);

            return services;
        }

        private static IServiceCollection AddIdentity(
            this IServiceCollection services,
            string secret)
        {
            SetUpIdentity(services, secret);

            return services;
        }

        private static void SetUpIdentity(IServiceCollection services, string secret)
        {
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<UsersDbContext>();


            services.AddTransient<IIdentity, IdentityService>();
            services.AddTransient<IJwtTokenGenerator, JwtTokenGeneratorService>();
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
        }
    }
}