using RestaurantManagement.Hosting.Domain.Exceptions;
using RestaurantManagement.Hosting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Hosting.Domain.Factories
{
    public class TableFactory : ITableFactory
    {
        private string Name = default!;
        private int NumberOfSeats = default!;
        private TableDescription? TableDescription;
        private string RestaurantName = default!;

        private bool IsNameSet = false;
        private bool IsNumberOfSeatsSet = false;
        private bool IsDescriptionSet = false;

        public ITableFactory WithDescription(string location, bool smokersAllowed, bool isIndoor)
        {
            TableDescription = new TableDescription(location,smokersAllowed, isIndoor);
            IsDescriptionSet = true;
            return this;
        }

        public ITableFactory WithName(string name)
        {
            Name = name;
            IsNameSet = true;
            return this;
        }

        public ITableFactory WithNumberOfSeats(int numberOfSeats)
        {
            NumberOfSeats = numberOfSeats;
            IsNumberOfSeatsSet = true;
            return this;
        }

        public ITableFactory WithRestaurantName(string name)
        {
            RestaurantName = name;
            return this;
        }

        public Table Build()
        {
            if (!IsNameSet || !IsNumberOfSeatsSet || !IsDescriptionSet) 
            {
                throw new InvalidTableException("Name, Number of seats and Description should be set!");
            }

            Table newTable = new Table(Name,NumberOfSeats,TableDescription!,RestaurantName);

            return newTable;
        }
    }
}
