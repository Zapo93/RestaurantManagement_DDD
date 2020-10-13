using RestaurantManagement.Application.Serving;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private static Dictionary<int, Order> OrderDataSet = new Dictionary<int, Order>();

        public Task<Order> GetOrderById(int orderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrders(Specification<Order> orderSpec, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task Save(Order entity, CancellationToken cancellationToken)
        {
            OrderDataSet[entity.Id] = entity;
        }
    }
}
