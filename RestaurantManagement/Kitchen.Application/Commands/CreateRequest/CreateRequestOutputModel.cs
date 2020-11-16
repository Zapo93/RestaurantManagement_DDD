using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Kitchen.Application.Commands.CreateRequest
{
    public class CreateRequestOutputModel
    {
        public CreateRequestOutputModel(int requestId)
        {
            RequestId = requestId;
        }

        public int RequestId { get; }
    }
}
