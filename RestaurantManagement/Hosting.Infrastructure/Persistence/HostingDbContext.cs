using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Common.Domain.Models;
using RestaurantManagement.Common.Infrastructure;
using RestaurantManagement.Hosting.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Hosting.Infrastructure.Persistence
{
    internal class HostingDbContext : DbContext,
        IHostingDbContext
    {
        private readonly Stack<object> savesChangesTracker;
        private readonly IEventDispatcher eventDispatcher;

        public HostingDbContext(
            DbContextOptions<HostingDbContext> options,
            IEventDispatcher eventDispatcher)
            : base(options)
        {
            this.savesChangesTracker = new Stack<object>();
            this.eventDispatcher = eventDispatcher;
        }

        public DbSet<Table> Tables { get; set; } = default!;

        public DbSet<Reservation> Reservations { get; set; } = default!;

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
