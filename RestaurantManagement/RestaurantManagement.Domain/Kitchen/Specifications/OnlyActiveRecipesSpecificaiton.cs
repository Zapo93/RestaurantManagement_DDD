using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Specifications
{
    public class OnlyActiveRecipesSpecificaiton : Specification<Recipe>
    {
        private bool OnlyActive;

        public OnlyActiveRecipesSpecificaiton(bool onlyActive) 
        {
            this.OnlyActive = onlyActive;
        }

        public override Expression<Func<Recipe, bool>> ToExpression()
        {
            if (OnlyActive) 
            {
                return recipe => recipe.Active;
            }

            return recipe => true;
        }
    }
}
