using MediatR;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Serving.Commands.CloseOrder
{
    public class CloseOrderCommand: IRequest<Unit>
    {
        public int OrderId;

        public class CloseOrderCommandHandler : IRequestHandler<CloseOrderCommand, Unit>
        {
            private readonly IOrderRepository OrderRepository;

            public CloseOrderCommandHandler(IOrderRepository orderRepository) 
            {
                this.OrderRepository = orderRepository;
            }

            public async Task<Unit> Handle(CloseOrderCommand request, CancellationToken cancellationToken)
            {
                Order targetOrder = await OrderRepository.GetOrderById(request.OrderId,cancellationToken);

                targetOrder.Close();

                await OrderRepository.Save(targetOrder,cancellationToken);

                return new Unit();
            }
        }
    }
}
