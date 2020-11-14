using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Exceptions
{
    public class InvalidRequestException: BaseDomainException
    {
        public InvalidRequestException(string error) 
        {
            Error = error;
        }
    }
}
