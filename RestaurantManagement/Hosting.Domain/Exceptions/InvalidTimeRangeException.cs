using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Hosting.Domain.Exceptions
{
    public class InvalidTimeRangeException: BaseDomainException
    {
        public InvalidTimeRangeException(string error) 
        {
            this.Error = error;
        }
    }
}
