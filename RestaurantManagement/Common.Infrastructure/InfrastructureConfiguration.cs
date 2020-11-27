using GreenPipes;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Common.Application;
using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Common.Infrastructure.Events;
using RestaurantManagement.Common.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace RestaurantManagement.Common.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IServiceCollection AddCommonInfrastructure<TDbContext>(
            this IServiceCollection services,
            IConfiguration configuration)
                where TDbContext : DbContext
        {
            //If you dont use the MethodImpl attribute the result of GetCallingAssembly may be wrong
            Assembly callingAssembly = Assembly.GetCallingAssembly();

            return services
                    .AddRepositories(callingAssembly)
                    .AddDatabase<TDbContext>(configuration)
                    .AddTransient<IEventDispatcher, EventDispatcher>()
                    .AddMessaging(configuration);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IServiceCollection AddCommonInfrastructure<TDbContext>(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString,
            string secret)
                where TDbContext : DbContext
        {
            //If you dont use the MethodImpl attribute the result of GetCallingAssembly may be wrong
            Assembly callingAssembly = Assembly.GetCallingAssembly();

            return services
                    .AddRepositories(callingAssembly)
                    .AddDatabase<TDbContext>(dbConnectionString)
                    .AddTransient<IEventDispatcher, EventDispatcher>();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services, Assembly targetAssembly)
            => services
                .Scan(scan => scan
                    .FromAssemblies(targetAssembly)
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IRepository<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

        private static IServiceCollection AddDatabase<TDbContext>(
            this IServiceCollection services,
            IConfiguration configuration)
                where TDbContext : DbContext
            => services
                .AddDbContext<TDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(TDbContext).Assembly.FullName)));

        private static IServiceCollection AddDatabase<TDbContext>(//For Test Purposes
            this IServiceCollection services,
            string dbConnectionString)
                where TDbContext : DbContext
            => services
                .AddDbContext<TDbContext>(options => options
                    .UseSqlServer(
                        dbConnectionString,
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(TDbContext).Assembly.FullName)), ServiceLifetime.Transient); //,ServiceLifetime.Transient

        private static IServiceCollection AddMessaging(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddTransient<IPublisher, Publisher>();

            var messageQueueSettings = GetMessageQueueSettings(configuration);

            Type eventMessageConsumerType = typeof(EventMessageConsumer);

            services
                .AddMassTransit(mt =>
                {
                    mt.AddConsumer(eventMessageConsumerType);

                    mt.AddBus(context => Bus.Factory.CreateUsingRabbitMq(rmq =>
                    {
                        rmq.Host(messageQueueSettings.Host, host =>
                        {
                            host.Username(messageQueueSettings.UserName);
                            host.Password(messageQueueSettings.Password);
                        });

                        rmq.UseHealthCheck(context);

                        var queueName = GetQueueName(eventMessageConsumerType);
                        rmq.ReceiveEndpoint(queueName, endpoint =>
                        {
                            endpoint.PrefetchCount = 6;
                            endpoint.UseMessageRetry(retry => retry.Interval(5, 200));

                            endpoint.ConfigureConsumer(context, eventMessageConsumerType);
                        });
                    }));
                })
                .AddMassTransitHostedService();

            return services;
        }

        private static string GetQueueName(Type consumerType) 
        {
            var assemblyShortName = Assembly.GetEntryAssembly().FullName.Split(',')[0];
            return assemblyShortName + "." + consumerType.Name;
        }

        private static MessageQueueSettings GetMessageQueueSettings(IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(MessageQueueSettings));

            return new MessageQueueSettings(
                settings.GetValue<string>(nameof(MessageQueueSettings.Host)),
                settings.GetValue<string>(nameof(MessageQueueSettings.UserName)),
                settings.GetValue<string>(nameof(MessageQueueSettings.Password)));
        }
    }
}
