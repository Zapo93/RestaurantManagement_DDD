using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Serving.Queries.GetDishes
{
    public class GetDishesOutputModel
    {
        public readonly IEnumerable<Dish> Dishes;

        public GetDishesOutputModel(IEnumerable<Dish> dishes)  
        {
            this.Dishes = dishes;
        }
    }
}
