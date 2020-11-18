using RestaurantManagement.Common.Domain;
using RestaurantManagement.Common.Domain.Models;
using System;

namespace RestaurantManagement.Hosting.Domain.Models
{
    public class Reservation: Entity<int>
    {
        internal Reservation(DateTimeRange timeRange, Guest? guest = null) 
        {
            this.TimeRange = timeRange;
            this.Guest = guest;
        }

        private Reservation() { }

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