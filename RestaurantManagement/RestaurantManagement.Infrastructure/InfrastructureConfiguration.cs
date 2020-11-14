using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.Common.Contracts;
using RestaurantManagement.Application.Identity;
using RestaurantManagement.Infrastructure.Common;
using RestaurantManagement.Infrastructure.Common.Persistence;
using RestaurantManagement.Infrastructure.Hosting;
using RestaurantManagement.Infrastructure.Identity;
using RestaurantManagement.Infrastructure.Kitchen;
using RestaurantManagement.Infrastructure.Serving;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            return services
                    .AddRepositories()
                    .AddDatabase(configuration)
                    .AddIdentity(configuration)
                    .AddTransient<IEventDispatcher, EventDispatcher>();
        }

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string dbConnectionString,
            string secret)
        {
            return services
                    .AddRepositories()
                    .AddDatabase(dbConnectionString)
                    .AddIdentity(secret)
                    .AddTransient<IEventDispatcher, EventDispatcher>();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IRepository<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<RestaurantManagementDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(RestaurantManagementDbContext).Assembly.FullName)))
                .AddScoped<IKitchenDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())
                .AddScoped<IServingDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())
                .AddScoped<IHostingDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())
                .AddTransient<IInitializer, DatabaseInitializer>();

        private static IServiceCollection AddDatabase(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString)
            => services
                .AddDbContext<RestaurantManagementDbContext>(options => options
                    .UseSqlServer(
                        dbConnectionString,
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(RestaurantManagementDbContext).Assembly.FullName)), ServiceLifetime.Transient) //,ServiceLifetime.Transient
                .AddScoped<IKitchenDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())
                .AddScoped<IServingDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())
                .AddTransient<IHostingDbContext>(provider => provider.GetService<RestaurantManagementDbContext>())//Must be transient
                .AddTransient<IInitializer, DatabaseInitializer>();

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
                .AddEntityFrameworkStores<RestaurantManagementDbContext>();

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

            services.AddTransient<IIdentity, IdentityService>();
            services.AddTransient<IJwtTokenGenerator, JwtTokenGeneratorService>();
        }
    }
}
