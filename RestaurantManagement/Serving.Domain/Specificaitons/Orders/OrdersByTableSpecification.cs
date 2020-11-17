using RestaurantManagement.Common.Domain;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Serving.Domain.Specificaitons.Orders
{
    public class OrdersByTableSpecification : Specification<Order>
    {
        private readonly int? TableId = null;

        public OrdersByTableSpecification(int? tableId) 
        {
            this.TableId = tableId; 
        }

        protected override bool Include => TableId != null;

        public override Expression<Func<Order, bool>> ToExpression()
        {
            return order => order.TableId == TableId;
        }
    }
}
