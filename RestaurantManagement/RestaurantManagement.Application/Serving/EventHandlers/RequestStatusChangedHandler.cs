using RestaurantManagement.Application.Common.Contracts;
using RestaurantManagement.Domain.Kitchen.Events;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Serving.EventHandlers
{
    public class RequestStatusChangedHandler : IEventHandler<RequestStatusChangedEvent>
    {
        private readonly IOrderRepository OrderRepository;

        public RequestStatusChangedHandler(IOrderRepository orderRepository) 
        {
            this.OrderRepository = orderRepository;
        }

        public async Task Handle(RequestStatusChangedEvent domainEvent)
        {
            Order targetOrder = await OrderRepository.GetOrderByRequestId(domainEvent.CreatorReferenceId, CancellationToken.None);
            if (targetOrder != null)
            {
                targetOrder.ChangeRequestStatus(domainEvent.CreatorReferenceId);
                await OrderRepository.Save(targetOrder, CancellationToken.None); 
            }
        }
    }
}
