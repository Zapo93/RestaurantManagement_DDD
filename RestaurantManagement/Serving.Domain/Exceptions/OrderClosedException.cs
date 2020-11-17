using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Serving.Domain.Exceptions
{
    public class OrderClosedException: BaseDomainException
    {
        public readonly string Error;

        public OrderClosedException(string error) 
        {
            this.Error = error;
        }
    }
}
