using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Common.Domain;
using RestaurantManagement.Common.Infrastructure.Events;

namespace RestaurantManagement.Common.Infrastructure
{
    internal class EventDispatcher : IEventDispatcher
    {
        private static readonly ConcurrentDictionary<Type, Type> HandlerTypesCache
            = new ConcurrentDictionary<Type, Type>();

        private static readonly ConcurrentDictionary<Type, Func<object, object, Task>> HandlersCache
            = new ConcurrentDictionary<Type, Func<object, object, Task>>();

        private static readonly Type HandlerType = typeof(IEventHandler<>);

        private static readonly MethodInfo MakeDelegateMethod = typeof(EventDispatcher)
            .GetMethod(nameof(MakeDelegate), BindingFlags.Static | BindingFlags.NonPublic)!;

        private static readonly Type EventHandlerFuncType = typeof(Func<Func<object, object, Task>>);

        private readonly IServiceProvider serviceProvider;
        private readonly IPublisher eventPublisher;

        public EventDispatcher(IServiceProvider serviceProvider, IPublisher eventPublisher)
        {
            this.serviceProvider = serviceProvider;
            this.eventPublisher = eventPublisher;
        }

        public async Task Dispatch(IDomainEvent domainEvent)
        {
            EventMessage message = new EventMessage();
            message.EventType = domainEvent.GetType();
            message.EventPayload = JsonConvert.SerializeObject(domainEvent);

            await eventPublisher.Publish(message);
        }

        public async Task Handle(EventMessage eventMessage)
        {
            IDomainEvent domainEvent = GetDomainEventFromEventMessage(eventMessage);

            var eventType = domainEvent.GetType();

            var handlerTypes = HandlerTypesCache.GetOrAdd(
                eventType,
                type => HandlerType.MakeGenericType(type));

            var eventHandlers = this.serviceProvider.GetServices(handlerTypes);

            foreach (var eventHandler in eventHandlers)
            {
                var handlerServiceType = eventHandler.GetType();

                var eventHandlerDelegate = HandlersCache.GetOrAdd(handlerServiceType, type =>
                {
                    var makeDelegate = MakeDelegateMethod
                        .MakeGenericMethod(eventType, type);

                    return ((Func<Func<object, object, Task>>)makeDelegate
                        .CreateDelegate(EventHandlerFuncType))
                        .Invoke();
                });

                await eventHandlerDelegate(domainEvent, eventHandler);
            }
        }

        private IDomainEvent GetDomainEventFromEventMessage(EventMessage eventMessage) 
        {
            IDomainEvent domainEvent = (IDomainEvent)JsonConvert.DeserializeObject(eventMessage.EventPayload, eventMessage.EventType);
            return domainEvent;
        }

        private static Func<object, object, Task> MakeDelegate<TEvent, TEventHandler>()
            where TEvent : IDomainEvent
            where TEventHandler : IEventHandler<TEvent>
            => (domainEvent, eventHandler) =>
                ((TEventHandler)eventHandler).Handle((TEvent)domainEvent);
    }
}
