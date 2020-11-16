using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Kitchen.Application.Commands.CreateRecipe
{
    public class CreateRecipeOutputModel
    {
        public CreateRecipeOutputModel(int recipeId) 
        {
            RecipeId = recipeId;
        }

        public int RecipeId { get; }
    }
}
