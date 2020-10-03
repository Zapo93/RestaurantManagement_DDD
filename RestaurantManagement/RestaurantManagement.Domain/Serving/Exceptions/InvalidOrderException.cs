using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Serving.Exceptions
{
    public class InvalidOrderException: BaseDomainException
    {
        public InvalidOrderException(string error) 
        {
            Error = error;
        }
    }
}
