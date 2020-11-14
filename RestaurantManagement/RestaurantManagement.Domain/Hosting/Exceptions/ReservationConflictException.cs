using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Hosting.Exceptions
{
    public class ReservationConflictException :BaseDomainException
    {
        public ReservationConflictException(string error) 
        {
            this.Error = error;
        }
    }
}
