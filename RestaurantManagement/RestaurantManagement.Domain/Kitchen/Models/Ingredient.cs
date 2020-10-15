using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Models
{
    public class Ingredient: ValueObject
    {
		//TODO change back to internal if the JSON serializer works with internal
        public Ingredient(string name, int quantityInGrams) 
        {
            Name = name;
            QuantityInGrams = quantityInGrams;
        }

        private Ingredient() { }

        public string Name { get; private set; }
        //TODO Validate - it should not be negative value
        public int QuantityInGrams { get; private set; }
    }
}
