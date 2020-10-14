using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Common.Models;
using RestaurantManagement.Domain.Hosting.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManagement.Domain.Hosting.Models
{
    public class Schedule : Entity<int>
    {
        internal Schedule(DateTimeRange timeRange)
        {
            this.TimeRange = timeRange;
            reservations = new HashSet<Reservation>();
        }

        public DateTimeRange TimeRange { get; private set; }
        private HashSet<Reservation> reservations;
        public IReadOnlyCollection<Reservation> Reservations => reservations.ToList().AsReadOnly();

        internal bool IsFreeInTime(DateTime targetTime)
        {
            DateTimeRange dateTimeRange = CreateReservationTimeRangeFromStartTime(targetTime);
            return IsFreeInTimeRange(dateTimeRange);
        }

        internal Reservation AddReservation(DateTime start, Guest? guest) 
        {
            DateTimeRange newReservationTimeRange = CreateReservationTimeRangeFromStartTime(start);
            return AddReservation(newReservationTimeRange,guest);
        }

        //Will be used when checking the current status of the table, and creating a reservation at 
        //the current time.
        private DateTimeRange CreateReservationTimeRangeFromStartTime(DateTime start)
        {
            DateTime end = start + GetDefaultReservationDuration();
            if (end > this.TimeRange.End)
            {
                end = this.TimeRange.End;
            }

            return new DateTimeRange(start, end);
        }

        private TimeSpan GetDefaultReservationDuration()
        {
            return new TimeSpan(
                ModelConstants.DefaultReservationDuration.Hours,
                ModelConstants.DefaultReservationDuration.Minutes,
                0);
        }

        internal Reservation AddReservation(DateTimeRange timeRange, Guest? guest) 
        {
            ValidateReservationTimeRange(timeRange);

            if (IsFreeInTimeRange(timeRange))
            {
                Reservation newReservation = new Reservation(timeRange, guest);
                reservations.Add(newReservation);
                return newReservation;
            }
            else 
            {
                throw new ReservationConflictException("Conflict with another reservation occured!");            
            }
        }

        private void ValidateReservationTimeRange(DateTimeRange resTimeRange) 
        {
            if (resTimeRange.Start < this.TimeRange.Start 
                || resTimeRange.End > this.TimeRange.End)
            {
                throw new InvalidTimeRangeException("Time range is out of the schedule bounds!");
            }

            if (resTimeRange.Start < DateTime.UtcNow) 
            {
                throw new InvalidTimeRangeException("Reservation can not start in the past!");
            }
        }

        public bool IsFreeInTimeRange(DateTimeRange timeRange)
        {
            foreach (Reservation res in reservations)
            {
                if (res.TimeRange.IsOverlapping(timeRange))
                {
                    return false;
                }
            }

            return true;
        }
    }
}