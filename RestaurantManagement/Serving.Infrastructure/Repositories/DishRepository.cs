﻿using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Serving.Application;
using RestaurantManagement.Common.Domain;
using RestaurantManagement.Common.Infrastructure.Persistence;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestaurantManagement.Serving.Infrastructure;

namespace RestaurantManagement.Serving.Infrastructure.Repositories
{
    internal class DishRepository : DataRepository<IServingDbContext, Dish>,
        IDishRepository
    {
        public DishRepository(IServingDbContext db) : base(db)
        {
        }

        public Task<Dish> GetDishById(int dishId, CancellationToken cancellationToken)
        {
            return this
                 .All()
                 .FirstOrDefaultAsync(dish => dish.Id == dishId, cancellationToken);
        }

        public Task<Dish> GetDishByRecipeId(int recipeId, CancellationToken cancellationToken)
        {
            return this
                 .All()
                 .FirstOrDefaultAsync(dish => dish.RecipeId == recipeId, cancellationToken);
        }

        public async Task<IEnumerable<Dish>> GetDishes(Specification<Dish> dishSpec, CancellationToken cancellationToken)
        {
            return await this.Data.Dishes.Where(dishSpec).ToListAsync();
        }
    }
}
