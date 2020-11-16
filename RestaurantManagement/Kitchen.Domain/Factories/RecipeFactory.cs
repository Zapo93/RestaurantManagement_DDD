using RestaurantManagement.Kitchen.Domain.Exceptions;
using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Kitchen.Domain.Factories
{
    public class RecipeFactory : IRecipeFactory
    {
        private string Name = default!;
        private string Preparation = default!;
        private string Description = default!;
        private List<Ingredient> Ingredients;

        private bool IsNameSet = false;
        private bool IsPreparationSet = false;

        public RecipeFactory() 
        {
            Ingredients = new List<Ingredient>();
        }

        public IRecipeFactory WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public IRecipeFactory WithIngredient(string name, int quantityInGrams)
        {
            Ingredient newIngredient = new Ingredient(name, quantityInGrams);
            Ingredients.Add(newIngredient);
            
            return this;
        }

        public IRecipeFactory WithName(string name)
        {
            Name = name;
            IsNameSet = true;

            return this;
        }

        public IRecipeFactory WithPreparation(string preparation)
        {
            Preparation = preparation;
            IsPreparationSet = true;

            return this;
        }

        public Recipe Build()
        {
            if (!IsNameSet || !IsPreparationSet) 
            {
                throw new InvalidRecipeException("Name and Preparation must be set!");
            }

            Recipe newRecipe = new Recipe(Name,Preparation,Description);

            if (Ingredients.Count > 0)
            {
                foreach (Ingredient ingredient in Ingredients) 
                {
                    newRecipe.AddIngredient(ingredient);
                }
            }
            else 
            {
                throw new InvalidRecipeException("There must be at least one ingredient!");
            }

            return newRecipe;
        }
    }
}
