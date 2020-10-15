using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Kitchen.Models;
using RestaurantManagement.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Infrastructure.Kitchen
{
    internal interface IKitchenDbContext: IDbContext
    {
        DbSet<Recipe> Recipes { get; }

        DbSet<Request> Requests { get; }
    }
}
