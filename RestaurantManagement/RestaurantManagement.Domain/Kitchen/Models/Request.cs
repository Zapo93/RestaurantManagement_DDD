using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Models
{
    public class Request: Entity<int>, IAggregateRoot
    {
        internal Request(string creatorReferenceId) 
        {
            items = new HashSet<RequestItem>();
            DateCreated = DateTime.UtcNow;
            Status = RequestStatus.Pending;
            CreatorReferenceId = creatorReferenceId;
        }

        private HashSet<RequestItem> items;
        public IReadOnlyCollection<RequestItem> Items => this.items.ToList().AsReadOnly();
        public DateTime DateCreated { get; private set; }
        public RequestStatus Status { get; private set; }
        public string CreatorReferenceId { get; private set; }
        public void AddItem(Recipe recipe, string note) 
        {
            var newRequestItem = new RequestItem(recipe,note);
            items.Add(newRequestItem);
        }

        public void AddItem(RequestItem newRequestItem)
        {
            items.Add(newRequestItem);
        }

        public void SetStatus(RequestStatus newStatus) 
        {
            this.Status.ValidateNewStatus(newStatus);
            //TODO test if the if statement works without .equals()
            if (newStatus == RequestStatus.Ready) 
            {
                SetAllItemsToReady();
            }

            this.Status = newStatus;
        }

        private void SetAllItemsToReady() 
        {
            foreach (RequestItem item in Items) 
            {
                item.SetStatus(RequestStatus.Ready);
            }
        }

        public void SetItemStatus(int itemId, RequestStatus newStatus) 
        {
            foreach (RequestItem item in Items)
            {
                if (item.Id == itemId) 
                {
                    item.SetStatus(newStatus);
                }
            }
        }
    }
}
