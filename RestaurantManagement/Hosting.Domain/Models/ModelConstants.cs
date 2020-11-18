using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Hosting.Domain.Models
{
    class ModelConstants
    {
        public class ScheduleStartingTime
        {
            public const int Hours = 11;
            public const int Minutes = 0;
        }

        public class ScheduleClosingTime
        {
            public const int Hours = 22;
            public const int Minutes = 30;
        }

        public class DefaultReservationDuration 
        {
            public const int Hours = 4;
            public const int Minutes = 0;
        }
    }
}
