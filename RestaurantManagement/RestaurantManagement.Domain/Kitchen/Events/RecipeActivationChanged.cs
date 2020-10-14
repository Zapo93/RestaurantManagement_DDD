using RestaurantManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Events
{
    public class RecipeActivationChanged: IDomainEvent
    {
        public int RecipeId { get; private set; }
        public bool Value { get; private set; }

        public RecipeActivationChanged(int recipeId, bool value) 
        {
            RecipeId = recipeId;
            Value = value;
        }
    }
}
