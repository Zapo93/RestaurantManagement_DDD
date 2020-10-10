using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Serving.Exceptions;
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
            Open = true;
        }

        private HashSet<OrderItem> items;
        public IReadOnlyCollection<OrderItem> Items => items.ToList().AsReadOnly();

        private HashSet<int> kitchenRequestIds;
        public IReadOnlyCollection<int> KitchenRequestIds => kitchenRequestIds.ToList().AsReadOnly();

        public int? TableId { get; private set; }

        public DateTime DateCreated { get; private set; }
        public int AssigneeId { get; private set; }

        //TODO check if this is initialized correctly when taken from persisense
        public bool Open { get; private set; }

        public void Close() 
        {
            Open = false;
        }

        public void AddItem(Dish dish, string note)
        {
            OrderItem item = new OrderItem(dish, note);
            AddItem(item);
        }

        public void AddItem(OrderItem newItem)
        {
            if (Open)
            {
                items.Add(newItem);
            }
            else
            {
                throw new OrderClosedException("Can not add items on closed order!");
            }
        }

        public void AddKitchenRequestById(int kitchenRequestId) 
        {
            if (Open)
            {
                //TODO check if the id is unique
                kitchenRequestIds.Add(kitchenRequestId);
            }
            else
            {
                throw new OrderClosedException("Can not add requests on closed order!");
            }
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
