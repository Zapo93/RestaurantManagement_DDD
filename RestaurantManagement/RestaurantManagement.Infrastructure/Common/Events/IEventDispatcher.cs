using RestaurantManagement.Domain.Common;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Common
{
    internal interface IEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
