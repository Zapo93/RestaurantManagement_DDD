using RestaurantManagement.Common.Domain.Models;

namespace RestaurantManagement.Hosting.Domain.Models
{
    public class TableDescription: ValueObject
    {
        internal TableDescription(string location, bool smokersAllowed, bool indoor) 
        {
            this.Location = location;
            this.AreSmokersAllowed = smokersAllowed;
            this.IsIndoor = indoor;
        }

        private TableDescription() { }
        public string Location { get; private set; }
        public bool AreSmokersAllowed { get; private set; }
        public bool IsIndoor { get; private set; }
    }
}