using RestaurantManagement.Common.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Kitchen.Domain.Models
{
    public class RequestItem: Entity<int>
    {
        internal RequestItem(Recipe recipe, string note) 
        {
            this.Recipe = recipe;
            this.Note = note!;
            this.Status = RequestItemStatus.Pending;
        }

        private RequestItem() { }
        public Recipe Recipe { get; private set; }
        public string? Note { get; private set; }

        public RequestItemStatus Status { get; private set; }

        public void SetStatus(RequestItemStatus newStatus) 
        {
            this.Status.ValidateNewStatus(newStatus);
            this.Status = newStatus;
        }
    }
}
