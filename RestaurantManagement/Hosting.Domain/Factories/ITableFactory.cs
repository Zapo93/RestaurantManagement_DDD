using RestaurantManagement.Common.Domain;
using RestaurantManagement.Hosting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Hosting.Domain.Factories
{
    public interface ITableFactory: IFactory<Table>
    {
        ITableFactory WithName(string name);
        ITableFactory WithNumberOfSeats(int numberOfSeats);
        ITableFactory WithDescription(string location, bool smokersAllowed, bool isIndoor);
        ITableFactory WithRestaurantName(string Name);
    }
}
