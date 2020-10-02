using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Exceptions
{
    public class InvalidStatusException : BaseDomainException
    {
        public InvalidStatusException(string error)
        {
            this.Error = error;
        }
    }
}
