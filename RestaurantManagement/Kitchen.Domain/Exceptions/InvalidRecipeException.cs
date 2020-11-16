using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Kitchen.Domain.Exceptions
{
    public class InvalidRecipeException: BaseDomainException
    {
        public InvalidRecipeException(string error) 
        {
            this.Error = error;
        }
    }
}
