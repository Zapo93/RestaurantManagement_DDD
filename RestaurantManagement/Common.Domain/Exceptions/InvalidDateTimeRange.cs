using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Common.Domain.Exceptions
{
    public class InvalidDateTimeRange: BaseDomainException
    {
        public InvalidDateTimeRange(string error) 
        {
            Error = error;
        }
    }
}
