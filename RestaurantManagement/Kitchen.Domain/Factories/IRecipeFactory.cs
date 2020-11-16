using RestaurantManagement.Common.Domain;
using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Kitchen.Domain.Factories
{
    public interface IRecipeFactory: IFactory<Recipe>
    {
        IRecipeFactory WithName(string name);
        IRecipeFactory WithDescription(string description);
        IRecipeFactory WithPreparation(string preparation);
        IRecipeFactory WithIngredient(string name, int quantityInGrams);
    }
}
