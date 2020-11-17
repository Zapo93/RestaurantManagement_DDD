using MediatR;
using RestaurantManagement.Serving.Application.Commands.Common;
using RestaurantManagement.Serving.Application.Commands.CreateOrder;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Serving.Application.Commands.AddItemsToOrder
{
    public class AddItemsToOrderCommand : IRequest<Unit>
    {
        public int OrderId;

        public List<OrderItemInputModel> Items;

        public AddItemsToOrderCommand()
        {
            Items = new List<OrderItemInputModel>();
        }

        public class AddItemsToOrderCommandHandler : IRequestHandler<AddItemsToOrderCommand, Unit>
        {
            private readonly IOrderRepository OrderRepository;
            private readonly IDishRepository DishRepository;

            public AddItemsToOrderCommandHandler(
                IOrderRepository orderRepository,
                IDishRepository dishRepository)
            {
                OrderRepository = orderRepository;
                DishRepository = dishRepository;
            }

            public async Task<Unit> Handle(AddItemsToOrderCommand command, CancellationToken cancellationToken)
            {
                Order targetOrder = await OrderRepository.GetOrderById(command.OrderId, cancellationToken);

                List<OrderItem> orderItems = new List<OrderItem>();

                foreach (var item in command.Items)
                {
                    Dish dish = await DishRepository.GetDishById(item.DishId, cancellationToken);
                    orderItems.Add(new OrderItem(dish, item.Note));
                }

                targetOrder.AddItems(orderItems);

                await OrderRepository.Save(targetOrder, cancellationToken);

                return new Unit();
            }
        }
    } 
}
