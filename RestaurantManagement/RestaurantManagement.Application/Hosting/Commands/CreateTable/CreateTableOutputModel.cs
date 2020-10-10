using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Hosting.Commands.CreateTable
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
