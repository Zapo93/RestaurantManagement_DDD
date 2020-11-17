using RestaurantManagement.Common.Domain;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Serving.Domain.Factories
{
    public interface IOrderFactory: IFactory<Order>
    {
        IOrderFactory WithAssignee(string assigneeId);
        IOrderFactory WithTableId(int? tableId);
        IOrderFactory WithItems(IEnumerable<OrderItem> items);
    }
}
