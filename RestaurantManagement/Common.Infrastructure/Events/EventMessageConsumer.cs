using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Common.Infrastructure.Events
{
    internal class EventMessageConsumer : IConsumer<EventMessage>
    {
        private readonly IEventDispatcher EventDispatcher;

        public EventMessageConsumer(IEventDispatcher eventDispatcher)
        {
            EventDispatcher = eventDispatcher;
        }

        public async Task Consume(ConsumeContext<EventMessage> context)
        {
            await EventDispatcher.Handle(context.Message);
        }
    }
}
