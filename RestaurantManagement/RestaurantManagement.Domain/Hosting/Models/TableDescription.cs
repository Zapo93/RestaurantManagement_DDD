using RestaurantManagement.Domain.Common;

namespace RestaurantManagement.Domain.Hosting.Models
{
    public class TableDescription: ValueObject
    {
        internal TableDescription(string location, bool smokersAllowed, bool indoor) 
        {
            this.Location = location;
            this.AreSmokersAllowed = smokersAllowed;
            this.IsIndoor = indoor;
        }

        public string Location { get; private set; }
        public bool AreSmokersAllowed { get; private set; }
        public bool IsIndoor { get; private set; }
    }
}