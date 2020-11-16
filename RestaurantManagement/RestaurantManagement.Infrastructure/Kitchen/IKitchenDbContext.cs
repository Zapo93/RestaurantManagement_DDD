using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Common.Infrastructure.Persistence;
using RestaurantManagement.Infrastructure.Common.Persistence;
using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Infrastructure.Kitchen
{
    internal interface IKitchenDbContext: IDbContext
    {
        DbSet<Recipe> Recipes { get; }
        DbSet<Request> Requests { get; }
        DbSet<Ingredient> Ingredients{ get; }
        DbSet<RequestItem> RequestItems{ get; }
    }
}
