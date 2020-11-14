using RestaurantManagement.Application.Common.Contracts;
using RestaurantManagement.Common.Domain;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Serving
{
    public interface IDishRepository : IRepository<Dish>
    {
        Task<Dish> GetDishById(int dishId, CancellationToken cancellationToken);
        Task<IEnumerable<Dish>> GetDishes(Specification<Dish> dishSpec, CancellationToken cancellationToken);
        Task<Dish> GetDishByRecipeId(int recipeId, CancellationToken cancellationToken);
    }
}
