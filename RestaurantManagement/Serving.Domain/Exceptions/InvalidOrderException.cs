using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Serving.Domain.Exceptions
{
    public class InvalidOrderException: BaseDomainException
    {
        public InvalidOrderException(string error) 
        {
            Error = error;
        }
    }
}
