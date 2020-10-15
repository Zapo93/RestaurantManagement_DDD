using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RestaurantManagement.Domain.Hosting.Models;
using RestaurantManagement.Domain.Kitchen.Models;
using RestaurantManagement.Domain.Serving.Models;
using RestaurantManagement.Infrastructure.Hosting;
using RestaurantManagement.Infrastructure.Kitchen;
using RestaurantManagement.Infrastructure.Serving;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Common.Persistence
{
    internal class RestaurantManagementDbContext : DbContext,
        IKitchenDbContext,
        IServingDbContext,
        IHostingDbContext
    {
        public DbSet<Recipe> Recipes { get; set; } = default!;

        public DbSet<Request> Requests { get; set; } = default!;

        public DbSet<Dish> Dishes { get; set; } = default!;

        public DbSet<Order> Orders { get; set; } = default!;

        public DbSet<Table> Tables { get; set; } = default!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
