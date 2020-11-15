using RestaurantManagement.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Common.Application.Contracts
{
    public interface IEventHandler<in TEvent>
       where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent);
    }
}
