using RestaurantManagement.Domain.Serving.Exceptions;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Serving.Factories
{
    public class DishFactory : IDishFactory
    {
        private string Name = default!;
        private string Description = String.Empty;
        private Money? Price;
        private int RecipeId = default!;
        private Uri ImageUrl = default!;

        private bool IsNameSet = false;
        private bool IsRecipeSet = false;
        private bool IsPriceSet = false;

        public DishFactory() {}

        public IDishFactory WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public IDishFactory WithImage(Uri imageUrl)
        {
            ImageUrl = imageUrl;
            return this;
        }

        public IDishFactory WithName(string name)
        {
            Name = name;
            IsNameSet = true;

            return this;
        }

        public IDishFactory WithPrice(double ammount, string currency = null!)
        {
            Price = new Money(ammount);
            IsPriceSet = true;

            return this;
        }

        public IDishFactory WithRecipeId(int recipeId)
        {
            RecipeId = recipeId;
            IsRecipeSet = true;

            return this;
        }

        public Dish Build()
        {
            if (!IsNameSet || !IsRecipeSet || !IsPriceSet) 
            {
                throw new InvalidDishException("Name, Price and Recipe must be set!");
            }

            return new Dish(Name,RecipeId,Description,Price!,ImageUrl);
        }

    }
}
