using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Serving.Application.Queries.GetOrders
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
