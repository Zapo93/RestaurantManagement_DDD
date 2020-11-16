using RestaurantManagement.Common.Domain;
using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Kitchen.Domain.Specifications
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
