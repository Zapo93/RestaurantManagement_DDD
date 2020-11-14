using RestaurantManagement.Common.Domain.Models;
using RestaurantManagement.Domain.Kitchen.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Models
{
    public class RequestItemStatus : Enumeration
    {
        public static readonly RequestItemStatus Pending = new RequestItemStatus(1, nameof(Pending));
        public static readonly RequestItemStatus InProgress = new RequestItemStatus(2, nameof(InProgress));
        public static readonly RequestItemStatus Ready = new RequestItemStatus(3, nameof(Ready));

        protected RequestItemStatus(int value, string name)
            : base(value, name)
        {
        }

        protected RequestItemStatus() { }

        public void ValidateNewStatus(RequestItemStatus newStatus)
        {
            if (newStatus.Value < this.Value)
            {
                new InvalidStatusException("The status must be greater than the current one.");
            }
        }
    }
}
