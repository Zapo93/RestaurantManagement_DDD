using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Common.Infrastructure.Events
{
    public class EventMessage
    {
        public Type EventType;
        public String EventPayload;
    }
}
