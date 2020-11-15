using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Hosting;
using RestaurantManagement.Common.Domain;
using RestaurantManagement.Common.Infrastructure.Persistence;
using RestaurantManagement.Domain.Hosting.Models;
using RestaurantManagement.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task DeleteReservation(int reservationId, CancellationToken cancellationToken)
        {
            Reservation targetRes = await this.Data.Reservations.FindAsync(reservationId);

            if (targetRes == null) 
            {
                return;
            }

            this.Data.Reservations.Remove(targetRes);

            await this.Data.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<Table>> GetTables(Specification<Table> tableSpecification, CancellationToken cancellationToken)
        {
            return await this.Data.Tables
                        .Where(tableSpecification)
                        .Include("Schedules.Reservations")
                        .ToListAsync();
        }

        public Task<Table> GetTableWithScheduleForTime(int tableId, DateTime start, CancellationToken cancellationToken)
        {
            return this
                 .All()
                 .Include(t => t.Schedules) //This sintaksis does the same thing as Include("Schedules.Reservations") 
                 .ThenInclude(s => s.Reservations)
                 .FirstOrDefaultAsync(table => table.Id == tableId, cancellationToken);
        }
    }
}
