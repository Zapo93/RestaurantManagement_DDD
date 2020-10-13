using RestaurantManagement.Domain.Kitchen.Exceptions;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Factories
{
    public class RequestFactory : IRequestFactory
    {
        private string CreatorReferenceId;
        private List<RequestItem> Items;

        private bool IsCreatorReferenceIdSet = false; 

        public RequestFactory() 
        {
            Items = new List<RequestItem>();
        }

        public IRequestFactory WithCreatorReferenceId(string creatorReferenceId)
        {
            CreatorReferenceId = creatorReferenceId;
            IsCreatorReferenceIdSet = true;

            return this;
        }

        public IRequestFactory WithItem(Recipe recipe, string note)
        {
            RequestItem requestItem = new RequestItem(recipe,note);
            Items.Add(requestItem);

            return this;
        }

        public Request Build()
        {
            if (!IsCreatorReferenceIdSet) 
            {
                throw new InvalidRequestException("Creator reference Id must be set!");
            }

            Request newRequest = new Request(CreatorReferenceId);
            if (Items.Count > 0)
            {
                foreach (RequestItem item in Items) 
                {
                    newRequest.AddItem(item);
                }
            }   
            else 
            {
                throw new InvalidRequestException("At least one item should be requested!");
            }

            return newRequest;
        }
    }
}
