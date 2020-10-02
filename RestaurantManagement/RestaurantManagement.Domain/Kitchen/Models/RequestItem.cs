using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Models
{
    public class RequestItem: Entity<int>
    {
        internal RequestItem(Recipe recipe, string note) 
        {
            this.Recipe = recipe;
            this.Note = note!;
            this.Status = RequestStatus.Pending;
        }

        public Recipe Recipe { get; private set; }
        public string? Note { get; private set; }

        public RequestStatus Status { get; private set; }

        public void SetStatus(RequestStatus newStatus) 
        {
            this.Status.ValidateNewStatus(newStatus);
            this.Status = newStatus;
        }
    }
}
