using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Serving.Exceptions;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Serving.Factories
{
    public class OrderFactory : IOrderFactory
    {
        private int AssigneeId = default!;
        private int? TableId = null;

        private List<OrderItem> Items;
        private List<int> KitchenRequestIds;

        private bool IsAssigneeSet = false;

        public OrderFactory() 
        {
            Items = new List<OrderItem>();
            KitchenRequestIds = new List<int>();
        }

        public IOrderFactory WithAssignee(int assigneeId)
        {
            AssigneeId = assigneeId;
            IsAssigneeSet = true;

            return this;
        }

        public IOrderFactory WithItems(IEnumerable<OrderItem> newItems)
        {
            Items.AddRange(newItems);

            return this;
        }

        public IOrderFactory WithKitchenRequestId(int kitchenRequestId)
        {
            KitchenRequestIds.Add(kitchenRequestId);

            return this;
        }

        public IOrderFactory WithTableId(int? tableId)
        {
            TableId = tableId;

            return this;
        }

        public Order Build()
        {
            if (!IsAssigneeSet) 
            {
                throw new InvalidOrderException("Assignee must be set!");
            }

            Order newOrder = new Order(AssigneeId,TableId);

            newOrder.AddItems(Items);

            return newOrder;
        }
    }
}
