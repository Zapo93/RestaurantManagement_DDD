using RestaurantManagement.Common.Domain;
using RestaurantManagement.Hosting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManagement.Hosting.Domain.Specifications
{
    public class TableMinimumSeatsSpecification: Specification<Table>
    {
        private readonly int? MinimumSeats;

        public TableMinimumSeatsSpecification(int? minimumSeats) 
        {
            this.MinimumSeats = minimumSeats;
        }

        protected override bool Include => this.MinimumSeats != null;

        public override Expression<Func<Table, bool>> ToExpression() 
        {
            return table => table.NumberOfSeats >= MinimumSeats;
        }
    }
}
