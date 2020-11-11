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
        private readonly string? AssigneeId = null;

        public OrdersByAssigneeSpecification(string? assigeneeId) 
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
