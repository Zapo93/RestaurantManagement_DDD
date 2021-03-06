﻿using RestaurantManagement.Common.Domain;
using RestaurantManagement.Common.Domain.Models;
using RestaurantManagement.Hosting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static RestaurantManagement.Hosting.Domain.Models.ModelConstants;

namespace RestaurantManagement.Hosting.Domain.Models
{
    public class Table : Entity<int>, IAggregateRoot
    {
        internal Table(
            string name,
            int numberOfSeats,
            TableDescription tableDescription,
            string? restaurantName = null)
        {
            this.Name = name;
            this.NumberOfSeats = numberOfSeats;
            this.Description = tableDescription;
            this.RestaurantName = RestaurantName;

            schedules = new HashSet<Schedule>();
        }

        private Table() { }

        private readonly HashSet<Schedule> schedules;
        public IReadOnlyCollection<Schedule>? Schedules => schedules?.ToList().AsReadOnly();
        public string Name { get; private set; }
        public string? RestaurantName { get; private set; }
        public int NumberOfSeats { get; private set; }
        public TableDescription Description { get; private set; }

        public bool IsFree(DateTime targetTime)
        {
            return ValidateTargetTime(targetTime)
                && (GetScheduleForDateTime(targetTime)?.IsFreeInTime(targetTime) ?? true);
        }

        private bool ValidateTargetTime(DateTime targetTime) 
        {
            DateTimeRange scheduleTimeRange = CreateScheduleTimeRangeFromDateTime(targetTime);

            return scheduleTimeRange.IsDateTimeIn(targetTime);
        }

        public Reservation AddReservation(DateTime start, Guest? guest)
        {
            Schedule? scheduleForDay = GetScheduleForDateTime(start);
            if (scheduleForDay == null) 
            {
                DateTimeRange newScheduleTimeRange = CreateScheduleTimeRangeFromDateTime(start);
                scheduleForDay = AddSchedule(newScheduleTimeRange);
            }

            return scheduleForDay.AddReservation(start, guest);
        }

        public Schedule? GetScheduleForDateTime(DateTime targetTime)
        {
            if (schedules != null)
            {
                foreach (Schedule sched in schedules)
                {
                    if (sched.TimeRange.IsDateTimeIn(targetTime))
                    {
                        return sched;
                    }
                } 
            }

            return null;
        }

        private DateTimeRange CreateScheduleTimeRangeFromDateTime(DateTime time) 
        {
            DateTime startTime = new DateTime(
                time.Year, 
                time.Month, 
                time.Day, 
                ModelConstants.ScheduleStartingTime.Hours,
                ModelConstants.ScheduleStartingTime.Minutes,
                0);

            DateTime endTime = new DateTime(
                time.Year,
                time.Month,
                time.Day,
                ModelConstants.ScheduleClosingTime.Hours,
                ModelConstants.ScheduleClosingTime.Minutes,
                0);

            return new DateTimeRange(startTime,endTime);
        }

        private Schedule AddSchedule(DateTimeRange newSchedTimeRange) 
        {
            if (HasNoConflictWithOtherSchedules(newSchedTimeRange))
            {
                Schedule newSchedule = new Schedule(newSchedTimeRange);
                schedules.Add(newSchedule);

                return newSchedule;
            }
            else 
            {
                throw new ScheduleConflictException("Conflict with another schedule occured!");
            }
        }

        private bool HasNoConflictWithOtherSchedules(DateTimeRange timeRange) 
        {
            //Schedule conflictedSched = schedules.FirstOrDefault(schedule => schedule.TimeRange.IsOverlapping(timeRange));
            foreach (Schedule sched in schedules) 
            {
                if (sched.TimeRange.IsOverlapping(timeRange))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
