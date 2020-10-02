using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Models
{
    public class Recipe : Entity<int>, IAggregateRoot
    {
        internal Recipe(
                string name,
                string howToPrepare,
                string description
            ) 
        {
            this.Name = name;
            this.HowToPrepare = howToPrepare;
            this.Description = description;
            ingredients = new List<Ingredient>();
        }

        private readonly List<Ingredient> ingredients;

        public string Name { get; private set; }
        public string HowToPrepare { get; private set; }
        public string Description { get; private set; }

        public IReadOnlyCollection<Ingredient> Ingredients => ingredients.AsReadOnly();

        public void AddIngredient(string name, int quantityInGrams) 
        {
            Ingredient newIngredient = new Ingredient(name, quantityInGrams);
            ingredients.Add(newIngredient);
        }
    }
}
