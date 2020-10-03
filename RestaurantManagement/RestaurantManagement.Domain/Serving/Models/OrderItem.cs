using RestaurantManagement.Domain.Common;

namespace RestaurantManagement.Domain.Serving.Models
{
    public class OrderItem: Entity<int>
    {
        internal OrderItem(Dish dish, string note = null!) 
        {
            Dish = dish;
            Note = note;
        }

        public Dish Dish { get; private set; }
        public string Note { get; private set; }
    }
}
