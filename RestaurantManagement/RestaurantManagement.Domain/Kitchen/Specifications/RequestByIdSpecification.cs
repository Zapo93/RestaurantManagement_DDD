using RestaurantManagement.Common.Domain;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Specifications
{
    public class RequestByIdSpecification : Specification<Request>
    {
        private readonly int? RequestId = null;

        public RequestByIdSpecification(int? requestId)
        {
            this.RequestId = requestId;
        }

        protected override bool Include => RequestId != null;

        public override Expression<Func<Request, bool>> ToExpression()
        {
            return request => request.Id == RequestId;
        }
    }
}
