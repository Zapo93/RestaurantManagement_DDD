
using RestaurantManagement.Domain.Hosting.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Hosting.Services
{
    public interface ITablesScheduleService
    {
        IReadOnlyCollection<Table> GetFreeTablesForTargetTime(IEnumerable<Table> allTables, DateTime targetTime); 
    }
}
