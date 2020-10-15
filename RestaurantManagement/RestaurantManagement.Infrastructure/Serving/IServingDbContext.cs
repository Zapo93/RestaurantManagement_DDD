using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Serving.Models;
using RestaurantManagement.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Infrastructure.Serving
{
    internal interface IServingDbContext: IDbContext
    {
        DbSet<Dish> Dishes { get; }
        DbSet<Order> Orders { get; }
    }
}
