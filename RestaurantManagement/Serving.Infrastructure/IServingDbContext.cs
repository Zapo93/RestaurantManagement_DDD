using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Common.Infrastructure.Persistence;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Serving.Infrastructure
{
    internal interface IServingDbContext: IDbContext
    {
        DbSet<Dish> Dishes { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<KitchenRequest> KitchenRequests { get; }
    }
}
