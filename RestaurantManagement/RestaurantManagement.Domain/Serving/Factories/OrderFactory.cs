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

        public IOrderFactory WithItem(Dish dish, string note)
        {
            OrderItem newItem = new OrderItem(dish,note);
            Items.Add(newItem);

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

            //TODO consider checking if there should be items and requestIds initially
            foreach (OrderItem item in Items) 
            {
                newOrder.AddItem(item);
            }

            foreach (int kitchenRequestId in KitchenRequestIds) 
            {
                newOrder.AddKitchenRequestById(kitchenRequestId);
            }

            return newOrder;
        }
    }
}
