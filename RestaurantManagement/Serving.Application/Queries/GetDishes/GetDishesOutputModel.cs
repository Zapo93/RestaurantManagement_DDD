using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Serving.Application.Queries.GetDishes
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
