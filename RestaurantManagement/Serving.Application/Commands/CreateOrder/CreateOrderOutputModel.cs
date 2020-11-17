using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Serving.Application.Commands.CreateOrder
{
    public class CreateOrderOutputModel
    {
        public CreateOrderOutputModel(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}
