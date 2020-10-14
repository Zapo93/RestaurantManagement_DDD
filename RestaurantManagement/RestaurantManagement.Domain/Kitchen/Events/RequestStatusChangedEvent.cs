using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Events
{
    public class RequestStatusChangedEvent: IDomainEvent
    {
        public string CreatorReferenceId { get; private set; }
        public int RequestId;

        public RequestStatusChangedEvent(string creatorReferenceId, int requestId) 
        {
            CreatorReferenceId = creatorReferenceId;
            RequestId = requestId;
        }
    }
}
