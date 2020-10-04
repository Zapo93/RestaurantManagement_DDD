using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Serving.Commands.CreateOrder
{
    public class CreateOrderOutputModel
    {
        public CreateOrderOutputModel(int orderId)
        {
            OrderId = orderId;
        }

        public object OrderId { get; }
    }
}
