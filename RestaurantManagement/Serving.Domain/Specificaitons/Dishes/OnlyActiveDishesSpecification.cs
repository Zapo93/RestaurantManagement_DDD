using RestaurantManagement.Common.Domain;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Serving.Domain.Specificaitons.Dishes
{
    public class OnlyActiveDishesSpecification : Specification<Dish>
    {
        private readonly bool OnlyActive;

        public OnlyActiveDishesSpecification(bool onlyActive) 
        {
            this.OnlyActive = onlyActive;
        }

        public override Expression<Func<Dish, bool>> ToExpression()
        {
            if (OnlyActive) 
            {
                return dish => dish.Active;
            }

            return dish => true;
        }
    }
}
