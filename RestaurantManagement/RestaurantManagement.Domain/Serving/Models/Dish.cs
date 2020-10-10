using RestaurantManagement.Domain.Common;
using System;

namespace RestaurantManagement.Domain.Serving.Models
{
    public class Dish : Entity<int>, IAggregateRoot
    {
        internal Dish(
            string name, 
            int recipeId, 
            string description, 
            Money price, 
            Uri imageUrl = null!) 
        {
            Name = name;
            RecipeId = recipeId;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            Active = true;
        }

        public string Name { get; private set; }
        public int RecipeId { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public Uri? ImageUrl { get; private set; }

        public bool Active { get; private set; }

        public void Activate() 
        {
            Active = true;
        }

        public void Deactivate() 
        {
            Active = false;
        }
    }
}
