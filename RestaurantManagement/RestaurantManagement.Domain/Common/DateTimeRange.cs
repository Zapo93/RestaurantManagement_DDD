using RestaurantManagement.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Common
{
    public class DateTimeRange: ValueObject
    {
        internal DateTimeRange(DateTime start, DateTime end) 
        {
            Validate(start, end);

            this.Start = start;
            this.End = end;
        }

        private DateTimeRange() { }

        public DateTime Start { get; }
        public DateTime End { get; }

        private void Validate(DateTime start, DateTime end) 
        {
            if (start >= end) 
            {
                throw new InvalidDateTimeRange("Sart time should be before End time!");
            }
        }

        public bool IsOverlapping(DateTimeRange other) 
        {
            return this.Start < other.End && other.Start < this.End;
        }

        public bool IsDateTimeIn(DateTime time)
        {
            return time > Start && time < End;
        }

    }
}
