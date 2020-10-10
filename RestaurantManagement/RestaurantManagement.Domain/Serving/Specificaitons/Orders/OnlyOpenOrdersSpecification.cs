using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Domain.Serving.Specificaitons.Orders
{
    public class OnlyOpenOrdersSpecification : Specification<Order>
    {
        private readonly bool OnlyOpen;

        public OnlyOpenOrdersSpecification(bool onlyOpen)
        {
            this.OnlyOpen = onlyOpen;
        }

        public override Expression<Func<Order, bool>> ToExpression()
        {
            if (OnlyOpen) 
            {
                return order => order.Open;
            }

            return order => true;
        }
    }
}
