using RestaurantManagement.Application.Serving;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Serving.Models;
using RestaurantManagement.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Serving.Repositories
{
    internal class OrderRepository : DataRepository<IServingDbContext, Order>,
        IOrderRepository
    {
        public OrderRepository(IServingDbContext db) : base(db)
        {
        }

        public Task<Order> GetOrderById(int orderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByRequestId(string creatorReferenceId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrders(Specification<Order> orderSpec, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
