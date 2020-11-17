using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Common.Domain.Models;
using RestaurantManagement.Common.Infrastructure;
using RestaurantManagement.Serving.Infrastructure;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Serving.Infrastructure.Persistence
{
    internal class ServingDbContext : DbContext,
        IServingDbContext
    {
        private readonly Stack<object> savesChangesTracker;
        private readonly IEventDispatcher eventDispatcher;

        public ServingDbContext(
            DbContextOptions<ServingDbContext> options,
            IEventDispatcher eventDispatcher)
            : base(options)
        {
            this.savesChangesTracker = new Stack<object>();
            this.eventDispatcher = eventDispatcher;
        }
        public DbSet<Dish> Dishes { get; set; } = default!;

        public DbSet<Order> Orders { get; set; } = default!;

        public DbSet<OrderItem> OrderItems { get; set; } = default!;

        public DbSet<KitchenRequest> KitchenRequests { get; set; } = default!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.savesChangesTracker.Push(new object());

            var entities = this.ChangeTracker
                .Entries<IEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entities)
            {
                var events = entity.Events.ToArray();

                entity.ClearEvents();

                foreach (var domainEvent in events)
                {
                    await this.eventDispatcher.Dispatch(domainEvent);
                }
            }

            this.savesChangesTracker.Pop();

            if (!this.savesChangesTracker.Any())
            {
                return await base.SaveChangesAsync(cancellationToken);
            }

            return 0;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
