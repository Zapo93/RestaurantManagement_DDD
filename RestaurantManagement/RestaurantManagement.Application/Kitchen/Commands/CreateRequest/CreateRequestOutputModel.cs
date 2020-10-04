using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Kitchen.Commands.CreateRequest
{
    public class CreateRequestOutputModel
    {
        public CreateRequestOutputModel(int requestId)
        {
            RequestId = requestId;
        }

        public object RequestId { get; }
    }
}
