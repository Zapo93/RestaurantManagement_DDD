using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Hosting.Exceptions
{
    public class InvalidTableException: BaseDomainException
    {
        public InvalidTableException(string error) 
        {
            Error = error;
        }
    }
}
