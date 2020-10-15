using RestaurantManagement.Application.Hosting;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Hosting.Models;
using RestaurantManagement.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Hosting.Repositories
{
    internal class TableRepository : DataRepository<IHostingDbContext, Table>,
        ITableRepository
    {
        public TableRepository(IHostingDbContext db) : base(db)
        {
        }

        public Task DeleteReservation(int reservationId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Table>> GetTables(Specification<Table> tableSpecification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Table> GetTableWithScheduleForTime(int tableId, DateTime start, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
