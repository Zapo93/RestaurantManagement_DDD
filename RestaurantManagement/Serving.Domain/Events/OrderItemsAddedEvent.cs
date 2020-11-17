using RestaurantManagement.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Serving.Domain.Events
{
    public class OrderItemsAddedEvent: IDomainEvent
    {
        public int OrderId;
        public string CreatorReferenceId;
        public IReadOnlyCollection<AddedOrderItem> Items => items.AsReadOnly();
        private List<AddedOrderItem> items;

        public OrderItemsAddedEvent(int orderId, string creatorId) 
        {
            OrderId = orderId;
            CreatorReferenceId = creatorId;
            items = new List<AddedOrderItem>();
        }

        internal void AddItem(int recipeId, string? note) 
        {
            items.Add(new AddedOrderItem(recipeId,note));
        }
    }
}
