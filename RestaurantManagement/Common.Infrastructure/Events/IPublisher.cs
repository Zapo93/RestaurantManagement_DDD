using System;
using System.Threading.Tasks;

namespace RestaurantManagement.Common.Infrastructure.Events
{
    internal interface IPublisher
    {
        Task Publish<TMessage>(TMessage message);

        Task Publish<TMessage>(TMessage message, Type messageType);
    }
}
