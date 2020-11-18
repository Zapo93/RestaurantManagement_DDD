using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Hosting.Domain.Exceptions
{
    public class InvalidTableException: BaseDomainException
    {
        public InvalidTableException(string error) 
        {
            Error = error;
        }
    }
}
