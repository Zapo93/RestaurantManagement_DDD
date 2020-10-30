using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Common.Models;
using RestaurantManagement.Domain.Serving.Events;
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
            items = new List<OrderItem>();
            kitchenRequests = new HashSet<KitchenRequest>();
            TableId = tableId;
            DateCreated = DateTime.UtcNow;
            AssigneeId = assigneeId;
            Open = true;
        }

        private Order() { }

        private List<OrderItem> items;
        public IReadOnlyCollection<OrderItem> Items => items.ToList().AsReadOnly();

        private readonly HashSet<KitchenRequest> kitchenRequests;

        public void ChangeRequestStatus(string requestId)
        {
            OrderStatusChangedEvent newEvent = new OrderStatusChangedEvent(
                "Request status changed",
                AssigneeId,
                requestId,
                TableId);

            RaiseEvent(newEvent);
        }

        public IReadOnlyCollection<KitchenRequest> KitchenRequests => kitchenRequests.ToList().AsReadOnly();

        public int? TableId { get; private set; }

        public DateTime DateCreated { get; private set; }
        public int AssigneeId { get; private set; }

        //TODO check if this is initialized correctly when taken from persisense
        public bool Open { get; private set; }

        public void Close() 
        {
            Open = false;
        }

        public void AddItems(IEnumerable<OrderItem> newItems)
        {
            if (Open)
            {
                ValidateOrderItems(newItems);
                items.AddRange(newItems);
                string requestId = GenerateKitchenRequestId();
                AddKitchenRequestById(requestId);
                GenerateAddedItemsEvent(requestId, newItems);
            }
            else
            {
                throw new OrderClosedException("Can not add items on closed order!");
            }
        }

        private void ValidateOrderItems(IEnumerable<OrderItem> newItems) 
        {
            foreach (var item in newItems) 
            {
                if (item.Dish == null)
                {
                    throw new InvalidDishException("Dish does not exist!");
                }
            }
        }

        private string GenerateKitchenRequestId() 
        {
            return new Guid().ToString().Substring(0, 8);
        }

        private void AddKitchenRequestById(string kitchenRequestId)
        {
            var newKitchenRequest = new KitchenRequest(kitchenRequestId);
            this.kitchenRequests.Add(newKitchenRequest);
        }

        private void GenerateAddedItemsEvent(string requestId, IEnumerable<OrderItem> newItems)
        {
            OrderItemsAddedEvent newEvent = new OrderItemsAddedEvent(Id,requestId);

            foreach (OrderItem item in newItems) 
            {
                newEvent.AddItem(item.Dish.RecipeId, item.Note); 
            }

            RaiseEvent(newEvent);
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
