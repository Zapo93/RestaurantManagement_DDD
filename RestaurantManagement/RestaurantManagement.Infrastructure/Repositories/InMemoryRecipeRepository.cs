using RestaurantManagement.Application.Kitchen;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class InMemoryRecipeRepository : IRecipeRepository
    {
        private static Dictionary<int, Recipe> RecipeDataSet = new Dictionary<int, Recipe>();

        public async Task<Recipe> GetRecipeById(int recipeId, CancellationToken cancellationToken)
        {
            return RecipeDataSet[recipeId];
        }

        public async Task Save(Recipe entity, CancellationToken cancellationToken)
        {
            //TODO this only works with 1 entity because ID is private and cannot be set
            RecipeDataSet[entity.Id] = entity;
        }
    }
}
