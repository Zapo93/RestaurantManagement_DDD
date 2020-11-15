using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Common.Infrastructure.Persistence
{
    public abstract class DataRepository<TDbContext, TEntity> : IRepository<TEntity>
        where TDbContext : IDbContext
        where TEntity : class, IAggregateRoot
    {
        protected DataRepository(TDbContext db) => this.Data = db;

        protected TDbContext Data { get; }

        protected IQueryable<TEntity> All() => this.Data.Set<TEntity>();

        public async Task Save(TEntity entity, CancellationToken cancellationToken)
        {
            this.Data.Update(entity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
