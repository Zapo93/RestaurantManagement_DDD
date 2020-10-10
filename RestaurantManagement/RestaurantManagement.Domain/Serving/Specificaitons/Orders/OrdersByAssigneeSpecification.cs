using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Domain.Serving.Specificaitons.Orders
{
    public class OrdersByAssigneeSpecification : Specification<Order>
    {
        private readonly int? AssigneeId = null;

        public OrdersByAssigneeSpecification(int? assigeneeId) 
        {
            this.AssigneeId = assigeneeId;
        }

        protected override bool Include => AssigneeId != null;

        public override Expression<Func<Order, bool>> ToExpression()
        {
            return order => order.AssigneeId == AssigneeId;
        }
    }
}
