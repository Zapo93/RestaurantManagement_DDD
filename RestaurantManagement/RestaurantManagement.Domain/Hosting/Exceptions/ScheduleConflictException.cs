using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Hosting.Exceptions
{
    public class ScheduleConflictException: BaseDomainException
    {
        public ScheduleConflictException(string error) 
        {
            Error = error;
        }
    }
}
