using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Hosting.Application.Commands.CreateTable
{
    public class CreateTableOutputModel
    {
        public CreateTableOutputModel(int tableId) 
        {
            this.TableId = tableId;
        }

        public int TableId { get; }
    }
}
