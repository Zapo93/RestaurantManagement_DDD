using RestaurantManagement.Common.Domain;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Serving.Factories
{
    public interface IOrderFactory: IFactory<Order>
    {
        IOrderFactory WithAssignee(string assigneeId);
        IOrderFactory WithTableId(int? tableId);
        IOrderFactory WithItems(IEnumerable<OrderItem> items);
    }
}
