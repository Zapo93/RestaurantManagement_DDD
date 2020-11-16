using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Kitchen.Queries.GetRequests
{
    public class GetRequestsOutputModel
    {
        public IEnumerable<Request> Requests { get; }
        public GetRequestsOutputModel(IEnumerable<Request> requests) 
        {
            this.Requests = requests;
        }
    }
}
