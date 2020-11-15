using RestaurantManagement.Common.Domain;
using System.Threading.Tasks;

namespace RestaurantManagement.Common.Infrastructure
{
    public interface IEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
