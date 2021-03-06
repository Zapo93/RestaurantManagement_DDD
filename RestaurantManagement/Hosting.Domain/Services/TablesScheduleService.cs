﻿using RestaurantManagement.Hosting.Domain.Models;
using RestaurantManagement.Hosting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantManagement.Hosting.Domain.Services
{
    public class TablesScheduleService : ITablesScheduleService
    {
        public IReadOnlyCollection<Table> GetFreeTablesForTargetTime(IEnumerable<Table> allTables, DateTime targetTime)
        {
            var freeTables = new List<Table>();

            foreach (Table table in allTables)
            {
                if (table.IsFree(targetTime)) 
                {
                    freeTables.Add(table);
                }
            }

            return freeTables.AsReadOnly();
        }
    }
}
