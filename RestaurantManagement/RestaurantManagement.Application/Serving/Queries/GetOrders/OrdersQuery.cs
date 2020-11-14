using MediatR;
using RestaurantManagement.Common.Domain;
using RestaurantManagement.Domain.Kitchen.Models;
using RestaurantManagement.Domain.Serving.Models;
using RestaurantManagement.Domain.Serving.Specificaitons.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Serving.Queries.GetOrders
{
    public class OrdersQuery: IRequest<GetOrdersOutputModel>
    {
        public bool OnlyOpen = false;
        public string? AssigneeId = null;
        public int? TableId = null;

        public class OrdersQueryHandler : IRequestHandler<OrdersQuery, GetOrdersOutputModel>
        {
            private readonly IOrderRepository OrderRepository;

            public OrdersQueryHandler(IOrderRepository orderRepository) 
            {
                this.OrderRepository = orderRepository;
            }

            public async Task<GetOrdersOutputModel> Handle(OrdersQuery query, CancellationToken cancellationToken)
            {
                Specification<Order> orderSpec = GetOrderSpecification(query);

                IEnumerable<Order> orders = await OrderRepository.GetOrders(orderSpec,cancellationToken);

                return new GetOrdersOutputModel(orders);
            }

            private Specification<Order> GetOrderSpecification(OrdersQuery query)
            {
                return new OnlyOpenOrdersSpecification(query.OnlyOpen)
                    .And(new OrdersByAssigneeSpecification(query.AssigneeId))
                    .And(new OrdersByTableSpecification(query.TableId));
            }
        }
    }
}
