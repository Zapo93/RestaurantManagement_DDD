namespace RestaurantManagement.Application.Kitchen.Commands.SetRequestStatus
{
    public class ItemStatus
    {
        public ItemStatus()
        {

        }
        public ItemStatus(int itemId, int newStatus) 
        {
            this.ItemId = itemId;
            this.NewStatus = newStatus;
        }
        
        public int ItemId { get; }
        public int NewStatus { get; }
    }
}