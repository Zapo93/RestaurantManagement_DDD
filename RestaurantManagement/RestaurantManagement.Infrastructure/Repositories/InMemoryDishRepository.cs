using RestaurantManagement.Application.Serving;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class InMemoryDishRepository : IDishRepository
    {
        private static Dictionary<int, Dish> DishDataSet = new Dictionary<int, Dish>();

        public async Task<Dish> GetDishById(int dishId, CancellationToken cancellationToken)
        {
            return DishDataSet[dishId];
        }

        public async Task Save(Dish entity, CancellationToken cancellationToken)
        {
            DishDataSet[entity.Id] = entity;
        }
    }
}
