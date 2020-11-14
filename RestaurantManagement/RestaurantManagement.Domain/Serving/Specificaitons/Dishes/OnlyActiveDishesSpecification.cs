using RestaurantManagement.Common.Domain;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Domain.Serving.Specificaitons.Dishes
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
