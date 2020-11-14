using RestaurantManagement.Common.Domain.Models;
using RestaurantManagement.Domain.Kitchen.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Models
{
    public class RequestStatus : Enumeration
    {
        public static readonly RequestStatus Pending = new RequestStatus(1, nameof(Pending));
        public static readonly RequestStatus InProgress = new RequestStatus(2, nameof(InProgress));
        public static readonly RequestStatus Ready = new RequestStatus(3, nameof(Ready));

        private RequestStatus(int value, string name)
            : base(value, name)
        {
        }

        private RequestStatus() { }

        public void ValidateNewStatus(RequestStatus newStatus)
        {
            if (newStatus.Value < this.Value)
            {
                new InvalidStatusException("The status must be greater than the current one.");
            }
        }
    }
}
