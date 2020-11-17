using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Serving.Application;
using RestaurantManagement.Common.Domain;
using RestaurantManagement.Common.Infrastructure.Persistence;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestaurantManagement.Serving.Infrastructure;

namespace RestaurantManagement.Serving.Infrastructure.Repositories
{
    internal class OrderRepository : DataRepository<IServingDbContext, Order>,
        IOrderRepository
    {
        public OrderRepository(IServingDbContext db) : base(db)
        {
        }

        public Task<Order> GetOrderById(int orderId, CancellationToken cancellationToken)
        {
            return this
                 .All()
                 .Include("Items.Dish")
                 .Include("KitchenRequests")
                 .FirstOrDefaultAsync(order => order.Id == orderId, cancellationToken);
        }

        public Task<Order> GetOrderByRequestId(string creatorReferenceId, CancellationToken cancellationToken)
        {
            return this
                 .All()
                 .Where(order => order.KitchenRequests.Any(kitchenRequest => kitchenRequest.RequestId == creatorReferenceId))
                 .Include("Items.Dish")
                 .Include("KitchenRequests")
                 .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetOrders(Specification<Order> orderSpec, CancellationToken cancellationToken)
        {
            return await this.Data.Orders
                .Where(orderSpec)
                .Include("Items.Dish")
                 .Include("KitchenRequests")
                .ToListAsync();
        }
    }
}
