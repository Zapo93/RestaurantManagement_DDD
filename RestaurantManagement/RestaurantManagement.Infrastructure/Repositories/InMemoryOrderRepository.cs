using RestaurantManagement.Application.Serving;
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

        public async Task Save(Order entity, CancellationToken cancellationToken)
        {
            OrderDataSet[entity.Id] = entity;
        }
    }
}
