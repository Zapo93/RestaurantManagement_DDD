using RestaurantManagement.Application.Common.Contracts;
using RestaurantManagement.Domain.Hosting.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Hosting
{
    public interface ITableRepository : IRepository<Table>
    {
        Task<Table> GetTableWithScheduleForTime(int tableId, DateTime start, CancellationToken cancellationToken);
    }
}
