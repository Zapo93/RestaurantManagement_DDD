using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RestaurantManagement.Common.Domain.Models;
using RestaurantManagement.Common.Infrastructure;
using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Kitchen.Infrastructure.Persistence
{
    internal class KitchenDbContext : DbContext,
        IKitchenDbContext
    {
        private readonly Stack<object> savesChangesTracker;
        private readonly IEventDispatcher eventDispatcher;

        public KitchenDbContext(
            DbContextOptions<KitchenDbContext> options,
            IEventDispatcher eventDispatcher)
            : base(options)
        {
            this.savesChangesTracker = new Stack<object>();
            this.eventDispatcher = eventDispatcher;
        }

        public DbSet<Recipe> Recipes { get; set; } = default!;

        public DbSet<Request> Requests { get; set; } = default!;

        public DbSet<Ingredient> Ingredients { get; set; } = default!;

        public DbSet<RequestItem> RequestItems { get; set; } = default!;

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
