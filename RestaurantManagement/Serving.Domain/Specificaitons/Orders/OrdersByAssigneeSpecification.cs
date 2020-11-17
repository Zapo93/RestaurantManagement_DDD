using RestaurantManagement.Common.Domain;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Serving.Domain.Specificaitons.Orders
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
