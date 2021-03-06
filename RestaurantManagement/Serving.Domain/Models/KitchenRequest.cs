﻿using RestaurantManagement.Common.Domain.Models;
using System;

namespace RestaurantManagement.Serving.Domain.Models
{
    public class KitchenRequest: Entity<int>
    {
        internal KitchenRequest(string value) 
        {
            this.RequestId = value;
        }

        private KitchenRequest() { }

        public string RequestId { get; }
    }
}