
using RestaurantManagement.Hosting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Hosting.Domain.Services
{
    public interface ITablesScheduleService
    {
        IReadOnlyCollection<Table> GetFreeTablesForTargetTime(IEnumerable<Table> allTables, DateTime targetTime); 
    }
}
