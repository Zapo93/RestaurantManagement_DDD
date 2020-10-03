using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManagement.Domain.Serving.Models
{
    public class Order: Entity<int>, IAggregateRoot
    {
        internal Order(int assigneeId, int? tableId = null)
        {
            items = new HashSet<OrderItem>();
            kitchenRequestIds = new HashSet<int>();
            TableId = tableId;
            DateCreated = DateTime.UtcNow;
            AssigneeId = assigneeId;
        }

        private HashSet<OrderItem> items;
        public IReadOnlyCollection<OrderItem> Items => items.ToList().AsReadOnly();

        private HashSet<int> kitchenRequestIds;
        public IReadOnlyCollection<int> KitchenRequestIds => kitchenRequestIds.ToList().AsReadOnly();

        public int? TableId { get; private set; }

        public DateTime DateCreated { get; private set; }
        public int AssigneeId { get; private set; }

        public void AddItem(Dish dish, string note) 
        {
            OrderItem item = new OrderItem(dish, note);
            items.Add(item);
        }

        public void AddKitchenRequestById(int kitchenRequestId) 
        {
            kitchenRequestIds.Add(kitchenRequestId);
        }

        public Money TotalPrice { get {return GetTotalPrice(); } }

        public Money GetTotalPrice() 
        {
            Money totalPrice = new Money(0);
            foreach (OrderItem item in items) 
            {
                totalPrice.Add(item.Dish.Price);
            }

            return totalPrice;
        }
    }
}
