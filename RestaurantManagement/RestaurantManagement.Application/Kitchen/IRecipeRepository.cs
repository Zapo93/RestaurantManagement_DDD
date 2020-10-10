using RestaurantManagement.Application.Common.Contracts;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Kitchen
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<Recipe> GetRecipeById(int recipeId, CancellationToken cancellationToken);
        Task<IEnumerable<Recipe>> GetRecipes(Specification<Recipe> recipeSpec, CancellationToken cancellationToken);
    }
}
