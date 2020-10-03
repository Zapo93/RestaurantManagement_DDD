using RestaurantManagement.Domain.Kitchen.Exceptions;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Factories
{
    public class RequestFactory : IRequestFactory
    {
        private List<RequestItem> Items;

        public RequestFactory() 
        {
            Items = new List<RequestItem>();
        }

        public IRequestFactory WithItem(Recipe recipe, string note)
        {
            RequestItem requestItem = new RequestItem(recipe,note);
            Items.Add(requestItem);

            return this;
        }

        public Request Build()
        {
            Request newRequest = new Request();
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
