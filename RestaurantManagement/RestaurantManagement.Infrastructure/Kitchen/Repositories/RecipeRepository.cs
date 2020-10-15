using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Kitchen;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Kitchen.Models;
using RestaurantManagement.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Kitchen.Repositories
{
    internal class RecipeRepository : DataRepository<IKitchenDbContext, Recipe>,
        IRecipeRepository
    {
        public RecipeRepository(IKitchenDbContext db) : base(db)
        {
        }

        public Task<Recipe> GetRecipeById(int recipeId, CancellationToken cancellationToken)
        {
            return this
                .All()
                .FirstOrDefaultAsync(recipe => recipe.Id == recipeId, cancellationToken);
        }

        public async Task<IEnumerable<Recipe>> GetRecipes(Specification<Recipe> recipeSpec, CancellationToken cancellationToken)
        {
            return await this.Data.Recipes.Where(recipeSpec).ToListAsync();
        }
    }
}
