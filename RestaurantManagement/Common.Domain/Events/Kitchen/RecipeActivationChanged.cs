using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Common.Domain.Events.Kitchen
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
