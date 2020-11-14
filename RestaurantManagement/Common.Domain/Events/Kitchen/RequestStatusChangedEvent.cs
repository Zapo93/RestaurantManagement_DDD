using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Common.Domain.Events.Kitchen
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
