using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Kitchen.Application.Queries.GetRecipes
{
    public class GetRecipesOutputModel
    {
        public IEnumerable<Recipe> Recipes { get; }

        public GetRecipesOutputModel(IEnumerable<Recipe> recipes) 
        {
            this.Recipes = recipes;
        }
    }
}
