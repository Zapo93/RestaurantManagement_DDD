using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Hosting.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Domain.Hosting.Specifications
{
    public class TableFreeAtDateTimeSpecification: Specification<Table>
    {
        private readonly DateTime TargetTime = default!;

        public TableFreeAtDateTimeSpecification(DateTime targetTime) 
        {
            this.TargetTime = targetTime;
        }

        public override Expression<Func<Table, bool>> ToExpression()
        {
            return table => table.IsFree(TargetTime);
        }
    }
}
