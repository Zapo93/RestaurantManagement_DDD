using RestaurantManagement.Common.Domain;
using RestaurantManagement.Common.Infrastructure.Events;
using System.Threading.Tasks;

namespace RestaurantManagement.Common.Infrastructure
{
    public interface IEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
        Task Handle(EventMessage eventMessage);
    }
}
