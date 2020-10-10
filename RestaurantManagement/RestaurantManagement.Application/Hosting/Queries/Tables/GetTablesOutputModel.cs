using RestaurantManagement.Domain.Hosting.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Hosting.Queries.Tables
{
    public class GetTablesOutputModel
    {
        public readonly IReadOnlyCollection<Table> Tables;

        public GetTablesOutputModel(IReadOnlyCollection<Table> tables) 
        {
            this.Tables = tables;
        }
    }
}
