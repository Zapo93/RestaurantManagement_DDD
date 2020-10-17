using RestaurantManagement.Domain.Common.Models;
using System;

namespace RestaurantManagement.Domain.Serving.Models
{
    public class KitchenRequest: Entity<int>
    {
        internal KitchenRequest(string value) 
        {
            this.RequestId = value;
        }

        public string RequestId { get; }
    }
}