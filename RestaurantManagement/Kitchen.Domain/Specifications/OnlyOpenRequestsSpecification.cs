using RestaurantManagement.Common.Domain;
using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Kitchen.Domain.Specifications
{
    public class OnlyOpenRequestsSpecification : Specification<Request>
    {
        private readonly bool OnlyOpenRequest;

        public OnlyOpenRequestsSpecification(bool onlyOpen) 
        {
            this.OnlyOpenRequest = onlyOpen;
        }

        public override Expression<Func<Request, bool>> ToExpression()
        {
            if (OnlyOpenRequest) 
            {
                return request => request.Status.Value < RequestStatus.Ready.Value;
            }

            return request => true;
        }
    }
}
