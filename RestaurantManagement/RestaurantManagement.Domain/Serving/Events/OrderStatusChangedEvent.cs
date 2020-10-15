using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Serving.Events
{
    public class OrderStatusChangedEvent: IDomainEvent
    {
        public readonly string Message;
        public readonly int AssigneeID;
        public readonly string? RequestId;
        public readonly int? TableId;

        public OrderStatusChangedEvent(
            string message, 
            int assigneeID, 
            string? requestId, 
            int? tableId)
        {
            Message = message;
            AssigneeID = assigneeID;
            RequestId = requestId;
            TableId = tableId;
        }
    }
}
