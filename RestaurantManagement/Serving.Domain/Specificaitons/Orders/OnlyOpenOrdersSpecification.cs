using RestaurantManagement.Common.Domain;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Serving.Domain.Specificaitons.Orders
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
