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
        private readonly DateTime? TargetTime = null;

        public TableFreeAtDateTimeSpecification(DateTime? targetTime) 
        {
            this.TargetTime = targetTime;
        }

        protected override bool Include => this.TargetTime != null;

        public override Expression<Func<Table, bool>> ToExpression()
        {
            if (TargetTime == null) 
            {
                return table => true;
            }

            //The method IsFree can not be translated to SQL! Another way must be used to get the free tables.
            return table => table.IsFree(TargetTime.Value);
        }
    }
}
