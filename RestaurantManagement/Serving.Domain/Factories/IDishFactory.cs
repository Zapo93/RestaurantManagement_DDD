using RestaurantManagement.Common.Domain;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Serving.Domain.Factories
{
    public interface IDishFactory: IFactory<Dish>
    {
        IDishFactory WithName(string name);
        IDishFactory WithRecipeId(int recipeId);
        IDishFactory WithDescription(string description);
        IDishFactory WithPrice(double ammount, string currency);
        IDishFactory WithImage(Uri imageUrl);
    }
}
