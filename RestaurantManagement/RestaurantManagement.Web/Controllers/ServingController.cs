using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Serving.Commands.AddItemsToOrder;
using RestaurantManagement.Application.Serving.Commands.CloseOrder;
using RestaurantManagement.Application.Serving.Commands.CreateDish;
using RestaurantManagement.Application.Serving.Commands.CreateOrder;
using RestaurantManagement.Application.Serving.Queries.GetDishes;
using RestaurantManagement.Application.Serving.Queries.GetOrders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Web.Controllers
{
    public class ServingController: BaseAPIController
    {
        [HttpPost]
        public async Task<ActionResult<CreateDishOutputModel>> CreateDish(CreateDishCommand createDishCommand)
        {
            return await Send(createDishCommand);
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrderOutputModel>> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            return await Send(createOrderCommand);
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> AddItemsToOrder(AddItemsToOrderCommand addItemsToOrderCommand)
        {
            return await Send(addItemsToOrderCommand);
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CloseOrder(CloseOrderCommand closeOrderCommand)
        {
            return await Send(closeOrderCommand);
        }

        [HttpGet]
        public async Task<ActionResult<GetDishesOutputModel>> GetDishes([FromQuery] DishesQuery dishesQuery)
        {
            return await Send(dishesQuery);
        }

        [HttpGet]
        public async Task<ActionResult<GetOrdersOutputModel>> GetOrders([FromQuery] OrdersQuery ordersQuery)
        {
            return await Send(ordersQuery);
        }
    }
}
