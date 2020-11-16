using RestaurantManagement.Common.Domain;
using RestaurantManagement.Common.Domain.Events.Kitchen;
using RestaurantManagement.Common.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Kitchen.Domain.Models
{
    public class Recipe : Entity<int>, IAggregateRoot
    {
        internal Recipe(
                string name,
                string preparation,
                string description,
                bool active = true
            ) 
        {
            this.Name = name;
            this.Preparation = preparation;
            this.Description = description;
            this.Active = active;
            ingredients = new List<Ingredient>();
        }

        private Recipe() { }

        private readonly List<Ingredient> ingredients;

        public string Name { get; private set; }
        public string Preparation { get; private set; }
        public string? Description { get; private set; }

        public IReadOnlyCollection<Ingredient> Ingredients => ingredients.AsReadOnly();

        public bool Active { get; private set; }

        public void Activate() 
        {
            Active = true;
            RaiseEvent(new RecipeActivationChanged(Id,Active));
        }

        public void Deactivate() 
        {
            Active = false;
            RaiseEvent(new RecipeActivationChanged(Id, Active));
        }

        public void AddIngredient(string name, int quantityInGrams) 
        {
            Ingredient newIngredient = new Ingredient(name, quantityInGrams);
            ingredients.Add(newIngredient);
        }

        public void AddIngredient(Ingredient newIngredient) 
        {
            ingredients.Add(newIngredient);
        }
    }
}
