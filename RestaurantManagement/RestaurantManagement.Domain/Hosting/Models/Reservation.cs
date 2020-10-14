using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Common.Models;
using System;

namespace RestaurantManagement.Domain.Hosting.Models
{
    public class Reservation: Entity<int>
    {
        internal Reservation(DateTimeRange timeRange, Guest? guest = null) 
        {
            this.TimeRange = timeRange;
            this.Guest = guest;
        }

        public DateTimeRange TimeRange { get; private set; }
        public Guest? Guest { get; private set; }

        public void Close() 
        {
            //TODO maybe check if reservation is currently active
            DateTime now = DateTime.UtcNow;
            if (TimeRange.End > now) 
            {
                TimeRange = new DateTimeRange(TimeRange.Start,now);
            }
        }
    }
}