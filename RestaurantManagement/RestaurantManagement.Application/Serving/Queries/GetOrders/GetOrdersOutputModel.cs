using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Serving.Queries.GetOrders
{
    public class GetOrdersOutputModel
    {
        public readonly IEnumerable<Order> Orders;

        public GetOrdersOutputModel(IEnumerable<Order> orders) 
        {
            this.Orders = orders;
        }
    }
}
