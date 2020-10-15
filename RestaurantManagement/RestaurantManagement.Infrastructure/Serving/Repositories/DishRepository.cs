using RestaurantManagement.Application.Serving;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Serving.Models;
using RestaurantManagement.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Serving.Repositories
{
    internal class DishRepository : DataRepository<IServingDbContext, Dish>,
        IDishRepository
    {
        public DishRepository(IServingDbContext db) : base(db)
        {
        }

        public Task<Dish> GetDishById(int dishId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Dish> GetDishByRecipeId(int recipeId, CancellationToken none)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dish>> GetDishes(Specification<Dish> dishSpec, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
