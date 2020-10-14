using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Common.Contracts
{
    public interface IEventHandler<in TEvent>
       where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent);
    }
}
