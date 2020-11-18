using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RestaurantManagement.Common.Domain.Models;
using RestaurantManagement.Hosting.Domain.Models;
using RestaurantManagement.Infrastructure.Hosting;
using RestaurantManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestaurantManagement.Common.Infrastructure;

namespace RestaurantManagement.Infrastructure.Common.Persistence
{
    internal class RestaurantManagementDbContext : DbContext,
        IHostingDbContext
    {
        private readonly Stack<object> savesChangesTracker;
        private readonly IEventDispatcher eventDispatcher;

        public RestaurantManagementDbContext(
            DbContextOptions<RestaurantManagementDbContext> options,
            IEventDispatcher eventDispatcher)
            : base(options)
        {
            this.savesChangesTracker = new Stack<object>();
            this.eventDispatcher = eventDispatcher;
        }

        public DbSet<Table> Tables { get; set; } = default!;

        public DbSet<Reservation> Reservations{ get; set; } = default!;

        public DbSet<Schedule> Schedules { get; set; } = default!;

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
