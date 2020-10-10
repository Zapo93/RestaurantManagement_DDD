using Newtonsoft.Json.Serialization;
using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Serving.Exceptions
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
