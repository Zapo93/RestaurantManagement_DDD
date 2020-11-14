using RestaurantManagement.Common.Domain;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Common
{
    internal interface IEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
