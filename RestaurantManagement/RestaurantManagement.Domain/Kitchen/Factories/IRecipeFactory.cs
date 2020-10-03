using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Factories
{
    public interface IRecipeFactory: IFactory<Recipe>
    {
        IRecipeFactory WithName(string name);
        IRecipeFactory WithDescription(string description);
        IRecipeFactory WithPreparation(string preparation);
        IRecipeFactory WithIngredient(string name, int quantityInGrams);
    }
}
